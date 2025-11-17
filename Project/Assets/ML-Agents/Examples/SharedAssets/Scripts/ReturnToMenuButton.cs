using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenuButton : MonoBehaviour
{
    // メニューシーンの名前をここに入れる
    // 実際のシーン名（たとえば "Menu" や "MainMenu"）に合わせて変更してください
    [SerializeField] private string menuSceneName = "Menu";

    public void OnClickReturnToMenu()
    {
        SceneManager.LoadScene(menuSceneName);
    }
}
