using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StoneSpawner : MonoBehaviour
{
    [Header("Spawn")]
    [SerializeField] private Stone _stonePrefab; //Префаб камня.

    [SerializeField] private Transform[] _spawnPoint; //Массив точкек спавна камей.

    [SerializeField] private float _spawnInterval; //Интервал спавна камней.

    [Space(10)]
    [SerializeField] private Turret _turret;

    [Space(4)]
    [Header("Balance")]
    [SerializeField] private int _amountStone; //Колличество камней спавна.

    [SerializeField][Range(0f, 1f)] private float _minHitpointsPercentage; //Процент от максимального колличества камней.

    [SerializeField] private float _maxHitpointsRate; //Повышение уровня сложности прямо пропорционально жизням камня.

    [SerializeField] public UnityEvent CompletedEvent; //Событие завершения генерации спавна.

    private float _timer; //Таймер спавна.

    private int _amountSpawned; //Счетчик созданных камней.
    public int AmountSpawner { get => _amountSpawned; set => _amountSpawned = value; } //Счетчик созданных камней.

    private int _stoneMaxHitpoints; //Максимальное колличество жизней камня.

    private int _stoneMinHitpoints; //Минимальное колличество жизней камня.

    private int _currentLevel; //Текущий уровень.
    public int CurrentLevel { get => _currentLevel; set => _currentLevel = value; } //Текущий уровень.

    private List<Size> _sizes = new List<Size>(); //Массив размеров камней, для вычисления колличества спавна каменй.

    private int _progressCountStone; //Расчет колличества камней на уровне, для отображения полосы прогресса.
    public int ProgressCountStone { get => _progressCountStone; } //Расчет колличества камней на уровне, для отображения полосы прогресса.

    private Color32[] _colors = { new Color32(250, 103, 103, 255),
                                  new Color32(103, 250, 111, 255),
                                  new Color32(250, 168, 250, 255),
                                  new Color32(250, 135, 103, 255),
                                  new Color32(103, 250, 205, 255)};

    private float _zOffset; //Смешение спавна камней по Z

    private void Start()
    {
        _zOffset = 3f;

        Stone.ZOffset = 0;

        CurrentLevel = PlayerPrefs.GetInt("LevelMenu:CurrentLevel", 1);

        int damagePerSecond = (int)((_turret.Damage * _turret.ProjectileAmount) * (1 / _turret.FireRate));

        _maxHitpointsRate += CurrentLevel * 0.02f;

        _stoneMaxHitpoints = (int)(damagePerSecond * _maxHitpointsRate);

        _stoneMinHitpoints = (int)(_stoneMaxHitpoints * _minHitpointsPercentage);

        _timer = _spawnInterval;

        _amountStone += _currentLevel;

        //Расчет колличества камней на уровне.
        for (int i = 0; i < _amountStone; i++)
        {
            int result = 2;

            _sizes.Add((Size)Random.Range(1, 4));

            for (int j = 0; j < (int)(_sizes[i]) * 2; j += 2)
            {

                result = result + j * 2;
            }

            _progressCountStone += result + 1;
        }
    }

    //Генерация спавна камней от времени.
    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _spawnInterval)
        {
            Spawn();

            _timer = 0f;
        }

        if (_amountSpawned == _amountStone)
        {
            enabled = false;

            CompletedEvent.Invoke();
        }
    }

    //Метод спавна камней.
    private void Spawn()
    {
        _zOffset += 0.0001f;

        Stone stone = Instantiate(_stonePrefab, _spawnPoint[Random.Range(0, _spawnPoint.Length)].position, Quaternion.identity);

        stone.StoneViewMaterial.color = _colors[Random.Range(0, _colors.Length)];

        stone.transform.position = new Vector3(stone.transform.position.x, stone.transform.position.y, stone.transform.position.z - _zOffset);

        stone.SetSize(_sizes[_amountSpawned]);
        
        float result = ((float)Random.Range(_stoneMinHitpoints, _stoneMaxHitpoints + 1f) * (int)_sizes[_amountSpawned] / 3f);

        if (result < 1) result = 1f;
       
        stone.MaxHeath = (int)result;

        _amountSpawned++;
    }
}
