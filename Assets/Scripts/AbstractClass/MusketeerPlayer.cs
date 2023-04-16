using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class MusketeerPlayer : Player
{
    [SerializeField] private GameObject fx;
    private bool go = true;

    bool coolDown = false;
    public Image img;

    public GameObject tempTarget;

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
    private void ControllerTarget()
    {
        if (coolDown)
            return;

        coolDown = true;
        img.fillAmount = 1;

        currentSpice -= specialCost;
        GameObject target = Instantiate(fx, virtualMousePos, Quaternion.identity);
        target.GetComponent<MusketeerTarget>().controller = true;
        target.GetComponent<MusketeerTarget>().cursorTransform = virtualCursor;
        tempTarget = target;
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

    public void RightClickAbility(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && continouosConsumption && go)
        {
            if (currentSpice < commonCost)
                return;

            StartCoroutine(Consume());
        }
    }

    public void EAbility(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            Debug.Log("Jam");
            if (currentSpice >= specialCost && !coolDown)
                ControllerTarget();
        }

        if(ctx.canceled)
        {
            tempTarget.GetComponent<MusketeerTarget>().Boat();
        }
    }
}
