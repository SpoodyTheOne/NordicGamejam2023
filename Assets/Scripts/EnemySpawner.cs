using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    [SerializeField]
    public ParticleSystem SpawnEffect;
    public RoomController room;

    public void Spawn(GameObject enemy = null)
    {
        GameObject newEnemy = Instantiate(enemy != null ? enemy : Enemy, transform.position, Quaternion.identity);
        
        newEnemy.GetComponent<EnemyBehavior>().spawner = this;
        if (room)
            room.OnEnemySpawned();

        SpawnEffect.Play();
    }

    public void OnEnemyDied(GameObject enemy)
    {
        if (room)
            this.room.OnEnemyDied();
    }

}
