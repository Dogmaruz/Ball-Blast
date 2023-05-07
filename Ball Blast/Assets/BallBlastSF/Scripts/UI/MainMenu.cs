using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenuPanel; //������ �� ������� UI ����.
    [SerializeField] private SceneHelper _sceneHelper;
    [SerializeField] private LossMenu _lossMenu; //������ �� UI ���� ���������.
    [SerializeField] private GameObject _lossMenuPanel; //������ �� UI ���� ���������.
    [SerializeField] private GameObject _progressPanel; //������ �� ������ ���������.

    public static bool IsActiv = true;

    private void Awake()
    {
        //������� ����� ��������� ���� ������� ���� �������.
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
    //���������� ���� � �������� ���� �����.
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

    //����� �� ����.
    public void Exit()
    {
        _sceneHelper.ExitGame();
    }

    //����� ����.
    public void Reset()
    {
        PlayerPrefs.DeleteAll();

        _mainMenuPanel.SetActive(false);
        _progressPanel.SetActive(true);

        Time.timeScale = 1f;

        _sceneHelper.RestartLevel();
    }
}
