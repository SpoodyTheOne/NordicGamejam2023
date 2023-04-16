using UnityEngine;

public class HerbBag : MonoBehaviour
{
    private void Awake()
    {
        transform.parent = null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponentInParent<Player>();

            player.currentSpice += Random.Range(3, 8);

            int randomHeal = Random.Range(0, 15);

            if (randomHeal < 2)
                player.TakeDamage(this.gameObject, -1);

            if (player.Health > player.maxHealth)
                player.TakeDamage(this.gameObject, 1);

            if (player.currentSpice > player.maxSpice)
                player.currentSpice = 100;

            Destroy(gameObject);
        }
    }
}
