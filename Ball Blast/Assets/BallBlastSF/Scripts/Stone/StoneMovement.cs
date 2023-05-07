using System;
using UnityEngine;
using UnityEngine.Events;

public class StoneMovement : MonoBehaviour
{
    [SerializeField] private float _horizontalSpeed; //�������������� ��������.

    [SerializeField] private float _reboundSpeed; //�������� �������.

    [SerializeField] private float _gravity; //����������.

    [SerializeField] private float _gravityOffset; //�������� �������� �� ������� ������ ��� ��������� ����������.

    [SerializeField] private float _rotationSpeed; //�������� ��������.

    [SerializeField] private UnityEvent _stoneBottomEdgeEvent; //������� ����� �� �����.

    private bool _useGravity; //���� ����������.

    private Vector3 _velosity; //������ ����������� �������� �����.

    private bool _isFreezing; //���� ���������.
    public bool IsFreezing { get => _isFreezing; set => _isFreezing = value; } //���� ���������.

    private void Awake()
    {
        //������ ����������� �������� ��� �������� ������� �����.
        _velosity.x = -Mathf.Sign(transform.position.x) * _horizontalSpeed;

        _isFreezing = true; ;
    }

    private void Update()
    {
        if (!_isFreezing) return; //��� ��������� �������� ����� �� ����������.

        TryEnableGravity();

        Move();
    }

    //�������� ���������� � ����������� �� ���������.
    private void TryEnableGravity()
    {
        if (Math.Abs(transform.position.x) <= Math.Abs(LevelBoundary.Instance.LeftBorder) - _gravityOffset)
        {
            _useGravity = true;
        }
    }

    //������ �������� �����.
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

    //������ ��������� ���������� ����� �� ������������ � ��������� ������.
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

    //��������� ������������ ���� ����� ��� ������� ������.
    public void AddVertivalVelocity(float velosity)
    {
        _velosity.y += velosity;
    }

    //������ ����������� �������� ��� ������� �� ��� �����.
    public void SetHorizontalDirection(float direction)
    {
        _velosity.x = Mathf.Sign(direction) * _horizontalSpeed;
    }
}
