using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> Rooms;

    public GameObject StartRoom;

    // Start is called before the first frame update
    void Start()
    {
        StartRoom.GetComponent<RoomController>().NextWave();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
