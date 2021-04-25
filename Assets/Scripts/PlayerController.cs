using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 3.0f;
    public int health = 5;
    public int damage = 3;
    public GameObject hintPrefab;
    public Canvas canvas;
    private GameObject hintInstance;
    private GameObject triggerInstance;
    private GameObject triggerEnemy;
    private CallableAction actionTarget;

    private Renderer renderer;
    public Color damageColor;
    private Color originalColor;

    // Start is called before the first frame update
    void Start()
    {
        renderer = this.gameObject.GetComponent<SpriteRenderer>();
        originalColor = renderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Z)) {
            transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.up, speed * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.S)) {
            transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.down, speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Q)) {
            transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.left, speed * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.D)) {
            transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.right, speed * Time.deltaTime);
        }

        if (actionTarget != null && Input.GetKeyDown(KeyCode.F) && triggerInstance != null)
        {
            triggerInstance.SendMessage("StartConversation");
            actionTarget.call();
        }

        if(Input.GetKeyDown(KeyCode.Space) && triggerEnemy != null)
        {
            triggerEnemy.SendMessage("receiveHit", damage);
        } 
    }

    void OnTriggerEnter2D(Collider2D other) {

        ActionTrigger actionTrigger = other.GetComponent<ActionTrigger>();
        if (actionTrigger) {
            this.actionTarget = actionTrigger.action;
            this.triggerInstance = other.gameObject;
            hintPrefab.GetComponentInChildren<TMP_Text>().text = this.actionTarget.getType();
            hintInstance = Instantiate(
                hintPrefab,
                other.gameObject.transform.position + (Vector3.right * 3),
                canvas.gameObject.transform.rotation,
                canvas.gameObject.transform
            );
        }

        if(other.gameObject.tag == "Enemy")
        {
            triggerEnemy = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        ActionTrigger actionTrigger = other.GetComponent<ActionTrigger>();
        if (actionTrigger) {
            if(this.triggerInstance == other.gameObject)
            {
                this.triggerInstance = null;
            }
            if (this.hintInstance) {
                Destroy(hintInstance);
            }
        }
    }

    public void receiveHit(int amount)
    {
        health -= amount;
        Debug.Log("Player hp : " + health);
        if (health <= 0)
        {
            Debug.Log("GAME OVER !");
        }
        renderer.material.color = damageColor;
        Invoke("RestoreMaterial", 0.0f);
    }

    private void RestoreMaterial()
    {
        renderer.material.color = originalColor;
    }
}
