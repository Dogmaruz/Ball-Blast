using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHelper : MonoBehaviour
{
    void Awake()
    {
        Cursor.lockState = CursorLockMode.None;

        Cursor.visible = true;
    }

    //Перезагрузка уровня.
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //Загрузка уровня.
    public void LoadLevel(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }

    //Выход из игры.
    public void ExitGame()
    {
        Application.Quit();
    }
}
