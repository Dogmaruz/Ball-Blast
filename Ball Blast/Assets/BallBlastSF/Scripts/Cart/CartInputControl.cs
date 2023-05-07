using UnityEngine;

public class CartInputControl : MonoBehaviour
{
    [SerializeField] private Cart _cartPrefab; //Префаб турелли.

    [SerializeField] private Turret _turret;
    
    void Update()
    {
        //Перемещение турелли от позиции мыши.
        _cartPrefab.MovementSetTarget( Camera.main.ScreenToWorldPoint(Input.mousePosition));

        //Если нажата клавиша мыши - выстрел.
        if (Input.GetMouseButton(0))
        {
            _turret.Fire();
        }

        //Переключает тип стрельбы с прямой на угловой.
        if (Input.GetKeyDown(KeyCode.F))
        {
            _turret.IsFanShot = !_turret.IsFanShot;
        }
    }
}
