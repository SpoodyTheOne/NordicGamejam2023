using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void LoadScene (string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
