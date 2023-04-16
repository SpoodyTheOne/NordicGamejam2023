using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CavemanPlayer : Player
{
    [SerializeField] private GameObject fx;

    private bool go = true;

    private bool coolDown = false;
    public Image img;
    public void StartPFX()
    {
        pfx.Play();
        pfx.loop = true;
    }

    public override void Update()
    {
        base.Update();

        if (coolDown)
        {
            img.fillAmount -= 1 / specialCooldown * Time.deltaTime;

            if (img.fillAmount <= 0)
            {
                img.fillAmount = 0;
                coolDown = false;
            }
        }

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
        if (coolDown)
            return;

        coolDown = true;
        img.fillAmount = 1;

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
