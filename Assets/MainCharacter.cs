using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    public CharacterController controller;
    public float speed;
    public float turnspeed;
    public Vector3 velocity, move;
    private float forwardMovement, sidewaysMovement;
    public Animator animator;
    //Jumping/falling variables
    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDistance;
    public float gravity = -9.81f;
    public float jumpPower;
    public float fallMultiplier = 1.25f;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //checks if player is on ground by casting a sphere at players feet and seeing if that collides with the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        applyGravity();


        //gathers input for player on x and z axis
        sidewaysMovement = Input.GetAxis("Horizontal") * speed;
        forwardMovement = Input.GetAxis("Vertical") * speed;
        move = (transform.right * sidewaysMovement) + (transform.forward * forwardMovement);
        velocity = new Vector3(move.x, velocity.y, move.z);

        //Moves player (vertically and horizontally)
        if (velocity != Vector3.zero)
        {
            controller.Move(velocity * Time.deltaTime);
            Vector3 animvec = new Vector3(move.x, 0, move.z);
            animator.SetFloat("Speed", animvec.magnitude);
        }
        
        

        
    }

    private void applyGravity()
    {
        //resets y velocity when player is on ground (so gravity doesn't constantly build up)
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        //applies gravity when player is in the air
        else if (!isGrounded && velocity.y > 0)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        //applies greater gravity when player is falling to make jumping feel less floaty
        else
        {
            velocity.y += gravity * fallMultiplier * Time.deltaTime;
        }
    }
}
