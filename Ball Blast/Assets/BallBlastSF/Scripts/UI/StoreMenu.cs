using UnityEngine;
using UnityEngine.UI;

public class StoreMenu : MonoBehaviour
{
    [SerializeField] private Image _fireRateImage;
    [SerializeField] private Image _projectileAmountImage;
    [SerializeField] private Image _damageImage;

    [SerializeField] private Turret _turret;
    [SerializeField] private Bag _bag;

    [SerializeField] private int _maxFireRate; //������������ ����������������.
    [SerializeField] private int _maxProjectileAmount; //������������ ����������� ��������.
    [SerializeField] private int _maxDamage; //������������ ������� �����.

    [SerializeField] private GameObject _storePanel; //������ ��������.
    [SerializeField] private GameObject _levelMenuPanel; //������ ������.

    private float _fillAmountStepFareRate; //��� � �������� ������ ����������������.
    private float _fillAmountStepProjectile; //��� � �������� ������ ���������.
    private float _fillAmountStepDamage; //��� � �������� ������ �����.

    private void Awake()
    {
        _fireRateImage.fillAmount = PlayerPrefs.GetFloat("StoreMenu:FireRate", 0f);
        _projectileAmountImage.fillAmount = PlayerPrefs.GetFloat("StoreMenu:ProjectileAmount", 0f);
        _damageImage.fillAmount = PlayerPrefs.GetFloat("StoreMenu:Damage", 0f);

        //������ ����� ��� �������� �������
        _fillAmountStepFareRate = (float)1f / (_maxFireRate);
        _fillAmountStepProjectile = (float)1f / (_maxProjectileAmount);
        _fillAmountStepDamage = (float)1f / (_maxDamage);
    }

    //����������� ����������������.
    public void SetFareRate()
    {
        if (_turret.FireRate <= 0.05f) return;

        if (!_bag.PullCoin(2)) return;

        _turret.FireRate -= 0.01f;
        _fireRateImage.fillAmount += _fillAmountStepFareRate;

        SaveStore();
    }

    //����������� ����������� ��������.
    public void SetProjectileAmount()
    {
        if (_turret.ProjectileAmount >= 15) return;

        if (!_bag.PullCoin(5)) return;

        _turret.ProjectileAmount += 1;
        _projectileAmountImage.fillAmount += _fillAmountStepProjectile;

        SaveStore();
    }

    //����������� ����.
    public void SetDamage()
    {
        if (_turret.Damage >= 100) return;

        if (!_bag.PullCoin(3)) return;

        _turret.Damage += 1;
        _damageImage.fillAmount += _fillAmountStepDamage;

        SaveStore();
    }

    //��������� ������ � ����.
    public void SaveStore()
    {
        PlayerPrefs.SetInt("Turret:Damage", _turret.Damage);
        PlayerPrefs.SetInt("Turret:ProjectileAmount", _turret.ProjectileAmount);
        PlayerPrefs.SetFloat("Turret:FireRate", _turret.FireRate);
        PlayerPrefs.SetFloat("StoreMenu:FireRate", _fireRateImage.fillAmount);
        PlayerPrefs.SetFloat("StoreMenu:ProjectileAmount", _projectileAmountImage.fillAmount);
        PlayerPrefs.SetFloat("StoreMenu:Damage", _damageImage.fillAmount);
    }

    //������� � ���������� ����.
    public void LevelMenu()
    {
        _storePanel.SetActive(false);
        _levelMenuPanel.SetActive(true);
        SaveStore();
        Time.timeScale = 0f;
    }
}
