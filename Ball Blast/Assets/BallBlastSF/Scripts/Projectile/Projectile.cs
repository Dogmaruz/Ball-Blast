using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed; //�������� �������.
    [SerializeField] private float _lifeTime; //����� ����� �������.
    private int _damage; //���� �����.

    private void Start()
    {
        Destroy(gameObject, _lifeTime); //����������� ������� ����� ������� �����.
    }

    //����������� �������.
    void Update()
    {
        transform.position += transform.up * _speed * Time.deltaTime;
    }

    //������ �������� ��� ������������ � ������. ��������� ����� ����� � ������� ������ �������.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destructible destructible = collision.transform.root.GetComponent<Destructible>();

        if (destructible != null)
        {
            destructible.OnTakeDamage(_damage);
        }

        Destroy(gameObject);
    }

    //������ ����������� �����.
    public void SetDamage(int damage)
    {
        _damage = damage;
    }
}
