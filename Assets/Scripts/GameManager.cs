using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> Rooms;

    public GameObject StartRoom;

    RoomController currentRoom;

    // Start is called before the first frame update
    void Start()
    {
        currentRoom = StartRoom.GetComponent<RoomController>();
        //currentRoom.NextWave();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentRoom.IsDone)
        {
            Vector3 ExitPos = currentRoom.RoomExit.transform.position;
            
            GameObject nextRoom = Instantiate(getRandomRoom(), currentRoom.transform.position, Quaternion.identity);

            Vector3 EntrancePos = nextRoom.GetComponent<RoomController>().RoomEntrance.transform.position;

            nextRoom.transform.Translate(ExitPos - EntrancePos);

            currentRoom = nextRoom.GetComponent<RoomController>();
        }
    }

    GameObject getRandomRoom()
    {
        return Rooms[Random.Range(0, Rooms.Count)];
    }
}
