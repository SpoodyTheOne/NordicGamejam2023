using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    private void OnParticleCollision(GameObject collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
            Destroy(gameObject);
    }
}
