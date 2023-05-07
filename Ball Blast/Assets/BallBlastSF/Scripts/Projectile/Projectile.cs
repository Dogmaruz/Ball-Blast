using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed; //Скорость снаряда.
    [SerializeField] private float _lifeTime; //Время жизни объекта.
    private int _damage; //Сила урона.

    private void Start()
    {
        Destroy(gameObject, _lifeTime); //Уничтожение объекта через заданое время.
    }

    //Перемещение снаряда.
    void Update()
    {
        transform.position += transform.up * _speed * Time.deltaTime;
    }

    //Расчет коллизии при столкновении с камнем. Уменьшает жизни камня и удаляет объект снаряда.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destructible destructible = collision.transform.root.GetComponent<Destructible>();

        if (destructible != null)
        {
            destructible.OnTakeDamage(_damage);
        }

        Destroy(gameObject);
    }

    //Задает колличество урона.
    public void SetDamage(int damage)
    {
        _damage = damage;
    }
}
