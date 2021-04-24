using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiggerController : MonoBehaviour
{
    public GameObject CloseArm;
    public GameObject FarArm;
    public float speed = 1f;
    public float rotationSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Z)) {
            CloseArm.transform.rotation = Quaternion.Lerp(CloseArm.transform.rotation, Quaternion.Euler(0, 0, -180), rotationSpeed * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.S)) {
            CloseArm.transform.rotation = Quaternion.Lerp(CloseArm.transform.rotation, Quaternion.Euler(0, 0, 0), rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Q)) {
            FarArm.transform.rotation = Quaternion.Lerp(FarArm.transform.rotation, Quaternion.Euler(0, 0, -90), rotationSpeed * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.D)) {
            FarArm.transform.rotation = Quaternion.Lerp(FarArm.transform.rotation, Quaternion.Euler(0, 0, 90), rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftArrow)) {
            transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.left, speed * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.RightArrow)) {
            transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.right, speed * Time.deltaTime);
        }
    }

}
