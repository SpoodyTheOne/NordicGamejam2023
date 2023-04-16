using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MusketeerPlayer : Player
{
    [SerializeField] private GameObject fx;
    private bool go = true;

    bool coolDown = false;
    public Image img;
    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.E) && currentSpice >= specialCost && !coolDown)
            Target();

        if (Input.GetMouseButton(1) && continouosConsumption && go)
        {
            if (currentSpice < commonCost)
                return;

            StartCoroutine(Consume());
        }

        if(coolDown)
        {
            img.fillAmount -= 1 / specialCooldown * Time.deltaTime;

            if (img.fillAmount <= 0)
            {
                img.fillAmount = 0;
                coolDown = false;
            }
        }

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
