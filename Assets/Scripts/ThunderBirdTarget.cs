using UnityEngine;

public class ThunderBirdTarget : MonoBehaviour
{
    private Vector2 mousePos;

    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePos;

        if (Input.GetKeyUp(KeyCode.E))
        {
            GameObject.Find("Thunderbird").gameObject.transform.position = transform.position;
            GameObject.Find("Thunderbird").gameObject.GetComponent<ThunderBirdPlayer>().BirdFormDeactivate();
            Destroy(gameObject);
        }
    }
}
