using UnityEngine;

public class FollowPlayerCamera : MonoBehaviour
{
    Player playerToFollow;

    float t;

    private void Start()
    {
        Invoke("ConnectCam", .1f);
    }

    private void ConnectCam()
    {
        playerToFollow = FindObjectOfType<Player>();
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position ,new Vector3(playerToFollow.gameObject.transform.position.x,
            playerToFollow.gameObject.transform.position.y, -10), 2f * Time.deltaTime);
    }
}
