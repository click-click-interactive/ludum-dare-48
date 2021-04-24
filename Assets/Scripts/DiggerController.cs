using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiggerController : MonoBehaviour
{
    public GameObject CloseArm;
    public GameObject FarArm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float moveSpeed = 1f;
        if (Input.GetKey(KeyCode.Z)) {
            CloseArm.transform.rotation = Quaternion.Lerp(CloseArm.transform.rotation, Quaternion.Euler(0, 0, -180), moveSpeed * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.S)) {
            CloseArm.transform.rotation = Quaternion.Lerp(CloseArm.transform.rotation, Quaternion.Euler(0, 0, 0), moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Q)) {
            FarArm.transform.rotation = Quaternion.Lerp(FarArm.transform.rotation, Quaternion.Euler(0, 0, -90), moveSpeed * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.D)) {
            FarArm.transform.rotation = Quaternion.Lerp(FarArm.transform.rotation, Quaternion.Euler(0, 0, 90), moveSpeed * Time.deltaTime);
        }
    }

}
