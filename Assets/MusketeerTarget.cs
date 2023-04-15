using UnityEngine;

public class MusketeerTarget : MonoBehaviour
{
    private Vector2 mousePos;
    [SerializeField] private GameObject boat;

    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePos;

        if (Input.GetKeyUp(KeyCode.E))
        {
            var boatTemp = Instantiate(boat, transform.position, Quaternion.identity);
            Destroy(boatTemp, 7f);
            Destroy(gameObject);
        }
    }
}
