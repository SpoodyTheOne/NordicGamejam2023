using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct wave
{
    [SerializeField]
    public List<GameObject> Enemies;
}

public class RoomController : MonoBehaviour
{
    public List<EnemySpawner> EnemySpawners;

    public GameObject RoomExit;
    public GameObject RoomEntrance;

    [SerializeField]
    public List<wave> Waves;

    private int EnemiesAlive = 0;
    private int _Wave;
    public int Wave { get => _Wave; }

    public void OnEnemyDied()
    {
        EnemiesAlive--;

        if (EnemiesAlive == 0)
            NextWave();
    }

    public void OnEnemySpawned()
    {
        EnemiesAlive++;
    }

    public void OnRoomEnter()
    {
        RoomEntrance.SetActive(true);
        NextWave();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void NextWave()
    {
        if (_Wave >= Waves.Count) {
            RoomExit.SetActive(false);
            RoomEntrance.SetActive(false);
            return;
        }

        _Wave++;

        StartCoroutine("NextWaveCoroutine");
    }

    IEnumerator NextWaveCoroutine()
    {
        yield return new WaitForSeconds(1f);

        int EnemiesSpawned = 0;

        while (EnemiesSpawned < Waves[_Wave].Enemies.Count)
        {
            int spawnerIndex = EnemiesSpawned % EnemySpawners.Count;

            EnemySpawners[spawnerIndex].Spawn( Waves[_Wave].Enemies[EnemiesSpawned++] );

            yield return new WaitForSeconds(0.2f);
        }

        yield return null;
    }
}
