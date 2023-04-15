using UnityEngine;

public class MoveUp : MonoBehaviour
{
    [SerializeField]
    public float speed;
    public Rigidbody2D rb;
    public bool moveNow;

    private void Update()
    {
        if (moveNow)
            rb.velocity = transform.up * speed * Time.deltaTime;
    }
}
