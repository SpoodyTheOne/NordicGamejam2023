using System.Collections;
using UnityEngine;

public class CavemanPlayer : Player
{
    [SerializeField] private GameObject fx;

    private bool go = true;
    public void StartPFX()
    {
        pfx.Play();
        pfx.loop = true;
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetMouseButton(1) && continouosConsumption && go)
        {
            if (currentSpice < commonCost)
                return;

            StartCoroutine(Consume());
        }

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
    private IEnumerator Consume()
    {
        go = false;
        currentSpice -= commonCost;
        yield return new WaitForSeconds(.5f);
        go = true;
    }
}
