using UnityEngine;

public enum Size
{
    Small,
    Normal,
    Big,
    Huge
}

[RequireComponent(typeof(StoneMovement))]
public class Stone : Destructible
{
    [SerializeField] private Size _size; //Размер камня.
    [SerializeField] private float _spawnUpForce; //Время спавна.
    [SerializeField] private SpriteRenderer _stoneViewMaterial; //Материал камня.
    [SerializeField] private int _maxCoinRandom; //Максимальное число рандома монет.
    [SerializeField] private int _maxFreezingRandom; //Максимальное число рандома заморозки.
    [SerializeField] private int _maxGodModeRandom; //Максимальное число рандома режима бога.
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private Freezing _freezingPrefab;
    [SerializeField] private GodMode _godModePrefab;

    private UILevelProgress _levelProgress;
    private StoneMovement _stoneMovement;

    private float _timer; //Таймер заморозки.
    private bool _isFreezing; //Флаг заморозки.
    private static float _zOffset; //Смешение спавна камней по z

    public SpriteRenderer StoneViewMaterial { get => _stoneViewMaterial; set => _stoneViewMaterial = value; }
    public static float ZOffset { get => _zOffset; set => _zOffset = value; }

    void Awake()
    {
        _stoneMovement = GetComponent<StoneMovement>();
        _levelProgress = FindObjectOfType<UILevelProgress>();

        base.OnDestroyEvent.AddListener(OnStoneDestroyed);

        SetSize(_size);
        
    }

    private void Update()
    {
        if (!_isFreezing) return;

        _timer += Time.deltaTime;

        if (_timer > 5f)
        {
            _timer = 0;
            _isFreezing = false;
            _stoneMovement.IsFreezing = true;
        }
    }

    private void OnDestroy()
    {
        base.OnDestroyEvent.RemoveListener(OnStoneDestroyed);
    }

    //Уничтожение камня и спавн новых если размер не Small.
    private void OnStoneDestroyed()
    {
        if (_size != Size.Small)
        {
            SpawnStones();
        }

        _levelProgress.UpdateProgressBar();

        Destroy(gameObject);
    }

    //Спавн двух новых новых камней и по рандому возможность спавна монет, заморозки и режим бога.
    private void SpawnStones()
    {
        int rndCoin = Random.Range(1, _maxCoinRandom);
        if (rndCoin == 2)
        {
            Coin coin = Instantiate(_coinPrefab, transform.position, Quaternion.identity);
            coin.GetComponent<Movement>().AddVertivalVelocity(_spawnUpForce / 2);
        }

        int rndFreezing = Random.Range(1, _maxFreezingRandom);
        if (rndFreezing == 3)
        {
            Freezing freezing = Instantiate(_freezingPrefab, transform.position, Quaternion.identity);
            freezing.GetComponent<Movement>().AddVertivalVelocity(_spawnUpForce / 2);
        }

        int rndGodMode = Random.Range(1, _maxGodModeRandom);
        if (rndGodMode == 5)
        {
            GodMode godmode = Instantiate(_godModePrefab, transform.position, Quaternion.identity);
            godmode.GetComponent<Movement>().AddVertivalVelocity(_spawnUpForce / 2);
        }

        for (int i = 0; i < 2; i++)
        {
            _zOffset += 0.0001f;

            Stone stone = Instantiate(this, transform.position, Quaternion.identity);
            stone.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - _zOffset);
            stone.SetSize(_size - 1);
            stone.MaxHeath = Mathf.Clamp(MaxHeath / 2, 1, MaxHeath / 2);
            stone._stoneMovement.AddVertivalVelocity(_spawnUpForce);
            stone._stoneMovement.SetHorizontalDirection((i % 2 * 2) - 1);
        }
    }

    //Задает размер камня в зависимости от типа.
    private Vector3 GetVectorFromSize(Size size)
    {
        if (size == Size.Huge) return new Vector3(1, 1, 1);
        if (size == Size.Big) return new Vector3(0.75f, 0.75f, 0.75f);
        if (size == Size.Normal) return new Vector3(0.6f, 0.6f, 0.6f);
        if (size == Size.Small) return new Vector3(0.4f, 0.4f, 0.4f);

        return Vector3.one;
    }

    //Задает размер.
    public void SetSize(Size size)
    {
        if (size < 0) return;

        transform.localScale = GetVectorFromSize(size);
        _size = size;
    }

    //Получить размер.
    public Size GetSize()
    {
        return _size;
    }

    //Метод заморозки камня.
    public void FreezingStone()
    {
        _timer = 0;
        _isFreezing = true;

        _stoneMovement.IsFreezing = false;
    }
}
