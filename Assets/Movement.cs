using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Transform target;
    public float speed;
    public float jumpForce;
    private float horizontalInput;
    private float verticalInput;
    private bool jumpInput;

    private Rigidbody rb;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        // moving from A to B
        //MovingFromAToB();

        // use arrow key to move
        MovingByKey();
    }

    void MovingFromAToB()
    {
        Vector3 a = transform.position;
        Vector3 b = target.position;
        transform.position = Vector3.MoveTowards(a, b, speed*Time.deltaTime);
    }

    void MovingByKey()
    {
        // Moving
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * speed * Time.deltaTime * verticalInput);
        transform.Translate(Vector3.right * speed * Time.deltaTime * horizontalInput);

        // Jump
        jumpInput = Input.GetKeyDown(KeyCode.Space);
        if (jumpInput)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

    }




}
