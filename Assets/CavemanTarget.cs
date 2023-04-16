using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CavemanTarget : MonoBehaviour
{
    private Vector2 mousePos;
    [SerializeField] GameObject hand;

    public bool controller;
    public Transform cursorTransform;

    void Update()
    {
        if (controller)
        {
            mousePos = Camera.main.ScreenToWorldPoint(cursorTransform.position);
            transform.position = mousePos;
        }
        else
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePos;
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            var handTemp = Instantiate(hand, transform.position, Quaternion.identity);
            Destroy(handTemp, 7f);
            Destroy(gameObject);
        }
    }

    public void Slam()
    {
        var handTemp = Instantiate(hand, transform.position, Quaternion.identity);
        Destroy(handTemp, 7f);
        Destroy(gameObject);
    }
}
