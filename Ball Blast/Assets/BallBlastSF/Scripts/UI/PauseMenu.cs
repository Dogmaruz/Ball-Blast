using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool _pauseGame; //Флаг активации меню.
    [SerializeField] private GameObject _pauseMenu; //Ссылка на UI меню.
    [SerializeField] private GameObject _mainMenu; //Ссылка на главное UI меню.
    [SerializeField] private GameObject _progressPanel; //Ссылка на панель прогресса.
    [SerializeField] private GameObject _LossMenu; //Ссылка на панель проигрыша.
    [SerializeField] private GameObject _levelMenu; //Ссылка на панель победы.
    [SerializeField] private GameObject _storeMenu; //Ссылка на панель магазина.

    //Обработка нажатия кнопки ESC.
    void Update()
    {
        if (_LossMenu.activeSelf || _levelMenu.activeSelf || _mainMenu.activeSelf || _storeMenu.activeSelf) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_pauseGame)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    //Продолжает игру и скрывает меню паузы.
    public void Resume()
    {
        _pauseMenu.SetActive(false);

        Time.timeScale = 1f;

        _pauseGame = false;
    }

    //Ставит игру на паузу.
    public void Pause()
    {
        _pauseMenu.SetActive(true);

        Time.timeScale = 0f;

        _pauseGame = true;
    }

    //Загружает главное меню.
    public void MainMenu()
    {
        _pauseMenu.SetActive(false);
        _mainMenu.SetActive(true);
        _progressPanel.SetActive(false);

        Time.timeScale = 0f;

        _pauseGame = false;
    }
}
