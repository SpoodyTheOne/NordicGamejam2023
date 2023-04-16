using UnityEngine;

public class MoveUp : MonoBehaviour
{
    [SerializeField]
    public float speed;
    public Rigidbody2D rb;
    public bool moveNow;

    void Start()
    {
        rb.velocity = transform.up * speed;
    }

    private void Update()
    {
    }
}
