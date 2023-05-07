using UnityEngine;
using UnityEngine.Events;

public class Destructible : MonoBehaviour
{
    [HideInInspector] public UnityEvent OnDestroyEvent;

    [HideInInspector] public UnityEvent OnDamageEvent;

    public int MaxHeath; //Максимальное колличество жизней.

    private int _health; //Жизнь.

    private bool _isDestroyed = false; //Флаг уничтожения.

    private void Start()
    {
        _health = MaxHeath;

        OnDamageEvent.Invoke();
    }

    //Задает урон.
    public void OnTakeDamage(int damage)
    {
        _health -= damage;

        OnDamageEvent?.Invoke();

        if (_health <= 0)
        {
            Kill();
        }
    }

    //Добавить жизней.
    public void AddHealth()
    {
        _health += 10;

        if (_health >= MaxHeath) _health = 100;

        OnDamageEvent?.Invoke();
    }

    //Метод обработки уничтожения, с событием.
    public void Kill()
    {
        if (_isDestroyed) return;

        _health = 0;

        _isDestroyed = true;

        OnDestroyEvent?.Invoke();
    }

    //Получить колличество жизней.
    public int GetHealth()
    {
        return _health;
    }
}
