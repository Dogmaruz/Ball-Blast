using UnityEngine;

public class CartInputControl : MonoBehaviour
{
    [SerializeField] private Cart _cartPrefab; //������ �������.

    [SerializeField] private Turret _turret;
    
    void Update()
    {
        //����������� ������� �� ������� ����.
        _cartPrefab.MovementSetTarget( Camera.main.ScreenToWorldPoint(Input.mousePosition));

        //���� ������ ������� ���� - �������.
        if (Input.GetMouseButton(0))
        {
            _turret.Fire();
        }

        //����������� ��� �������� � ������ �� �������.
        if (Input.GetKeyDown(KeyCode.F))
        {
            _turret.IsFanShot = !_turret.IsFanShot;
        }
    }
}
