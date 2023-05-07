using System;
using UnityEngine;
using UnityEngine.Events;

public class Turret : MonoBehaviour
{
    [SerializeField] private Projectile _projectilePrefab; //������ �������.

    [SerializeField] private Transform _shotPoint; //����� ������ �������.

    [Space(10)]
    [Header("Turret Settings")]
    [SerializeField] private float _fireRate; //������� ��������.
    public float FireRate { get => _fireRate; set => _fireRate = value; } //������� ��������.

    [SerializeField] private int _damage; //������� �����
    public int Damage { get => _damage; set => _damage = value; } //������� �����.

    [SerializeField] private int _projectileAmount; //����������� ��������.
    public int ProjectileAmount { get => _projectileAmount; set => _projectileAmount = value; } //����������� ��������.

    [Space(10)]
    [Header("Projectile Settings")]
    [SerializeField] private GameObject _flash; //���� �� �������.

    [SerializeField] private float _projectileInterval; //�������� ��������� ��������.

    [SerializeField] private float _angleProjectile; //���� ������� ��������.

    [SerializeField] private float _stepY; //�������� �������� ���� ������������ �������.

    [SerializeField] private GameObject _bulletForward; //������ ������ �������� ������.

    [SerializeField] private GameObject _bulletSidelong; //������ ������ �������� ��� �����.

    [SerializeField] private UnityEvent _fireEvent; //������� ��������.

    private float _timer;

    private bool _isFanShot; //����������� � ������ �� ������� ��������.
    public bool IsFanShot { get => _isFanShot; set => _isFanShot = value; } //����������� � ������ �� ������� ��������.

    private void Awake()
    {
        _damage = PlayerPrefs.GetInt("Turret:Damage", 1);

        _projectileAmount = PlayerPrefs.GetInt("Turret:ProjectileAmount", 1);

        _fireRate = PlayerPrefs.GetFloat("Turret:FireRate", 0.2f);
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        //������ ������� � ����������� �� ���� ��������.
        if (IsFanShot)
        {
            _bulletForward.SetActive(false);

            _bulletSidelong.SetActive(true);
        }
        else
        {
            _bulletForward.SetActive(true);

            _bulletSidelong.SetActive(false);
        }
    }

    //����� ������� ���� � ���������� �� IsFanShot (���� �������� - ����� ��� � �������)
    private void SpawnProjectile()
    {
        if (_projectileAmount % 2 != 0)
        {
            Projectile projectile = Instantiate(_projectilePrefab, _shotPoint.position, transform.rotation);

            projectile.SetDamage(_damage);
        }

        int countInterval = _projectileAmount / 2 * 2;

        int _index = 0;

        int _indexAngle = 0;

        for (int i = 0; i < countInterval; i++)
        {
            if (i % 2 == 0)
            {
                _index++;
            }

            if (IsFanShot)
            {
                _indexAngle = _index;
            }

            Projectile projectile = Instantiate(_projectilePrefab,
                                                new Vector3(_shotPoint.position.x - _index * _projectileInterval, _shotPoint.position.y - Math.Abs(_index) * _stepY, _shotPoint.position.z),
                                                Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + _indexAngle * _angleProjectile));
            projectile.SetDamage(_damage);

            _index = -_index;
        }

        _flash.SetActive(true);

        Invoke("HideFlash", 0.12f);
    }

    //���������� ������� � ����������� �� ��������� �������.
    public void Fire()
    {
        if (_timer >= _fireRate)
        {
            SpawnProjectile();

            _fireEvent?.Invoke();

            _timer = 0;
        }
    }

    //�������� ������ �������.
    private void HideFlash()
    {
        _flash.SetActive(false);
    }

    //������ ����������� ���������.
    public void SetProjectileAmount(int amount)
    {
        _projectileAmount = amount;
    }
}
