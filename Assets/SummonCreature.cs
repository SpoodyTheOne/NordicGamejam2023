using UnityEngine;

public class SummonCreature : MonoBehaviour
{
    public GameObject creature;
    public Transform creatureSpawn;

    public void SpawnCreature()
    {
        Instantiate(creature, creatureSpawn.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
