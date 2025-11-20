using UnityEngine;
using TMPro;

public class GoalSceneTimeDisplay : MonoBehaviour
{
    public TextMeshProUGUI clearTimeText;

    void Start()
    {
        // 保存されたクリアタイムを取得
        float clearTime = PlayerPrefs.GetFloat("ClearTime", -1f);

        // 取得できていれば表示
        if (clearTime >= 0f)
        {
            clearTimeText.text = $"Clear Time : {clearTime:F2} s";
        }
        else
        {
            clearTimeText.text = "Clear Time : ---";
        }
    }
}
