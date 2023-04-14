using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThunderBirdPlayer : Player
{
    private bool birdForm;
    [SerializeField] private GameObject[] playerForms;

    private float flyTime = 1;
    [SerializeField] private Image timeForFly;
    private bool lerpTime;

    [SerializeField] GameObject[] fx;
    private GameObject effect = null;

    float timeElapsed;
    float birdSpeed;
    public override void Awake()
    {
        base.Awake();

        BirdFormDeactivate();
    }
    public override void Update()
    {
        base.Update();

        Debug.Log(speed);

        speed = Mathf.Lerp(speed, 4f, timeElapsed);

        if (speed > 4.0001)
        {
            timeElapsed += Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(mousePos.x, mousePos.y), speed * Time.deltaTime);
        }

        if (Input.GetMouseButtonDown(1) && !birdForm)
        {
            effect = Instantiate(fx[0], gameObject.transform.position, Quaternion.identity);
            Destroy(effect, 1.2f);

            effect.transform.parent = playerForms[1].transform;

            timeElapsed = 0;
            birdForm = true;
        }
        if (Input.GetMouseButtonUp(1))
            BirdFormDeactivate();

        if (birdForm)
            BirdFormActivate();
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();


    }

    private void BirdFormActivate()
    {
        if (timeElapsed < flyTime)
        {
            float valueToLerp = Mathf.Lerp(0, 1, timeElapsed / flyTime);
            timeElapsed += Time.deltaTime;
            timeForFly.fillAmount = timeElapsed;
        }

        speed = Mathf.Lerp(speed, 16f, timeElapsed);

        playerForms[1].SetActive(true);
        playerForms[0].SetActive(false);

        transform.position = Vector2.MoveTowards(transform.position, new Vector2(mousePos.x, mousePos.y), speed * Time.deltaTime);

        if (timeElapsed > flyTime)
            BirdFormDeactivate();
    }
    private void BirdFormDeactivate()
    {
        if (effect != null)
            effect.transform.parent = null;

        timeElapsed = 0;
        birdForm = false;

        playerForms[0].SetActive(true);
        playerForms[1].SetActive(false);
    }
}
