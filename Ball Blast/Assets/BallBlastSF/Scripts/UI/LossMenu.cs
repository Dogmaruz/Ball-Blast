using UnityEngine;

public class LossMenu : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu; //������ �� ������� UI ����.
    [SerializeField] private GameObject _lossMenu; //������ �� ������� UI ����.
    [SerializeField] private StoneSpawner _stoneSpawner;
    [SerializeField] private SceneHelper _sceneHelper;
    [SerializeField] private GameObject _progressPanel; //������ �� ������ ���������.

    private bool _isActive;

    public bool IsActive { get => _isActive; set => _isActive = value; }

    //������� ���� � �������� ���� �����.
    public void Restart()
    {
        _lossMenu.SetActive(false);

        Time.timeScale = 1f;

        _stoneSpawner.AmountSpawner = 0;

        IsActive = false;

        _sceneHelper.RestartLevel();
    }

    //��������� ������� ����.
    public void MainMenu()
    {
        IsActive = true;

        _lossMenu.SetActive(false);
        _mainMenu.SetActive(true);
        _progressPanel.SetActive(false);

        Time.timeScale = 0f;
    }
}
