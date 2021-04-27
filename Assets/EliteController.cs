using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteController : MonoBehaviour
{
    public float attackSpeed = 5.0f;
    public float secondsBetweenAttacks = 5.0f;
    public float stunTime = 5.0f;
    public bool stunned = false;
    public bool attacking = false;
    public Vector3 target = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!attacking) {
            target = FindObjectOfType<PlayerController>().transform.position;
        }
    }

    public void StartWaiting()
    {
        target = FindObjectOfType<PlayerController>().transform.position;
        StartCoroutine(WaitForAttack());
    }

    IEnumerator WaitForAttack()
    {
        Debug.Log("Wait for attack...");
        target = FindObjectOfType<PlayerController>().transform.position;

        yield return new WaitForSeconds(secondsBetweenAttacks);

        target = (target - transform.position).normalized;
        attacking = true;
        StartCoroutine(Attack());
    }

    IEnumerator WaitForStun()
    {
        Debug.Log("Stunned! Waiting for end of stun...");

        yield return new WaitForSeconds(stunTime);

        stunned = false;
        StartCoroutine(WaitForAttack());
    }

    IEnumerator Attack()
    {
        Debug.Log("Attack!");

        while (!stunned && attacking) {
            transform.position = transform.position + target * attackSpeed * Time.deltaTime;
            yield return null;
        }
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag != "Player") {
            stunned = true;
            StartCoroutine(WaitForStun());

            Debug.Log("Stunned!");
        } else {
            StartCoroutine(WaitForAttack());

            Debug.Log("Hit player!");
        }

        attacking = false;
    }
}
