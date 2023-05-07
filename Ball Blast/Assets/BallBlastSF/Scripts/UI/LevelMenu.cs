using UnityEngine;

public class LevelMenu : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu; //Ссылка на главное UI меню.
    [SerializeField] private GameObject _levelMenu; //Ссылка на главное UI меню.
    [SerializeField] private StoneSpawner _stoneSpawner;
    [SerializeField] private Bag _bag;
    [SerializeField] private Turret _turret;
    [SerializeField] private SceneHelper _sceneHelper;
    [SerializeField] private GameObject _progressPanel; //Ссылка на панель прогресса.
    [SerializeField] private GameObject _storePanel; //Ссылка на панель магазина.

    //Продолжает игру и скрывает меню паузы.
    public void NextLevel()
    {
        _levelMenu.SetActive(false);
        Time.timeScale = 1f;
        _stoneSpawner.CurrentLevel++;
        _stoneSpawner.AmountSpawner = 0;
        PlayerPrefs.SetInt("LevelMenu:CurrentLevel", _stoneSpawner.CurrentLevel);
        PlayerPrefs.SetInt("LevelMenu:AmountCoin", _bag.GetAmountCoin());

        _sceneHelper.RestartLevel();
    }

    //Загружает главное меню.
    public void MainMenu()
    {
        _levelMenu.SetActive(false);
        _mainMenu.SetActive(true);
        _progressPanel.SetActive(false);
        Time.timeScale = 0f;
    }

    //Загружает меню магазина.
    public void StoreMenu()
    {
        _levelMenu.SetActive(false);
        _storePanel.SetActive(true);
        Time.timeScale = 1f;
    }
}
