using UnityEngine;

public class CavemanPlayer : Player
{
    [SerializeField] private GameObject fx;

    public void StartPFX()
    {
        pfx.Play();
        pfx.loop = true;
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.E) && currentSpice >= specialCost)
            Target();
    }
    private void Target()
    {
        currentSpice -= specialCost;
        var target = Instantiate(fx, mousePos, Quaternion.identity);
    }

    public override bool TakeDamage(GameObject attacker, float Amount)
    {
        if (GetComponentInChildren<CavemanWeapon>().blocking)
            return base.TakeDamage(attacker, Amount/2);
        else
            return base.TakeDamage(attacker, Amount);
    }
}
