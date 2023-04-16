using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusketeerPlayer : Player
{
    [SerializeField] private GameObject fx;
    private bool go = true;
    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.E) && currentSpice >= specialCost)
            Target();

        if (Input.GetMouseButton(1) && continouosConsumption && go)
        {
            if (currentSpice < commonCost)
                return;

            StartCoroutine(Consume());
        }

    }
    private void Target()
    {
        currentSpice -= specialCost;
        var target = Instantiate(fx, mousePos, Quaternion.identity);
    }
    public void StartPFX()
    {
        pfx.Play();
        pfx.loop = true;
    }
    private IEnumerator Consume()
    {
        go = false;
        currentSpice -= commonCost;
        yield return new WaitForSeconds(.5f);
        go = true;
    }
}
