using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private List<Band> bands;
    private Band selected;
    public Headquarters hq;

    public float cameraSpeed = 4;
    public int startingBands = 3;
    public GameObject bandPrefab;

    public int money;

    // Use this for initialization
    void Start() {
        CreateInitialBands();

    }

    void CreateInitialBands() {
        //TODO: Implement once Band prefab is ready.
    }

    // Update is called once per frame
    void Update() {
        UpdateCamera();
        UpdateMouse();
        UpdateSelection();
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

    void UpdateSelection() {
        if (bands[1] && Input.GetKeyDown(KeyCode.Alpha1)) {
            selected = bands[1];
        }
        if (bands[2] && Input.GetKeyDown(KeyCode.Alpha2)) {
            selected = bands[2];
        }
        if (bands[3] && Input.GetKeyDown(KeyCode.Alpha3)) {
            selected = bands[3];
        }
        if (bands[4] && Input.GetKeyDown(KeyCode.Alpha4)) {
            selected = bands[4];
        }
        if (bands[5] && Input.GetKeyDown(KeyCode.Alpha5)) {
            selected = bands[5];
        }
        if (bands[6] && Input.GetKeyDown(KeyCode.Alpha6)) {
            selected = bands[6];
        }
        if (bands[7] && Input.GetKeyDown(KeyCode.Alpha7)) {
            selected = bands[7];
        }
        if (bands[8] && Input.GetKeyDown(KeyCode.Alpha8)) {
            selected = bands[8];
        }
        if (bands[9] && Input.GetKeyDown(KeyCode.Alpha9)) {
            selected = bands[9];
        }
        if (bands[0] && Input.GetKeyDown(KeyCode.Alpha0)) {
            selected = bands[0];
        }
    }
}