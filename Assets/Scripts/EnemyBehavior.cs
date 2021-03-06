using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

    public int health = 5;
    public int damage = 1;
    
    private Renderer renderer;
    private Color originalColor;
    public Color damageColor;

    private float hitLast = 0;
    public float hitDelay = 1.0f;

    public float speed = 1.0f;
    public float proximityThreshold = 1.0f;
    private Animator animator;
    private string orientation;
    private string previousOrientation;

    private Vector3 target;
    public BoolVariable canMove;

    public GameManager gameManager;
    

    public void setCanMove(bool value)
    {
        this.canMove.RuntimeValue = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        renderer = this.gameObject.GetComponent<SpriteRenderer>();
        originalColor = renderer.material.color;
        animator = GetComponent<Animator>();
        previousOrientation = "NONE";
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(canMove.RuntimeValue)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform.position;
            if (target != null)
            {
                if (Vector3.Distance(target, transform.position) > proximityThreshold)
                {
                    Vector3 direction = (target - transform.position).normalized;
                    transform.position = transform.position + direction * speed * Time.deltaTime;

                    orientation = getOrientation(direction);

                    if (previousOrientation != orientation)
                    {
                        if (orientation == "UP")
                        {
                            animator.SetInteger("direction", (int)PlayerDirection.Up);
                        }
                        if (orientation == "RIGHT")
                        {
                            animator.SetInteger("direction", (int)PlayerDirection.Right);
                        }
                        if (orientation == "DOWN")
                        {
                            animator.SetInteger("direction", (int)PlayerDirection.Down);
                        }
                        if (orientation == "Left")
                        {
                            animator.SetInteger("direction", (int)PlayerDirection.Left);
                        }
                        if (orientation == "NEUTRAL")
                        {
                            animator.SetInteger("direction", (int)PlayerDirection.None);
                        }
                        previousOrientation = orientation;
                    }
                }
                else
                {
                    animator.SetInteger("direction", (int)PlayerDirection.None);
                    previousOrientation = "NEUTRAL";
                    if (hitLast == 0)
                    {
                        hitLast = Time.time;
                    }
                    if (Time.time - hitLast > hitDelay)
                    {
                        GameObject.FindGameObjectWithTag("Player").SendMessage("receiveHit", damage);
                        hitLast = Time.time;
                    }
                }
            }
        }   
    }

    private string getOrientation(Vector3 direction)
    {
        float absX = Math.Abs(direction.x);
        float absY = Math.Abs(direction.y);

        if(absX > absY)
        {
            if(direction.x > 0)
            {
                return "RIGHT";
            } 
            else
            {
                return "LEFT";
            }
        }
        else if (absY > absX)
        {
            if(direction.y > 0)
            {
                return "UP";
            }
            else
            {
                return "DOWN";
            }
        }
        return "NEUTRAL";
    }


    public void receiveHit(int amount)
    {
        health -= amount;
        if(health <= 0)
        {
            Debug.Log(this.name.Replace("(Clone)", ""));
            if (this.name.Replace("(Clone)", "") == "Hero")
            {
                gameManager.SendMessage("gameplayEventEnded", "kill_hero");
            }
            Destroy(gameObject);
        }
        renderer.material.color = damageColor;
        Invoke("RestoreMaterial", 0.05f);
    }

    private void RestoreMaterial()
    {
        renderer.material.color = originalColor;
    }

    
}
