using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool _pauseGame; //���� ��������� ����.
    [SerializeField] private GameObject _pauseMenu; //������ �� UI ����.
    [SerializeField] private GameObject _mainMenu; //������ �� ������� UI ����.
    [SerializeField] private GameObject _progressPanel; //������ �� ������ ���������.
    [SerializeField] private GameObject _LossMenu; //������ �� ������ ���������.
    [SerializeField] private GameObject _levelMenu; //������ �� ������ ������.
    [SerializeField] private GameObject _storeMenu; //������ �� ������ ��������.

    //��������� ������� ������ ESC.
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

    //���������� ���� � �������� ���� �����.
    public void Resume()
    {
        _pauseMenu.SetActive(false);

        Time.timeScale = 1f;

        _pauseGame = false;
    }

    //������ ���� �� �����.
    public void Pause()
    {
        _pauseMenu.SetActive(true);

        Time.timeScale = 0f;

        _pauseGame = true;
    }

    //��������� ������� ����.
    public void MainMenu()
    {
        _pauseMenu.SetActive(false);
        _mainMenu.SetActive(true);
        _progressPanel.SetActive(false);

        Time.timeScale = 0f;

        _pauseGame = false;
    }
}
