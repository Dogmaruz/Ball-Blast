using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bag bag = collision.transform.root.GetComponent<Bag>();

        if (bag)
        {
            //ѕомещаем монету при столкновении с тулелью.
            bag.PushCoin(1);
            Destroy(gameObject);
        }
    }
}
