using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;
using Unity.Cinemachine;

public class WheelController : NetworkBehaviour 
{
    [Header("Wheel Collider References")]
    [SerializeField] WheelCollider frontRight;
    [SerializeField] WheelCollider frontLeft;
    [SerializeField] WheelCollider backRight;
    [SerializeField] WheelCollider backLeft;

    [Header("Wheel Mesh References")]
    [SerializeField] Transform frontRightWheelTransform;
    [SerializeField] Transform frontLeftWheelTransform;
    [SerializeField] Transform backRightWheelTransform;
    [SerializeField] Transform backLeftWheelTransform;

    [Header("Car Movement Settings")]
    public float acceleration = 500f;
    public float breakingForce = 300f;
    public float breakingForceMultiplier = 0.5f;
    public float maxTurnAngle = 15f;
    
    [Header("Car Current Forces")]
    private float currentAcceleration = 0f;
    private float currentBreakForce = 0f;
    private float currentTurnAngle = 0f;

    private Vector2 moveInput;
    private bool isBraking;

    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    public void OnBrake(InputAction.CallbackContext context)
    {
        if (context.performed)
            isBraking = true;
        else if (context.canceled)
            isBraking = false;
    }

    private void FixedUpdate()
    {
        if (!IsOwner) return;
        
        // How fast (and which way) the car is currently moving along its own forward axis
        float forwardSpeed = Vector3.Dot(rb.linearVelocity, transform.forward);
        //Steering 
        currentTurnAngle = maxTurnAngle * moveInput.y;
        frontLeft.steerAngle = currentTurnAngle;
        frontRight.steerAngle = currentTurnAngle;
        // Is the player pressing the opposite direction to current motion?
        bool isReversingDirection = moveInput.x != 0f
            && Mathf.Sign(moveInput.x) != Mathf.Sign(forwardSpeed)
            && Mathf.Abs(forwardSpeed) > 0.5f; // small deadzone so it doesn't trigger near-zero speed
        if (isReversingDirection)
        {
            // Kill momentum hard before allowing the new direction to take over
            currentAcceleration = 0f;
            currentBreakForce = breakingForce;
        }
        else if (isBraking)
        {
            currentAcceleration = acceleration * moveInput.x;
            currentBreakForce = breakingForce;
        }
        else if (Mathf.Approximately(moveInput.x, 0f))
        {
            currentAcceleration = 0f;
            currentBreakForce = breakingForce * 0.5f;
        }
        else
        {
            currentAcceleration = acceleration * moveInput.x;
            currentBreakForce = 0f;
        }
        
        frontRight.motorTorque = currentAcceleration;
        frontLeft.motorTorque = currentAcceleration;
        backRight.motorTorque = currentAcceleration;
        backLeft.motorTorque = currentAcceleration;
        
        frontRight.brakeTorque = currentBreakForce;
        frontLeft.brakeTorque = currentBreakForce;
        backRight.brakeTorque = currentBreakForce;
        backLeft.brakeTorque = currentBreakForce;
        
        // Update Wheel Meshes
        UpdateWheel(frontLeft, frontLeftWheelTransform);
        UpdateWheel(frontRight, frontRightWheelTransform);
        UpdateWheel(backLeft, backLeftWheelTransform);
        UpdateWheel(backRight, backRightWheelTransform);
    }

    void UpdateWheel(WheelCollider wheelCol, Transform transform)
    {
        Vector3 position; 
        Quaternion rotation;
        wheelCol.GetWorldPose(out position, out rotation);

        // Set wheel transtorm state
        transform.position = position;
        transform.rotation = rotation;
    }
    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            CinemachineCamera cam = FindFirstObjectByType<CinemachineCamera>();
            if (cam != null)
            {
                cam.Target.TrackingTarget = transform;
            }
        }
    }
}
