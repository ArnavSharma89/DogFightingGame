using TMPro;
using Unity.Mathematics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class PhysFlightControl : MonoBehaviour {
    public float thrust;
    public float thrust_mult;
    public float yaw_mult;
    public float pitch_mult;

    private quaternion stableRot;
    Rigidbody rb;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        stableRot = transform.rotation;
    }

    void FixedUpdate() {
        float pitch = Input.GetAxis("Vertical");
        float yaw = Input.GetAxis("Horizontal");
        float roll  = 0f;
        if (Input.GetKey(KeyCode.Q)) roll = 1f;
        else if (Input.GetKey(KeyCode.E)) roll = -1f;
        rb.AddForce(thrust * thrust_mult * Vector3.forward);
        Vector3 torque =
            (pitch * pitch_mult * Vector3.right) +   // pitch nose up/down
            (yaw * yaw_mult * Vector3.up) +          // yaw left/right
            (roll * 10f * Vector3.forward);    // roll wings

        // rb.AddTorque(pitch * pitch_mult , yaw * yaw_mult, -yaw * yaw_mult * 2);

        rb.AddTorque(torque);
    }
}
