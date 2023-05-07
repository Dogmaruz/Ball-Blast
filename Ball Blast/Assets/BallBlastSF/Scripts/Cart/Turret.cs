using System;
using UnityEngine;
using UnityEngine.Events;

public class Turret : MonoBehaviour
{
    [SerializeField] private Projectile _projectilePrefab; //Префаб снаряда.

    [SerializeField] private Transform _shotPoint; //Точка спавна снаряда.

    [Space(10)]
    [Header("Turret Settings")]
    [SerializeField] private float _fireRate; //Частота выстрела.
    public float FireRate { get => _fireRate; set => _fireRate = value; } //Частота выстрела.

    [SerializeField] private int _damage; //Уровень урона
    public int Damage { get => _damage; set => _damage = value; } //Уровень урона.

    [SerializeField] private int _projectileAmount; //Колличество снарядов.
    public int ProjectileAmount { get => _projectileAmount; set => _projectileAmount = value; } //Колличество снарядов.

    [Space(10)]
    [Header("Projectile Settings")]
    [SerializeField] private GameObject _flash; //След от снаряда.

    [SerializeField] private float _projectileInterval; //Интервал отрисовки снарядов.

    [SerializeField] private float _angleProjectile; //Угол наклона снарядов.

    [SerializeField] private float _stepY; //Смещение снарядов вниз относительно турелли.

    [SerializeField] private GameObject _bulletForward; //Спрайт режима выстрела вперед.

    [SerializeField] private GameObject _bulletSidelong; //Спрайт режима выстрела под углом.

    [SerializeField] private UnityEvent _fireEvent; //Событие выстрела.

    private float _timer;

    private bool _isFanShot; //Переключает с прямой на веерную стрельбу.
    public bool IsFanShot { get => _isFanShot; set => _isFanShot = value; } //Переключает с прямой на веерную стрельбу.

    private void Awake()
    {
        _damage = PlayerPrefs.GetInt("Turret:Damage", 1);

        _projectileAmount = PlayerPrefs.GetInt("Turret:ProjectileAmount", 1);

        _fireRate = PlayerPrefs.GetFloat("Turret:FireRate", 0.2f);
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        //Меняет спрайты в зависимости от типа стрельбы.
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

    //Метод содания пуль в зависимоти от IsFanShot (типа стрельбы - прямо или в стороны)
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

    //Производит выстрел в зависимости от интервала времени.
    public void Fire()
    {
        if (_timer >= _fireRate)
        {
            SpawnProjectile();

            _fireEvent?.Invoke();

            _timer = 0;
        }
    }

    //Скрывает впышку пламени.
    private void HideFlash()
    {
        _flash.SetActive(false);
    }

    //Задает колличество выстрелов.
    public void SetProjectileAmount(int amount)
    {
        _projectileAmount = amount;
    }
}
