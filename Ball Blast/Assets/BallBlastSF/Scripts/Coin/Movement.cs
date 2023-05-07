using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _horizontalSpeed; //Горизонтальная скорость.

    [SerializeField] private float _reboundSpeed; //Скорость отскока.

    [SerializeField] private float _gravity; //Гравитация.

    private Vector3 _velosity; //Вектор направления движения.

    private void Awake()
    {
        //Задает направление движения при создании объекта.
        _velosity.x = -Mathf.Sign(transform.position.x) * _horizontalSpeed;
    }

    private void Update()
    {
        Move();
    }

    //Расчет движения.
    private void Move()
    {
        _velosity.y -= _gravity * Time.deltaTime;

        _velosity.x = Mathf.Sign(_velosity.x) * _horizontalSpeed;

        transform.position += _velosity * Time.deltaTime;
    }

    //Расчет поведения траектории от столкновений с границами экрана.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        LevelEdge levelEdge = collision.GetComponent<LevelEdge>();

        if (_reboundSpeed <= 0.5f)
        {
            enabled = false;

            return;
        }

        if (levelEdge != null)
        {
            if (levelEdge.Type == EdgeType.Bottom)
            {
                _velosity.y = _reboundSpeed;

                _reboundSpeed /= 1.5f;

                _velosity.x /= 2;
            }

            if (levelEdge.Type == EdgeType.Left && _velosity.x < 0 || levelEdge.Type == EdgeType.Right && _velosity.x > 0)
            {
                _velosity.x *= -1;
            }
        }
    }

    //Добавляет вертикальную силу вверх.
    public void AddVertivalVelocity(float velosity)
    {
        _velosity.y += velosity;
    }
}
