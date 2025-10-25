// using UnityEngine;
// using System;
// using TMPro;
// public class FlightController : MonoBehaviour {

//     [Header("Gui")]
//     public TextMeshProUGUI t;

//     [Header("Physics")]
//     Rigidbody plane;
//     float LiftCoefficient = 0.9f;
//     public Transform CoM;

//     int currentthrottle;
//     int maxThrottle = 100;

//     void Awake() {
//         plane = gameObject.GetComponent<Rigidbody>();
//         currentthrottle = 0;
//         t.color = Color.green;
//         plane.centerOfMass = CoM.position; // lower CoM below geometric center
//     }
//     void Movement() {
//         if (Input.GetKeyDown(KeyCode.Space)) {
//             float cT = MathF.Min(currentthrottle + 1, maxThrottle);
//             currentthrottle = (int)cT;
//         }
//         //Forward Force
//         plane.AddForce(Vector3.forward * currentthrottle * 10, ForceMode.Force);

//         //Lift Force
//         plane.AddForce(Vector3.up * LiftCoefficient * currentthrottle, ForceMode.Force);
//     }

//     void Update() {
//         t.SetText("Throttle " + currentthrottle + "\n" + "Velocity " + plane.linearVelocity);
//         Movement();
//     }
// }
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ArcadeFlightController : MonoBehaviour {
    Rigidbody plane;

    [Header("Movement Settings")]
    [SerializeField] float acceleration = 25f;
    [SerializeField] float deceleration = 20f;
    [SerializeField] float maxSpeed = 500f;
    [SerializeField] float minSpeed = 30f;
    [SerializeField] float rotationSpeed = 80f;
    [SerializeField] float rollSpeed = 90f;

    [Header("Stability")]
    [SerializeField] float autoLevelStrength = 2f;
    [SerializeField] bool autoLevel = true;

    float currentSpeed;

    void Awake() {
        plane = GetComponent<Rigidbody>();
        plane.useGravity = false; // arcade planes fly freely
        currentSpeed = minSpeed;
    }

    void FixedUpdate() {
        HandleThrottle();
        HandleRotation();
        MovePlane();

        if (autoLevel)
            ApplyAutoLeveling();
    }

    void HandleThrottle() {
        if (Input.GetKey(KeyCode.Space))
            currentSpeed = Mathf.Min(currentSpeed + acceleration * Time.fixedDeltaTime, maxSpeed);
        else if (Input.GetKey(KeyCode.LeftShift))
            currentSpeed = Mathf.Max(currentSpeed - deceleration * Time.fixedDeltaTime, minSpeed);
    }

    void HandleRotation() {
        float pitch = 0f;
        float yaw = 0f;
        float roll = 0f;

        // Pitch (W/S)
        if (Input.GetKey(KeyCode.W)) pitch = 1f;
        if (Input.GetKey(KeyCode.S)) pitch = -1f;

        // Yaw (A/D)
        if (Input.GetKey(KeyCode.A)) yaw = -1f;
        if (Input.GetKey(KeyCode.D)) yaw = 1f;

        // Roll (Q/E)
        if (Input.GetKey(KeyCode.Q)) roll = 1f;
        if (Input.GetKey(KeyCode.E)) roll = -1f;

        // Smooth rotation using Transform.Rotate
        Vector3 rotation = new Vector3(pitch * rotationSpeed, yaw * rotationSpeed, roll * rollSpeed);
        transform.Rotate(rotation * Time.fixedDeltaTime, Space.Self);
    }

    void MovePlane() {
        // Constant forward motion
        plane.linearVelocity = -transform.forward * currentSpeed;
    }

    void ApplyAutoLeveling() {
        // Auto-stabilize roll and pitch gradually
        Vector3 localRot = transform.localEulerAngles;
        if (localRot.x > 180) localRot.x -= 360;
        if (localRot.z > 180) localRot.z -= 360;

        // Apply gradual correction
        float rollCorrection = -localRot.z * autoLevelStrength * Time.fixedDeltaTime;
        float pitchCorrection = -localRot.x * (autoLevelStrength / 2f) * Time.fixedDeltaTime;

        transform.Rotate(pitchCorrection, 0f, rollCorrection, Space.Self);
    }

    void Update() {
        Debug.Log($"Speed: {currentSpeed:F1} m/s");
    }
}
