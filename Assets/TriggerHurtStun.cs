using UnityEngine;

public class TriggerHurtStun : IHurter
{
    [SerializeField] private float stunTime = 2f;

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.GetComponent<EnemyBehavior>())
            return;

        base.OnTriggerEnter2D(other);

        other.gameObject.GetComponent<EnemyBehavior>().Stun(stunTime);
    }
}
