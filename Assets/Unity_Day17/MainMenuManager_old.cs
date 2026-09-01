using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager_old : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        Application.Quit();

        Debug.Log("게임 종료");
    }
}
