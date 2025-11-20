using UnityEngine;
using UnityEngine.SceneManagement;
using MLAgentsExamples;

public class GoalDetector : MonoBehaviour
{
    public CrawlerAgent agent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("goal"))
        {
            Debug.Log("Goal detected!");

            float clearTime = AdjustTrainingTimescale.ElapsedTime;
            PlayerPrefs.SetFloat("ClearTime", clearTime);
            SceneManager.LoadScene("GoalScene");
        }
    }
}
