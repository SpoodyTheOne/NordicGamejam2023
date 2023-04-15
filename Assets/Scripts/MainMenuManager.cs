using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public void ActivateObject(GameObject object1)
    {
        object1.SetActive(true);
    }

    public void DeactivateObject(GameObject object1)
    {
        object1.SetActive(false);
    }
}
