using UnityEngine;
using UnityEngine.UI;

public class UICoinText : MonoBehaviour
{
    [SerializeField] private Bag _bag;
    [SerializeField] private Text _text;

    private void Start()
    {
        _bag._onPushCoin.AddListener(UpdateCoinText);
    }

    private void OnDestroy()
    {
        _bag._onPushCoin.RemoveListener(UpdateCoinText);
    }

    //Обновляет текстовое поле монет при событии добавления в сумку.
    public void UpdateCoinText()
    {
        _text.text = _bag.GetAmountCoin().ToString();
    }
}
