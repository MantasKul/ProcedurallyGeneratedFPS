using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Camera")]
    public Camera playerCamera;
    public float mouseSensitivity = 5f;

    private float m_CameraVerticalAngle = 0f;

    [Header("Movement")]
    public float movementSpeed;
    public float jumpForce = 5f;

    private bool isGrounded = false;
    private Rigidbody rb;
    private Vector3 movementDirection;

    [Header("Powers")]
    private float nextAbility;
    public float abilityCoolDown = 0.5f;
    // Dash
    public bool dashEnabled;
    private bool isDashing;
    public float dashForce = 50f;

    // Shield
    public bool shieldEnabled;
    private bool isShielded;
    private Collider shield;

    private bool jump;
    public void Awake()
    {
        dashEnabled = true;
        shieldEnabled = false;

        Cursor.lockState = CursorLockMode.Locked;

        rb = GetComponent<Rigidbody>();
        shield = GetComponent<SphereCollider>();

        movementSpeed = 10f;
}

    private void Update()
    {
        CameraController();
        IsGrounded();
        movementDirection = GetMoveInput();

        if(Input.GetKeyDown(KeyCode.E))
        {
            if(dashEnabled == true && shieldEnabled == false)
            {
                dashEnabled = false;
                shieldEnabled = true;
            }
            else if(dashEnabled == false && shieldEnabled == true)
            {
                dashEnabled = true;
                shieldEnabled = false;    
            }
        }

        if (Input.GetMouseButtonDown(1) && Time.time >= nextAbility)
        {
            nextAbility = Time.time + abilityCoolDown;
            if (dashEnabled == true)
                isDashing = true;
            if (shieldEnabled == true)
                StartCoroutine(Shield());
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            jump = true;
    }

    private void FixedUpdate()
    {
        rb.velocity = movementDirection * movementSpeed + new Vector3(0f, rb.velocity.y, 0f);

        if (jump)
        {
            rb.velocity = Vector3.up * jumpForce;
            jump = false;
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * 3f * Time.fixedDeltaTime;
        }
        else if (rb.velocity.y > 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * 4f * Time.fixedDeltaTime;
        }

        // Got to call Dash() from FixedUpdate since it's physics action, and get button press from Update because it's bit better
        if (isDashing)
        {
            Dash();
        }

        if(isShielded)
        {
            Shield();
        }
    }

    #region Camera
    private void CameraController()
    {
        transform.Rotate(new Vector3(0f, (Input.GetAxis("Mouse X") * mouseSensitivity), 0f), Space.Self);

        // add vertical inputs to the camera's vertical angle. negative because when it's positive it's inverted
        m_CameraVerticalAngle -= Input.GetAxis("Mouse Y") * mouseSensitivity;

        // limit the camera's vertical angle to min/max, so you couldn't spin infinitally, only look up/down
        m_CameraVerticalAngle = Mathf.Clamp(m_CameraVerticalAngle, -90f, 90f);

        // apply the vertical angle as a local rotation to the camera transform along its right axis (makes it pivot up and down)
        playerCamera.transform.localEulerAngles = new Vector3(m_CameraVerticalAngle, 0, 0);
    }
    #endregion

    #region Movement Input
    private Vector3 GetMoveInput()
    {
        // Getting input
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        // Transfroming input to Vector3 and normalizing so you wouldn't walk at 2x speed sideways (applying 1 on x and 1 on y getting 2)
        Vector3 input = transform.right * inputX + transform.forward * inputY;
        Vector3 inputDir = input.normalized;

        return inputDir;
    }
    #endregion
    private void IsGrounded()
    {
        // cast ray from a center of a player straight down, if it hits anything return true.
        if (Physics.Raycast(transform.position, -Vector3.up, 1.05f))
            isGrounded = true;
        else
            isGrounded = false;
    }

    #region Powers
    private void Dash()
    {
        // if not moving simply dash forward designer's choice, could be possible to simply not do anything if not moving
        if (GetMoveInput() == new Vector3(0f, 0f, 0f))
            rb.AddForce(transform.forward * dashForce, ForceMode.Impulse);

        // Apply force to the direction of movement
        rb.AddForce(GetMoveInput() * dashForce, ForceMode.Impulse);
        isDashing = false;
    }

    IEnumerator Shield()
    {
        shield.enabled = true;
        shieldEnabled = false;
        yield return new WaitForSeconds(5f);
        shieldEnabled = true;
        shield.enabled = false;
    }
    #endregion

    public void increaseSpeed()
    {
        movementSpeed += 0.5f;
    }
}
