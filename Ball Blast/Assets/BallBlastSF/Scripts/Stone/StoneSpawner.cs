using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StoneSpawner : MonoBehaviour
{
    [Header("Spawn")]
    [SerializeField] private Stone _stonePrefab; //������ �����.

    [SerializeField] private Transform[] _spawnPoint; //������ ������ ������ �����.

    [SerializeField] private float _spawnInterval; //�������� ������ ������.

    [Space(10)]
    [SerializeField] private Turret _turret;

    [Space(4)]
    [Header("Balance")]
    [SerializeField] private int _amountStone; //����������� ������ ������.

    [SerializeField][Range(0f, 1f)] private float _minHitpointsPercentage; //������� �� ������������� ����������� ������.

    [SerializeField] private float _maxHitpointsRate; //��������� ������ ��������� ����� ��������������� ������ �����.

    [SerializeField] public UnityEvent CompletedEvent; //������� ���������� ��������� ������.

    private float _timer; //������ ������.

    private int _amountSpawned; //������� ��������� ������.
    public int AmountSpawner { get => _amountSpawned; set => _amountSpawned = value; } //������� ��������� ������.

    private int _stoneMaxHitpoints; //������������ ����������� ������ �����.

    private int _stoneMinHitpoints; //����������� ����������� ������ �����.

    private int _currentLevel; //������� �������.
    public int CurrentLevel { get => _currentLevel; set => _currentLevel = value; } //������� �������.

    private List<Size> _sizes = new List<Size>(); //������ �������� ������, ��� ���������� ����������� ������ ������.

    private int _progressCountStone; //������ ����������� ������ �� ������, ��� ����������� ������ ���������.
    public int ProgressCountStone { get => _progressCountStone; } //������ ����������� ������ �� ������, ��� ����������� ������ ���������.

    private Color32[] _colors = { new Color32(250, 103, 103, 255),
                                  new Color32(103, 250, 111, 255),
                                  new Color32(250, 168, 250, 255),
                                  new Color32(250, 135, 103, 255),
                                  new Color32(103, 250, 205, 255)};

    private float _zOffset; //�������� ������ ������ �� Z

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

        //������ ����������� ������ �� ������.
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

    //��������� ������ ������ �� �������.
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

    //����� ������ ������.
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
