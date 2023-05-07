using UnityEngine;
using UnityEngine.Events;

public class LevelState : MonoBehaviour
{
    [SerializeField] private StoneSpawner _spawner;

    [SerializeField] private Cart _cart;

    [SerializeField] private ParticleSystem _cartParticleSystems; //Ссылка на систему частиц турели.

    [SerializeField] private GameObject _storeMenuPanel; //Ссылка на StoreMenu,

    [Space(5)]
    public UnityEvent PassedEvent;

    public UnityEvent DefeatEvent;

    private float _timer;

    private float _timerGodMode; //Таймер окнчания режима бога.

    private bool _chekPassed; //Флаг окончания спавна камней.

    private bool _isGodMode; //Флаг режима бога.

    public bool IsGodMode { get => _isGodMode; set => _isGodMode = value; } //Флаг режима бога.

    private void Awake()
    {
        _cart.CollisionStoneEvent.AddListener(OnCartCollisionStone);

        _spawner.CompletedEvent.AddListener(OnSpawnCompleted);
    }

    //Активирует флаг окончания спавна камней.
    private void OnSpawnCompleted()
    {
        _chekPassed = true;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        //Проверка на окнчание времени действия режима бога.
        if (_isGodMode)
        {
            _timerGodMode += Time.deltaTime;

            if (_timerGodMode > 5)
            {
                _timerGodMode = 0;

                _isGodMode = false;

                _cartParticleSystems.Stop();
            }
        }

        if (_timer > 0.5f)
        {
            if (_chekPassed)
            {
                //Проверяет есть ли камни на сцене.
                if (FindObjectsOfType<Stone>().Length == 0)
                {
                    if (!_storeMenuPanel.activeSelf)
                    {
                        PassedEvent.Invoke();
                    }
                }
            }

            _timer = 0;
        }
    }

    private void OnDestroy()
    {
        _cart.CollisionStoneEvent.RemoveListener(OnCartCollisionStone);

        _spawner.CompletedEvent.RemoveListener(OnSpawnCompleted);
    }

    private void OnCartCollisionStone()
    {
        if (_isGodMode) return; //Если включен режим бога, то коллизия столкновения не происходит.

        DefeatEvent.Invoke();
    }

    public void ResetTimer()
    {
        _timerGodMode = 0;
    }
}
