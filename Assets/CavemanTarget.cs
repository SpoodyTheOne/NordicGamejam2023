using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CavemanTarget : MonoBehaviour
{
    private Vector2 mousePos;
    [SerializeField] GameObject hand;
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePos;

        if (Input.GetKeyUp(KeyCode.E))
        {
            var handTemp = Instantiate(hand, transform.position, Quaternion.identity);
            Destroy(handTemp, 7f);
            Destroy(gameObject);
        }
    }
}
