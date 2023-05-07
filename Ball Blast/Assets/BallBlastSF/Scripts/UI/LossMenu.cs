using UnityEngine;

public class LossMenu : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu; //Ссылка на главное UI меню.
    [SerializeField] private GameObject _lossMenu; //Ссылка на главное UI меню.
    [SerializeField] private StoneSpawner _stoneSpawner;
    [SerializeField] private SceneHelper _sceneHelper;
    [SerializeField] private GameObject _progressPanel; //Ссылка на панель прогресса.

    private bool _isActive;

    public bool IsActive { get => _isActive; set => _isActive = value; }

    //Рестарт игры и скрывает меню паузы.
    public void Restart()
    {
        _lossMenu.SetActive(false);

        Time.timeScale = 1f;

        _stoneSpawner.AmountSpawner = 0;

        IsActive = false;

        _sceneHelper.RestartLevel();
    }

    //Загружает главное меню.
    public void MainMenu()
    {
        IsActive = true;

        _lossMenu.SetActive(false);
        _mainMenu.SetActive(true);
        _progressPanel.SetActive(false);

        Time.timeScale = 0f;
    }
}
