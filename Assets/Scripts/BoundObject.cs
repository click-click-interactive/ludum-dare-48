using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BoundObject : MonoBehaviour
{
    public GameObject boundObject;
    public Transform boundPoint;
    private Camera _camera;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (boundPoint && boundObject) {
            boundObject.transform.position = boundPoint.position;
        }

        if (boundObject.transform.hasChanged) {
        }
    }

     public Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles) {
        return Quaternion.Euler(angles) * (point - pivot) + pivot;
    }

    void FixedUpdate()
    {
    }

    void OnMouseDrag()
    {
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(
            new Vector3(boundPoint.position.x, boundPoint.position.y - 0.05f),
            new Vector3(boundPoint.position.x, boundPoint.position.y + 0.05f)
        );
        Gizmos.DrawLine(
            new Vector3(boundPoint.position.x - 0.05f, boundPoint.position.y),
            new Vector3(boundPoint.position.x + 0.05f, boundPoint.position.y)
        );
    }
}
