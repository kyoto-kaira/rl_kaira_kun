using UnityEngine;

public class SpawnOnClick : MonoBehaviour
{
    public GameObject spawnPrefab; // 生成するプレハブ

    [Header("Spawn Options")]
    [Tooltip("生成したターゲットを CrawlerAgent に登録するか")]
    public bool registerToCrawlerAgent = true;

    [Tooltip("登録先の CrawlerAgent。未設定ならシーンから自動検出します")]
    public CrawlerAgent crawlerAgent;

    [Tooltip("生成した GameObject にタグを設定するか")]
    public bool setTagOnSpawned = true;

    [Tooltip("生成した GameObject に付与するタグ名（CrawlerAgent 側の m_TargetTag と一致させる）")]
    public string targetTag = "Target";

    [Tooltip("新規生成前に既存のTargetを全削除するか")]
    public bool removeExistingTargetsOnSpawn = true;

    // このスクリプトが生成したターゲットの追跡（タグ未使用時のフォールバック用）
    private static readonly System.Collections.Generic.List<GameObject> s_spawnedTargets =
        new System.Collections.Generic.List<GameObject>();

    void Update()
    {
        // マウス左クリックを検出
        if (Input.GetMouseButtonDown(0))
        {
            // カメラからマウス位置に向けてレイを飛ばす
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // レイが当たったら
            if (Physics.Raycast(ray, out hit))
            {
                // 既存ターゲットを削除（タグが設定されていればタグで、なければ本スクリプト管理分のみ）
                if (removeExistingTargetsOnSpawn)
                {
                    bool deletedByTag = false;
                    if (!string.IsNullOrEmpty(targetTag))
                    {
                        try
                        {
                            var existing = GameObject.FindGameObjectsWithTag(targetTag);
                            for (int i = 0; i < existing.Length; i++)
                            {
                                Destroy(existing[i]);
                            }
                            deletedByTag = true;
                        }
                        catch (UnityException)
                        {
                            Debug.LogWarning($"Tag '{targetTag}' がプロジェクトに定義されていません。Tags & Layers で追加してください。");
                        }
                    }

                    // タグで削除できなかった場合は、このスクリプトが生成したものだけ削除
                    if (!deletedByTag)
                    {
                        for (int i = s_spawnedTargets.Count - 1; i >= 0; i--)
                        {
                            if (s_spawnedTargets[i] != null)
                            {
                                Destroy(s_spawnedTargets[i]);
                            }
                            s_spawnedTargets.RemoveAt(i);
                        }
                    }
                }

                // 当たった場所にプレハブ生成
                var spawned = Instantiate(spawnPrefab, hit.point, Quaternion.identity);

                // 2. 生成した GameObject に "Target" タグを付ける（任意）
                if (setTagOnSpawned && !string.IsNullOrEmpty(targetTag))
                {
                    try
                    {
                        spawned.tag = targetTag; // タグが未定義の場合は例外
                    }
                    catch (UnityException)
                    {
                        Debug.LogWarning($"Tag '{targetTag}' がプロジェクトに定義されていません。Tags & Layers で追加してください。");
                    }
                }

                // 1. 生成した Transform を crawlerAgent.RegisterTarget(createdTransform); で登録（任意）
                if (registerToCrawlerAgent)
                {
                    var agent = crawlerAgent != null ? crawlerAgent : FindObjectOfType<CrawlerAgent>();
                    if (agent != null)
                    {
                        agent.RegisterTarget(spawned.transform);
                    }
                    else
                    {
                        Debug.LogWarning("CrawlerAgent が見つかりませんでした。RegisterTarget はスキップされます。");
                    }
                }

                // 追跡リストに追加（タグ未使用時の削除用フォールバック）
                s_spawnedTargets.Add(spawned);
            }
        }
    }
}