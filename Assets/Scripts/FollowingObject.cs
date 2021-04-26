using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingObject : MonoBehaviour
{
  public GameObject objectToFollow;
  public float speed = 1.0f;

  public void Update()
  {
    transform.position = new Vector3(
      objectToFollow.transform.position.x,
      objectToFollow.transform.position.y,
      transform.position.z
    );
  }
}