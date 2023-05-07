using UnityEngine;
using UnityEngine.Events;

public class Destructible : MonoBehaviour
{
    [HideInInspector] public UnityEvent OnDestroyEvent;

    [HideInInspector] public UnityEvent OnDamageEvent;

    public int MaxHeath; //������������ ����������� ������.

    private int _health; //�����.

    private bool _isDestroyed = false; //���� �����������.

    private void Start()
    {
        _health = MaxHeath;

        OnDamageEvent.Invoke();
    }

    //������ ����.
    public void OnTakeDamage(int damage)
    {
        _health -= damage;

        OnDamageEvent?.Invoke();

        if (_health <= 0)
        {
            Kill();
        }
    }

    //�������� ������.
    public void AddHealth()
    {
        _health += 10;

        if (_health >= MaxHeath) _health = 100;

        OnDamageEvent?.Invoke();
    }

    //����� ��������� �����������, � ��������.
    public void Kill()
    {
        if (_isDestroyed) return;

        _health = 0;

        _isDestroyed = true;

        OnDestroyEvent?.Invoke();
    }

    //�������� ����������� ������.
    public int GetHealth()
    {
        return _health;
    }
}
