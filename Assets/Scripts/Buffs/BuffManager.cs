using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    [SerializeField]
    public float SpeedMult { get { return _SpeedMult; } }
    private float _SpeedMult = 1f;

    [SerializeField]
    public float DamageMult { get { return _DamageMult; } }
    private float _DamageMult = 1f;

    [SerializeField]
    public float HeatLossMult { get { return _HeatLossMult; } }
    private float _HeatLossMult = 1f;

    [SerializeField]
    public float CritMult { get { return _HeatLossMult; } }
    private float _CritMult = 1f;

    [SerializeField]
    public List<IBuffInterface> Buffs { get { return _buffs; } }
    private List<IBuffInterface> _buffs = new List<IBuffInterface>();

    public void BuffAdded(IBuffInterface buff)
    {

        _buffs.Add(buff);

        _DamageMult += buff.DamageMult;
        _SpeedMult += buff.SpeedMult;
        _HeatLossMult += buff.HeatLossMult;

        Debug.Log("Buff Added");

    }

    public void BuffRemoved(IBuffInterface buff)
    {

        _buffs.Remove(buff);

        _DamageMult -= buff.DamageMult;
        _SpeedMult -= buff.SpeedMult;
        _HeatLossMult -= buff.HeatLossMult;

        Debug.Log("Buff Removed");

    }

    public void OnAttack()
    {
        foreach (IBuffInterface buff in Buffs)
            buff.OnAttack();
    }

    // TODO:
    // Make damage source parameter, aka which enemy hit
    public void OnDamaged() 
    {
        foreach (IBuffInterface buff in Buffs)
            buff.OnDamaged();
    }

    public void OnDeath()
    {
        foreach (IBuffInterface buff in Buffs)
            buff.OnDeath();
    }

    public void OnDash()
    {
        foreach (IBuffInterface buff in Buffs)
            buff.OnDash();
    }

    public void OnEnemyDamaged(GameObject enemy)
    {
        Debug.Log("recieved fuck");

        foreach (IBuffInterface buff in Buffs) {
            Debug.Log("RUN SHIT FOR THIS" + buff.GetType().Name);
            buff.OnEnemyDamaged(enemy);
        }
    }

    public bool hasBuff<T>() {

        if (this.getBuff<T>() == null)
            return false;

        return true;

    }

    public IBuffInterface getBuff<T>() {

        foreach (IBuffInterface buff in Buffs) {
        if (typeof(T) == buff.GetType())
            return buff;
        }

        return null;
    }
}
