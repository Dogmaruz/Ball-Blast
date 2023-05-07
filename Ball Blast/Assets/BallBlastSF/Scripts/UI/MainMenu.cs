using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenuPanel; //Ссылка на главное UI меню.
    [SerializeField] private SceneHelper _sceneHelper;
    [SerializeField] private LossMenu _lossMenu; //Ссылка на UI меню проигрыша.
    [SerializeField] private GameObject _lossMenuPanel; //Ссылка на UI меню проигрыша.
    [SerializeField] private GameObject _progressPanel; //Ссылка на панель прогресса.

    public static bool IsActiv = true;

    private void Awake()
    {
        //Убирает шкалу прогресса если главное меню активно.
        if (IsActiv)
        {
            _mainMenuPanel.SetActive(true);
            _progressPanel.SetActive(false);

            Time.timeScale = 0f;
        }
        else
        {
            _mainMenuPanel.SetActive(false);
            _progressPanel.SetActive(true);
        }

        IsActiv = false;
    }
    //Продолжает игру и скрывает меню паузы.
    public void Play()
    {
        _mainMenuPanel.SetActive(false);
        _progressPanel.SetActive(true);

        if (_lossMenu.IsActive)
        {
            _lossMenuPanel.SetActive(true);
        }

        Time.timeScale = 1f;
    }

    //Выход из игры.
    public void Exit()
    {
        _sceneHelper.ExitGame();
    }

    //Сброс игры.
    public void Reset()
    {
        PlayerPrefs.DeleteAll();

        _mainMenuPanel.SetActive(false);
        _progressPanel.SetActive(true);

        Time.timeScale = 1f;

        _sceneHelper.RestartLevel();
    }
}
