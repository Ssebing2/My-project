using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void QultGame()
    {
        Application.Quit();

        Debug.Log("게임 종료");
    }
}
