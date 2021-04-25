using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 3.0f;

    public GameObject hintPrefab;
    public Canvas canvas;
    private GameObject hintInstance;
    private CallableAction actionTarget;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool moved = false;

        if (Input.GetKey(KeyCode.Z)) {
            transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.up, speed * Time.deltaTime);
            moved = true;
            animator.SetInteger("direction", (int) PlayerDirection.Up);
        } else if (Input.GetKey(KeyCode.S)) {
            transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.down, speed * Time.deltaTime);
            moved = true;
            animator.SetInteger("direction", (int) PlayerDirection.Down);
        }

        if (Input.GetKey(KeyCode.Q)) {
            transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.left, speed * Time.deltaTime);
            moved = true;
            animator.SetInteger("direction", (int) PlayerDirection.Left);
        } else if (Input.GetKey(KeyCode.D)) {
            transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.right, speed * Time.deltaTime);
            moved = true;
            animator.SetInteger("direction", (int) PlayerDirection.Right);
        }

        if (!moved) {
            animator.SetInteger("direction", (int) PlayerDirection.None);
        }

        if (actionTarget != null && Input.GetKeyDown(KeyCode.F))
        {
            actionTarget.call();
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        ActionTrigger actionTrigger = other.GetComponent<ActionTrigger>();
        if (actionTrigger && actionTrigger.action) {
            this.actionTarget = actionTrigger.action;
            hintPrefab.GetComponentInChildren<TMP_Text>().text = this.actionTarget.getType();
            hintInstance = Instantiate(
                hintPrefab,
                other.gameObject.transform.position + (Vector3.right * 3),
                canvas.gameObject.transform.rotation,
                canvas.gameObject.transform
            );
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        ActionTrigger actionTrigger = other.GetComponent<ActionTrigger>();
        if (actionTrigger) {
            if (this.hintInstance) {
                Destroy(hintInstance);
            }
        }
    }
}
