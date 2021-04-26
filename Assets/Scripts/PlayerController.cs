using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 3.0f;
    public int health = 5;
    public int damage = 3;
    private GameObject triggerEnemy;
    private Animator animator;

    private Renderer renderer;
    public Color damageColor;
    private Color originalColor;

    public GameManager gameManager;

    public BoolVariable canControl;
    private bool winCirclePressed = false;
    private bool roomOneExited = false;
    private bool magicalDoorTaken = false;
    private bool trapRoomExited = false;

    public Conversation conversationTrapScare;
    // Start is called before the first frame update
    void Start()
    {
        renderer = this.gameObject.GetComponent<SpriteRenderer>();
        originalColor = renderer.material.color;
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        bool moved = false;

        if (canControl.RuntimeValue == true) {
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

            if(Input.GetKeyDown(KeyCode.F) && triggerEnemy != null)
            {
                triggerEnemy.SendMessage("receiveHit", damage);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("coll  " + collision);
        if (collision.gameObject.tag == "Enemy")
        {
            triggerEnemy = collision.gameObject;
        }

        if(collision.gameObject.tag == "WinCircle" && !winCirclePressed)
        {
            Debug.Log("winCircleCollision ! ");
            gameManager.SendMessage("gameplayEventEnded", "win_circle_stepped");
            winCirclePressed = true;
        }
        if(collision.gameObject.tag == "ExitTrigger" && !roomOneExited)
        {
            Debug.Log("ExitTrigger ! ");
            gameManager.SendMessage("gameplayEventEnded", "room_1_exit");
            roomOneExited = true;
        }
        if(collision.gameObject.tag == "MagicDoor" && !magicalDoorTaken)
        {
            Debug.Log("MagicDoor ! ");
            gameManager.SendMessage("gameplayEventEnded", "magic_door_enter");
            magicalDoorTaken = true;
        }
        if(collision.gameObject.tag == "TrapScareTrigger")
        {
            gameManager.startConversation(conversationTrapScare);
        }
        if(collision.gameObject.tag == "TrapRoomExitTrigger" && !trapRoomExited)
        {
            gameManager.SendMessage("gameplayEventEnded", "trap_room_exit");
            trapRoomExited = true;
        }
        if(collision.gameObject.tag == "demonking")
        {
            gameManager.SendMessage("gameplayEventEnded", "demon_king");
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(triggerEnemy != null & collision.gameObject.tag == "Enemy")
        {
            triggerEnemy = null;
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
        Invoke("RestoreMaterial", 0.05f);
    }

    private void RestoreMaterial()
    {
        renderer.material.color = originalColor;
    }
}
