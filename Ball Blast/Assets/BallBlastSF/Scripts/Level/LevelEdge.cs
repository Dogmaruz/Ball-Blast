using UnityEngine;

//���� ������ ������.
public enum EdgeType
{
    Bottom,
    Left,
    Right
}

public class LevelEdge : MonoBehaviour
{
    [SerializeField] private EdgeType _type;
    public EdgeType Type => _type; //���������� ��� ������� ������.
}
