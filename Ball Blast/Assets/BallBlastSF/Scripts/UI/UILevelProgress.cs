using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UILevelProgress : MonoBehaviour
{
    [SerializeField] private StoneSpawner _stoneSpawner;

    [SerializeField] private Text _currentLevelText; //—сылка на текстовое поле текщего уровн€.
    [SerializeField] private Text _nextLevelText;//—сылка на текстовое поле следующего уровн€.
    [SerializeField] private Image _progressBar; //—сылка на прогресс бар.
    [SerializeField] private UnityEvent _progressBarEvent;

    private float _fillAmountStep; //Ўаг в прогресс баре.

    public float FillAmountStep { get => _fillAmountStep; set => _fillAmountStep = value; } //Ўаг в прогресс баре.

    private void Start()
    {
        _currentLevelText.text = _stoneSpawner.CurrentLevel.ToString();
        _nextLevelText.text = (_stoneSpawner.CurrentLevel + 1).ToString();
        _progressBar.fillAmount = 0f;

        FillAmountStep = (float)1f / (_stoneSpawner.ProgressCountStone);
    }

    //ќбновление прогресс бара с вызовом событи€.
    public void UpdateProgressBar()
    {
        _progressBar.fillAmount += FillAmountStep;
        _progressBarEvent.Invoke();
    }
}
