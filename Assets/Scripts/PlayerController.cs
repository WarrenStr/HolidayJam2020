using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB;

    public float speed = 5;
    public float boostSpeed = 12;
    public float jumpForce;
    public float gravityModifier;

    public Transform groundChecker;
    private float groundDistance = 0.2f;
    public LayerMask ground;
    
    public bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, ground);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        float xMovement = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float zMovement = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        transform.Translate(Vector3.right * xMovement);
        transform.Translate(Vector3.forward * zMovement);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = boostSpeed;
        }
        else
        {
            speed = 5;
        }
    }
}
