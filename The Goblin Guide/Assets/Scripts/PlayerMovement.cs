using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController controller;
    [SerializeField] float speed;
    [SerializeField] float sprint;
    float currentVelocity = 0;
    float baseSpeed;

    //Jump variables
    bool isGrounded;
    [SerializeField] float groundCheckDistance;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float gravity;
    [SerializeField] float jumpHeight;
    [HideInInspector] public Vector3 inputDirection;
    [HideInInspector] public bool charGrounded;
    Vector3 velocity;
    Camera playerCam;

    //pickups
    public int goldvalue;
    public int mushroomvalue;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerCam = FindFirstObjectByType<Camera>();

    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        inputDirection = new Vector3(horizontal, 0.0f, vertical);

        if(Input.GetKey(KeyCode.LeftShift) && mushroomvalue >= 1)
        {
            speed = 20 * (1 + mushroomvalue);
        }
        else
        {
            speed = 15;
        }

        if (inputDirection.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + playerCam.transform.eulerAngles.y;
            float smoothRotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, 0.01f);
            Vector3 moveDir = Quaternion.Euler(0.0f, smoothRotation, 0.0f) * Vector3.forward;
            transform.rotation = Quaternion.Euler(0.0f, smoothRotation, 0.0f);
            controller.Move(moveDir * Time.deltaTime * speed);

        }

        //Jump code
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

        velocity.y -= gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        //Debug.Log(velocity);

        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * 2 * gravity);
            charGrounded= false;

        }
        else
        {
            charGrounded= true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Coin")
        {
            goldvalue++;
            UnityEngine.Debug.Log("Gold picked up: " + goldvalue);
        }

        if (other.tag == "Mushroom" && goldvalue >= 15)
        {
            mushroomvalue++;
            goldvalue -= 15;
            UnityEngine.Debug.Log("Mushrooms collected: " + mushroomvalue);
            Destroy(other.gameObject);
        }
    }
}