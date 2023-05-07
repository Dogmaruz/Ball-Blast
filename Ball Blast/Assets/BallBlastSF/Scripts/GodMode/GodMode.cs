using UnityEngine;

public class GodMode : MonoBehaviour
{
    private LevelState _levelState;

    private ParticleSystem _cartParticleSystems; //������ �� ParticleSystem ������.

    private void Awake()
    {
        //������� ������� �� ����.
        _levelState = FindObjectOfType<LevelState>();

        _cartParticleSystems = FindObjectOfType<CartParticleSystem>().GetComponent<ParticleSystem>();
    }

    //�������� ���� ���� ��� ������������ � �������.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Turret turret = collision.transform.root.GetComponent<Turret>();

        if (turret)
        {
            _levelState.IsGodMode = true;

            _levelState.ResetTimer(); //����� ������� ��� ��������� �������� ������ ����.

            _cartParticleSystems.Play();

            Destroy(gameObject);
        }
    }
}
