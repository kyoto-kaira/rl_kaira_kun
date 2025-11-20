using UnityEngine;
using TMPro;

namespace MLAgentsExamples
{
    public class AdjustTrainingTimescale : MonoBehaviour
    {
        public TextMeshProUGUI timerText;  // 経過時間表示用

        public static float ElapsedTime { get; private set; } = 0f;

        void Start()
        {
            // シーン開始時に経過時間リセット
            ElapsedTime = 0f;
        }

        void Update()
        {
            // 経過時間更新
            ElapsedTime += Time.unscaledDeltaTime;

            // 経過時間を右下に表示
            if (timerText != null)
            {
                timerText.text = $"Time: {ElapsedTime:F2}";
            }

            // ---- TimeScale 調整 ----
            if (Input.GetKeyDown(KeyCode.Alpha1)) Time.timeScale = 1f;
            if (Input.GetKeyDown(KeyCode.Alpha2)) Time.timeScale = 2f;
            if (Input.GetKeyDown(KeyCode.Alpha3)) Time.timeScale = 3f;
            if (Input.GetKeyDown(KeyCode.Alpha4)) Time.timeScale = 4f;
            if (Input.GetKeyDown(KeyCode.Alpha5)) Time.timeScale = 5f;
            if (Input.GetKeyDown(KeyCode.Alpha6)) Time.timeScale = 6f;
            if (Input.GetKeyDown(KeyCode.Alpha7)) Time.timeScale = 7f;
            if (Input.GetKeyDown(KeyCode.Alpha8)) Time.timeScale = 8f;
            if (Input.GetKeyDown(KeyCode.Alpha9)) Time.timeScale = 9f;
            if (Input.GetKeyDown(KeyCode.Alpha0)) Time.timeScale *= 2f;
        }
    }
}
