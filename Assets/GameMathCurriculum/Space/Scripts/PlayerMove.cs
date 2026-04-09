using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody rb;
    public float Speed = 5f;
    public float rotSpeed = 30f;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        float h = Input.GetAxis("Horizontal");       
        float v = Input.GetAxis("Vertical");

        rb.linearVelocity = new Vector3(h, 0f, v) * Speed;


        if(Input.GetKey(KeyCode.Q))
        {
            transform.rotation *= Quaternion.AngleAxis(rotSpeed * Time.deltaTime, Vector3.down);
        }

        if(Input.GetKey(KeyCode.E))
        {
            transform.rotation *= Quaternion.AngleAxis(rotSpeed * Time.deltaTime, Vector3.up);
        }
       
    }
}
