using UnityEngine;

public class CameraFollow : MonoBehaviour {
    Vector3 offset;
    public Camera main;

    void Start() {
        offset = main.transform.position - transform.position;
    }

    void Update() {
        main.transform.position = transform.position + offset;
    }
}
