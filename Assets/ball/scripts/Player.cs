using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float moveForce = 20f;
    public float maxSpeed = 10f;

    [Header("Camera")]
    public float cameraDistance = 8f;
    public float cameraHeight = 4f;
    public float cameraSmoothSpeed = 5f;

    private Rigidbody rb;
    private Camera mainCam;
    private Vector3 cameraOffset;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCam = Camera.main;
        cameraOffset = new Vector3(0f, cameraHeight, -cameraDistance);
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void LateUpdate()
    {
        HandleCamera();
    }

    void HandleMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 force = new Vector3(h, 0f, v) * moveForce;
        rb.AddForce(force);

        // Clamp horizontal speed so the ball doesn't rocket away
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        if (flatVel.magnitude > maxSpeed)
        {
            Vector3 clamped = flatVel.normalized * maxSpeed;
            rb.linearVelocity = new Vector3(clamped.x, rb.linearVelocity.y, clamped.z);
        }
    }

    void HandleCamera()
    {
        Vector3 targetPos = transform.position + cameraOffset;
        mainCam.transform.position = Vector3.Lerp(
            mainCam.transform.position,
            targetPos,
            cameraSmoothSpeed * Time.deltaTime
        );
        mainCam.transform.LookAt(transform.position);
    }
}