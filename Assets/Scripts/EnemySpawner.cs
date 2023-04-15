using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    public RoomController room;

    void Spawn(GameObject enemy = null)
    {
        GameObject newEnemy = Instantiate(enemy != null ? enemy : Enemy, transform.position, Quaternion.identity);
        
        // newEnemy.GetComponent<EnemyDeathTrigger>().setSpawner(this.gameObject);

        if (room)
            room.OnEnemySpawned();
    }

    void OnEnemyDied(GameObject enemy)
    {
        if (room)
            this.room.OnEnemyDied();
    }

}
