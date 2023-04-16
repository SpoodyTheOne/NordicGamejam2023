using UnityEngine;
using System.Collections;

public class NecromancerStaffScript : MonoBehaviour
{
    public Transform[] summonSpots;
    public GameObject summoningPrefab;

    public float summonTime;

    private bool summoning;

    // Update is called once per frame
    void Update()
    {
        if (!summoning)
            StartCoroutine(Summon());
    }
    public IEnumerator Summon()
    {
        summoning = true;
        int randNumber = Random.Range(0, summonSpots.Length);
        var obj = Instantiate(summoningPrefab, summonSpots[randNumber].transform.position, Quaternion.identity);
        Destroy(obj, 5f);
        yield return new WaitForSeconds(summonTime);
        summoning = false;
    }
}
