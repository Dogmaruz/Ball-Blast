using UnityEngine;

public class GodMode : MonoBehaviour
{
    private LevelState _levelState;

    private ParticleSystem _cartParticleSystems; //Ссылка на ParticleSystem турели.

    private void Awake()
    {
        //Находим объекты по типу.
        _levelState = FindObjectOfType<LevelState>();

        _cartParticleSystems = FindObjectOfType<CartParticleSystem>().GetComponent<ParticleSystem>();
    }

    //Включаем режи бога при столкновении с турелью.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Turret turret = collision.transform.root.GetComponent<Turret>();

        if (turret)
        {
            _levelState.IsGodMode = true;

            _levelState.ResetTimer(); //Сброс таймера при повторном поднятии режима бога.

            _cartParticleSystems.Play();

            Destroy(gameObject);
        }
    }
}
