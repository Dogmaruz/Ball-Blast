using UnityEngine;
using UnityEngine.Events;

public class Bag : MonoBehaviour
{
    [SerializeField] private UICoinText _textCoinUI; //Ссылка на текстовое поле монет.

    private int _amountCoin; //Счетчик монет.

    public UnityEvent _onPushCoin;

    private void Awake()
    {
        //Загружает монеты из файла и выводит на экран.
        _amountCoin = PlayerPrefs.GetInt("LevelMenu:AmountCoin", 0);
        _textCoinUI.UpdateCoinText();
    }

    //Положить монету.
    public void PushCoin(int amount)
    {
        _amountCoin += amount;

        _onPushCoin?.Invoke();
    }

    //Получить колличество монет.
    public int GetAmountCoin()
    {
        return _amountCoin;
    }

    //Взять монету.
    public bool PullCoin(int amount)
    {
        if (_amountCoin - amount < 0) return false;

        _amountCoin -= amount;
        _onPushCoin?.Invoke();

        return true;
    }
}
