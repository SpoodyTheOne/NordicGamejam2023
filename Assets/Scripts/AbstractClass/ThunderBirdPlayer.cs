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

    float timeElapsed;
    float birdSpeed = 4;

    public override void Awake()
    {
        base.Awake();


    }
    public override void Update()
    {
        base.Update();

        if (Input.GetMouseButtonDown(1))
        {
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

        playerForms[1].SetActive(true);
        playerForms[0].SetActive(false);

        transform.position = Vector2.MoveTowards(transform.position, new Vector2(mousePos.x, mousePos.y), birdSpeed);

        if (timeElapsed > flyTime)
            BirdFormDeactivate();
    }
    private void BirdFormDeactivate()
    {
        birdForm = false;

        playerForms[0].SetActive(true);
        playerForms[1].SetActive(false);

        speed = 4;
    }
}
