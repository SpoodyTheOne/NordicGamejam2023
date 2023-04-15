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
    public LayerMask WhoCanStarRoomREEEE;
    public List<EnemySpawner> EnemySpawners;

    public GameObject RoomExit;
    public GameObject RoomEntrance;

    public bool IsDone = false;

    [SerializeField]
    public List<wave> Waves;

    [SerializeField]
    private int EnemiesAlive = 0;

    [SerializeField]
    private int _Wave = -1;

    public int Wave { get => _Wave; }

    public void OnEnemyDied()
    {
        EnemiesAlive--;
        
        Debug.Log("Enemy Ded");

        if (EnemiesAlive == 0)
            NextWave();
    }

    public void OnEnemySpawned()
    {
        Debug.Log("Enemy spawn");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name);

        if (((1<<other.gameObject.layer) & WhoCanStarRoomREEEE) == 0) // Check if part of canHit layermask
            return;

        OnRoomEnter();
    }

    public void OnRoomEnter()
    {
        if (IsDone)
            return;

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
        _Wave++;

        if (_Wave >= Waves.Count) {
            RoomExit.SetActive(false);
            RoomEntrance.SetActive(false);
            IsDone = true;
            return;
        }

        EnemiesAlive = Waves[_Wave].Enemies.Count;

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
