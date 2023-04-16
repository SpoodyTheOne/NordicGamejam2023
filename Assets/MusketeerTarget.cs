using UnityEngine;

public class MusketeerTarget : MonoBehaviour
{
    private Vector2 mousePos;
    [SerializeField] private GameObject boat;
    public bool controller;
    public Transform cursorTransform;

    void Update()
    {
        if (controller)
        {
            mousePos = Camera.main.ScreenToWorldPoint(cursorTransform.position);
            transform.position = mousePos;
        } else
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePos;
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            var boatTemp = Instantiate(boat, transform.position, Quaternion.identity);
            Destroy(boatTemp, 7f);
            Destroy(gameObject);
        }
    }

    public void Boat()
    {
        var boatTemp = Instantiate(boat, transform.position, Quaternion.identity);
        Destroy(boatTemp, 7f);
        Destroy(gameObject);
    }
}
