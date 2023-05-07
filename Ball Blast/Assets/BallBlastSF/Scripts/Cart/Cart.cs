using UnityEngine;
using UnityEngine.Events;

public class Cart : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _movementSpeed; //Скорость турели.
    [SerializeField] private float _vehicleWidht;

    [Header("Wheel")]
    [SerializeField] private Transform[] _wheels; //Ссылка на колеса.
    [SerializeField] private float _wheelRadius; //Радиус колеса.

    [HideInInspector] public UnityEvent CollisionStoneEvent;

    private Vector3 _movementTarget; //Цель движения.
    private float _deltaMovement;
    private float _lastPositionX; //Предыдущая позиция.

    void Start()
    {
        _movementTarget = transform.position;
    }

    void Update()
    {
        Move();

        RotateWheel();
    }

    //Расчет столкновения турели с камнем.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Stone stone = collision.transform.root.GetComponent<Stone>();

        if (stone)
        {
            CollisionStoneEvent?.Invoke();
        }
    }

    //Расчет движения турели.
    private void Move()
    {
        _lastPositionX = transform.position.x;

        transform.position = Vector3.MoveTowards(transform.position, _movementTarget, _movementSpeed * Time.deltaTime);

        _deltaMovement = transform.position.x - _lastPositionX;
    }

    //Расчет вращения колес.
    private void RotateWheel()
    {
        float angle = (180 * _deltaMovement) / (Mathf.PI * _wheelRadius * 2);

        for (int i = 0; i < _wheels.Length; i++)
        {
            _wheels[i].Rotate(0, 0, -angle);
        }
    }

    //Задает цель движения.
    public void MovementSetTarget(Vector3 target)
    {
        _movementTarget = ClampMovementTarget(target);
    }

    //Задает цель движения в зависимости от ограничений экрана.
    private Vector3 ClampMovementTarget(Vector3 target)
    {
        float leftBorder = LevelBoundary.Instance.LeftBorder + _vehicleWidht * 0.5f;
        float rightBorder = LevelBoundary.Instance.RightBorder - _vehicleWidht * 0.5f;

        Vector3 moveTarget = target;

        moveTarget.y = transform.position.y;
        moveTarget.z = transform.position.z;

        if (moveTarget.x < leftBorder) moveTarget.x = leftBorder;
        if (moveTarget.x > rightBorder) moveTarget.x = rightBorder;
        
        return moveTarget;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position + new Vector3(-_vehicleWidht * 0.5f, -0.55f, 0), transform.position + new Vector3(_vehicleWidht * 0.5f, -0.55f, 0));
    }
#endif
}
