using UnityEngine;
using UnityEngine.Events;

public class Bag : MonoBehaviour
{
    [SerializeField] private UICoinText _textCoinUI; //������ �� ��������� ���� �����.

    private int _amountCoin; //������� �����.

    public UnityEvent _onPushCoin;

    private void Awake()
    {
        //��������� ������ �� ����� � ������� �� �����.
        _amountCoin = PlayerPrefs.GetInt("LevelMenu:AmountCoin", 0);
        _textCoinUI.UpdateCoinText();
    }

    //�������� ������.
    public void PushCoin(int amount)
    {
        _amountCoin += amount;

        _onPushCoin?.Invoke();
    }

    //�������� ����������� �����.
    public int GetAmountCoin()
    {
        return _amountCoin;
    }

    //����� ������.
    public bool PullCoin(int amount)
    {
        if (_amountCoin - amount < 0) return false;

        _amountCoin -= amount;
        _onPushCoin?.Invoke();

        return true;
    }
}
