using UnityEngine;

public class TriggerHurtStay : IHurter
{
    private void OnTriggerStay2D(Collider2D other, bool sa)
    {
        
        if (((1 << other.gameObject.layer) & canHit) == 0) // Check if part of canHit layermask
            return;

        IDamagable otherDamagable = other.GetComponent<IDamagable>();

        bool succ = false;

        if (otherDamagable)
        {
            succ = otherDamagable.TakeDamage(this.gameObject, this.Damage);
        }

        OnDamage(other, succ);
    }
}
