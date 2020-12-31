using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB;
    private Animator playerAnim;

    public float speed = 1;
    public float sprintSpeed = 5;
    public float jumpForce;
    public float gravityModifier;

    public Transform groundChecker;
    private float groundDistance = 0.2f;
    public LayerMask ground;
    
    public bool isGrounded;

    float initialSpeed;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;
        initialSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, ground);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            playerAnim.SetTrigger("Jump_trig");
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
        if (xMovement != 0 || zMovement != 0)
        {
            playerAnim.SetBool("Walk", true);
        }
        else if (xMovement == 0 || zMovement == 0)
        {
            playerAnim.SetBool("Walk", false);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = sprintSpeed;
            playerAnim.SetBool("Run", true);
        }
        else
        {
            speed = initialSpeed;
            playerAnim.SetBool("Run", false);
        }
    }
}
