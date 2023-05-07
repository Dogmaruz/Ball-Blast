using System;
using UnityEngine;
using UnityEngine.Events;

public class StoneMovement : MonoBehaviour
{
    [SerializeField] private float _horizontalSpeed; //Горизонтальная скорость.

    [SerializeField] private float _reboundSpeed; //Скорость отскока.

    [SerializeField] private float _gravity; //Гравитация.

    [SerializeField] private float _gravityOffset; //Величина смещения от границы экрана для включения гравитации.

    [SerializeField] private float _rotationSpeed; //Скорость вращения.

    [SerializeField] private UnityEvent _stoneBottomEdgeEvent; //Событие удара об землю.

    private bool _useGravity; //Флаг гравитации.

    private Vector3 _velosity; //Вектор направления движения камня.

    private bool _isFreezing; //Флаг заморозки.
    public bool IsFreezing { get => _isFreezing; set => _isFreezing = value; } //Флаг заморозки.

    private void Awake()
    {
        //Задает направление движения при создании объекта камня.
        _velosity.x = -Mathf.Sign(transform.position.x) * _horizontalSpeed;

        _isFreezing = true; ;
    }

    private void Update()
    {
        if (!_isFreezing) return; //При заморозке движение камня не происходит.

        TryEnableGravity();

        Move();
    }

    //Включает гравитацию в зависимости от координат.
    private void TryEnableGravity()
    {
        if (Math.Abs(transform.position.x) <= Math.Abs(LevelBoundary.Instance.LeftBorder) - _gravityOffset)
        {
            _useGravity = true;
        }
    }

    //Расчет движения камня.
    private void Move()
    {
        if (_useGravity)
        {
            _velosity.y -= _gravity * Time.deltaTime;
        }

        transform.Rotate(0, 0, _rotationSpeed * -Mathf.Sign(_velosity.x) * Time.deltaTime);

        _velosity.x = Mathf.Sign(_velosity.x) * _horizontalSpeed;

        transform.position += _velosity * Time.deltaTime;
    }

    //Расчет поведения траектории камня от столкновений с границами экрана.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        LevelEdge levelEdge = collision.GetComponent<LevelEdge>();

        if (levelEdge != null)
        {
            if (levelEdge.Type == EdgeType.Bottom)
            {
                _velosity.y = _reboundSpeed;

                if (GetComponent<Stone>().GetSize() == Size.Huge)
                {
                    _stoneBottomEdgeEvent?.Invoke();

                }
            }

            if (levelEdge.Type == EdgeType.Left && _velosity.x < 0 || levelEdge.Type == EdgeType.Right && _velosity.x > 0)
            {
                _velosity.x *= -1;
            }
        }
    }

    //Добавляет вертикальную силу вверх при делении камней.
    public void AddVertivalVelocity(float velosity)
    {
        _velosity.y += velosity;
    }

    //Задает направление движения при делении на два камня.
    public void SetHorizontalDirection(float direction)
    {
        _velosity.x = Mathf.Sign(direction) * _horizontalSpeed;
    }
}
