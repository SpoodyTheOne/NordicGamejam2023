using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletPattern
{
    [SerializeField]
    public float Arc = 15;
    [SerializeField]
    public int Amount = 3;
    [SerializeField]
    public bool Burst = true;
    [SerializeField]
    public float TimeToShoot = 1f;
}

[System.Serializable]
public struct BulletPatternSequence
{
    [SerializeField]
    public List<BulletPattern> sequence;
    [SerializeField]
    public int currentPattern;
}

public class InstantiateOverTime : MonoBehaviour
{
    private float CanShootTime;
    
    public GameObject projectile;
    public List<BulletPatternSequence> Patterns;
    BulletPatternSequence _CurrentSequence;

    private void Start()
    {
        _CurrentSequence = getRandomPattern();
    }

    private void Update()
    {
        if (CanShootTime < Time.time) // We are allowed to shoot
        {
            BulletPattern currentPattern = _CurrentSequence.sequence[_CurrentSequence.currentPattern];

            // Instantiate boolets for patters
            Vector2 arcStart = rotate(-transform.up, -currentPattern.Arc/2);

            for (int i = 0; i < currentPattern.Amount; i++)
            {
                Vector2 cArc = rotate(arcStart, (currentPattern.Arc/currentPattern.Amount)*i );

                GameObject p = Instantiate(projectile, transform.position, Quaternion.identity);
                p.transform.LookAt((Vector2)p.transform.position + cArc, new Vector3(0,0,1) );
                Vector3 r = p.transform.rotation.eulerAngles;
                p.transform.rotation = Quaternion.Euler(0, 0, r.z);
            }

            CanShootTime = Time.time + currentPattern.TimeToShoot;
            _CurrentSequence.currentPattern++;

            if (_CurrentSequence.currentPattern >= _CurrentSequence.sequence.Count)
            { // Done shooting this sequence
                _CurrentSequence = getRandomPattern();
                _CurrentSequence.currentPattern = 0;
            }
        }
    }

    BulletPatternSequence getRandomPattern()
    {
        return Patterns[ Random.Range(0,Patterns.Count) ];
    }

    public static Vector2 rotate(Vector2 v, float deg) {
        float delta = deg * Mathf.Deg2Rad;
        return new Vector2(
            v.x * Mathf.Cos(delta) - v.y * Mathf.Sin(delta),
            v.x * Mathf.Sin(delta) + v.y * Mathf.Cos(delta)
    );
}

}
