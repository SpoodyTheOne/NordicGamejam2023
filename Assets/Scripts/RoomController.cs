using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct wave
{
    [SerializeField]
    List<GameObject> Enemies;
}

public class RoomController : MonoBehaviour
{
    public List<GameObject> EnemySpawners;

    public GameObject RoomExit;
    public GameObject RoomEntrance;

    [SerializeField]
    public List<wave> Waves;

    private int EnemiesAlive = 0;

    public void OnEnemyDied()
    {
        EnemiesAlive--;

        if (EnemiesAlive == 0);
            // coroutine;
    }

    public void OnEnemySpawned()
    {
        EnemiesAlive++;
    }

    public void OnRoomEnter()
    {
        // coroutine;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
