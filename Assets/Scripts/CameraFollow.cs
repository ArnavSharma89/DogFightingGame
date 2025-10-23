using UnityEngine;

public class CameraFollow : MonoBehaviour {
    //the difference between the position of the camera and the player;
    Vector3 offset;

    //Main Camera of the scene;
    public Camera main;

    void Start() {
        offset = main.transform.position - transform.position;
    }

    void Update() {
        main.transform.position = transform.position + offset;
    }
}
