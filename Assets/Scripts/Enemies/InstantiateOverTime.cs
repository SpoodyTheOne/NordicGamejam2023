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
    public float Speed = 800;
    [SerializeField]
    public bool Burst = true;
    [SerializeField]
    public float Cooldown = 1f;
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
            Vector2 arcStart = -transform.up;

            for (int i = 0; i < currentPattern.Amount; i++)
            {
                float stepSize =  currentPattern.Arc/((float)currentPattern.Amount);
                Vector2 cArc = rotate(arcStart, stepSize*i + stepSize/2f - currentPattern.Arc/2f);

                GameObject p = Instantiate(projectile, transform.position, Quaternion.identity);
                p.transform.LookAt((Vector2)p.transform.position + cArc, new Vector3(0,0,1) );
                Vector3 r = p.transform.rotation.eulerAngles;
                p.transform.rotation = Quaternion.Euler(0, 0, r.z);
                p.GetComponent<MoveUp>().speed = currentPattern.Speed;
            }

            CanShootTime = Time.time + currentPattern.Cooldown;
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
        float delta = deg * (float)Mathf.Deg2Rad;
        return new Vector2(
            v.x * Mathf.Cos(delta) - v.y * Mathf.Sin(delta),
            v.x * Mathf.Sin(delta) + v.y * Mathf.Cos(delta)
    );
}

}
