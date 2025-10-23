using UnityEngine;

public class FlightController : MonoBehaviour {
    Rigidbody plane;

    int currentthrottle = 0;
    int maxThrottle = 100;

    void Awake() {
        plane = gameObject.GetComponent<Rigidbody>();
    }
    void Movement() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            while (currentthrottle < maxThrottle) {
                currentthrottle++;
                break;
            }

        }
        plane.AddForce(Vector3.forward * currentthrottle * 10, ForceMode.Force);
    }

    void Update() {
        Movement();
        Debug.Log(currentthrottle + " " + plane.linearVelocity);
    }
}


