using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UILevelProgress : MonoBehaviour
{
    [SerializeField] private StoneSpawner _stoneSpawner;

    [SerializeField] private Text _currentLevelText; //������ �� ��������� ���� ������� ������.
    [SerializeField] private Text _nextLevelText;//������ �� ��������� ���� ���������� ������.
    [SerializeField] private Image _progressBar; //������ �� �������� ���.
    [SerializeField] private UnityEvent _progressBarEvent;

    private float _fillAmountStep; //��� � �������� ����.

    public float FillAmountStep { get => _fillAmountStep; set => _fillAmountStep = value; } //��� � �������� ����.

    private void Start()
    {
        _currentLevelText.text = _stoneSpawner.CurrentLevel.ToString();
        _nextLevelText.text = (_stoneSpawner.CurrentLevel + 1).ToString();
        _progressBar.fillAmount = 0f;

        FillAmountStep = (float)1f / (_stoneSpawner.ProgressCountStone);
    }

    //���������� �������� ���� � ������� �������.
    public void UpdateProgressBar()
    {
        _progressBar.fillAmount += FillAmountStep;
        _progressBarEvent.Invoke();
    }
}
