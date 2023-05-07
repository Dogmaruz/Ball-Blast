using UnityEngine;

public class Freezing : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Turret turret = collision.transform.root.GetComponent<Turret>();

        if (turret)
        {
            var stones = FindObjectsOfType<Stone>();

            foreach (var item in stones)
            {
                item.FreezingStone();
            }

            Destroy(gameObject);
        }
    }
}
