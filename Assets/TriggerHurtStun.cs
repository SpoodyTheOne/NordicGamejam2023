using UnityEngine;

public class TriggerHurtStun : IHurter
{
    [SerializeField] private float stunTime = 2f;

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);

        other.gameObject.GetComponent<EnemyBehavior>().Stun(stunTime);
    }
}
