using UnityEngine;
using UnityEngine.UI;

public class StoreMenu : MonoBehaviour
{
    [SerializeField] private Image _fireRateImage;
    [SerializeField] private Image _projectileAmountImage;
    [SerializeField] private Image _damageImage;

    [SerializeField] private Turret _turret;
    [SerializeField] private Bag _bag;

    [SerializeField] private int _maxFireRate; //Максимальная скорострельность.
    [SerializeField] private int _maxProjectileAmount; //Максимальное колличество патронов.
    [SerializeField] private int _maxDamage; //Максимальный уровень урона.

    [SerializeField] private GameObject _storePanel; //Панель магазина.
    [SerializeField] private GameObject _levelMenuPanel; //Панель уровня.

    private float _fillAmountStepFareRate; //Шаг в прогресс панели скорострельности.
    private float _fillAmountStepProjectile; //Шаг в прогресс панели выстрелов.
    private float _fillAmountStepDamage; //Шаг в прогресс панели урона.

    private void Awake()
    {
        _fireRateImage.fillAmount = PlayerPrefs.GetFloat("StoreMenu:FireRate", 0f);
        _projectileAmountImage.fillAmount = PlayerPrefs.GetFloat("StoreMenu:ProjectileAmount", 0f);
        _damageImage.fillAmount = PlayerPrefs.GetFloat("StoreMenu:Damage", 0f);

        //Расчет шагов для прогресс панелей
        _fillAmountStepFareRate = (float)1f / (_maxFireRate);
        _fillAmountStepProjectile = (float)1f / (_maxProjectileAmount);
        _fillAmountStepDamage = (float)1f / (_maxDamage);
    }

    //Увеличивает скорострельность.
    public void SetFareRate()
    {
        if (_turret.FireRate <= 0.05f) return;

        if (!_bag.PullCoin(2)) return;

        _turret.FireRate -= 0.01f;
        _fireRateImage.fillAmount += _fillAmountStepFareRate;

        SaveStore();
    }

    //Увеличивает колличество патронов.
    public void SetProjectileAmount()
    {
        if (_turret.ProjectileAmount >= 15) return;

        if (!_bag.PullCoin(5)) return;

        _turret.ProjectileAmount += 1;
        _projectileAmountImage.fillAmount += _fillAmountStepProjectile;

        SaveStore();
    }

    //Увеличивает урон.
    public void SetDamage()
    {
        if (_turret.Damage >= 100) return;

        if (!_bag.PullCoin(3)) return;

        _turret.Damage += 1;
        _damageImage.fillAmount += _fillAmountStepDamage;

        SaveStore();
    }

    //Сохраняет данные в файл.
    public void SaveStore()
    {
        PlayerPrefs.SetInt("Turret:Damage", _turret.Damage);
        PlayerPrefs.SetInt("Turret:ProjectileAmount", _turret.ProjectileAmount);
        PlayerPrefs.SetFloat("Turret:FireRate", _turret.FireRate);
        PlayerPrefs.SetFloat("StoreMenu:FireRate", _fireRateImage.fillAmount);
        PlayerPrefs.SetFloat("StoreMenu:ProjectileAmount", _projectileAmountImage.fillAmount);
        PlayerPrefs.SetFloat("StoreMenu:Damage", _damageImage.fillAmount);
    }

    //Возврат в предыдущее меню.
    public void LevelMenu()
    {
        _storePanel.SetActive(false);
        _levelMenuPanel.SetActive(true);
        SaveStore();
        Time.timeScale = 0f;
    }
}
