using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateOverTime : MonoBehaviour
{
    public float lastInstant, timeBetweenInstances;
    public GameObject instant;
    public bool instantiatingSwitch;
    public float lastSwitch, deactiveTime, activeTime;
    private bool instantiating = true;

    private void Update()
    {
        if (Time.time - lastInstant > timeBetweenInstances)
        {
            if (instantiating == true)
            {
                Instantiate(instant, transform.position, transform.rotation);
                lastInstant = Time.time;
            }
        }

        if (instantiatingSwitch)
        {
            if (!instantiating && Time.time - lastSwitch > deactiveTime)
            {
                instantiating = true;
                lastSwitch = Time.time;
            }

            if (instantiating && Time.time - lastSwitch > activeTime)
            {
                instantiating = false;
                lastSwitch = Time.time;
            }
        }
    }
}
