using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public int cameraSpeed = 4;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        UpdateCamera();
        UpdateMouse();
    }

    void UpdateCamera() {
        Vector3 cameraMovement = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            cameraMovement.z += cameraSpeed;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            cameraMovement.x -= cameraSpeed;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            cameraMovement.z -= cameraSpeed;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            cameraMovement.x += cameraSpeed;
        }
        transform.Translate(cameraMovement);
    }

    void UpdateMouse() {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit)) {
                Transform hitTransform = hit.transform;
                hitTransform.Translate(0, -10, 0);
            }
        }
    }
}
