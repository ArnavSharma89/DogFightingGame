using System;
using TMPro;
using UnityEngine;

public class FlightController : MonoBehaviour {
    private Rigidbody rb;

    private int throttle;
    private int maxThrottle;

    void Awake() {
        rb = GetComponent<Rigidbody>();
        throttle = 0;
        maxThrottle = 100;
    }

    void Update() {
        updateThrottle();
    }

    private void updateThrottle() {
        if (Input.GetKey(KeyCode.Space)) {
            throttle = Mathf.Min(throttle + 1, maxThrottle);
        } else if (Input.GetKey(KeyCode.LeftControl)) {
            throttle = Mathf.Max(throttle - 1, 0);
        }
    }
}

