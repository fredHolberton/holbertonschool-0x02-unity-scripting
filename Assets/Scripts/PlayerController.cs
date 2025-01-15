using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Speed of the sphere mouvement
    public float speed;

    Rigidbody rb;
    float vertical;
    float horizontal;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // DEPLACEMENT
        vertical = Input.GetAxisRaw("Vertical");
        horizontal = Input.GetAxisRaw("Horizontal");
        Vector3 mouvement = new Vector3(horizontal, 0, vertical).normalized;
        rb.velocity = new Vector3(mouvement.x* speed, rb.velocity.y, mouvement.z * speed);
    }
}
