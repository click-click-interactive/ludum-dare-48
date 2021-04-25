using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

    public int health = 5;
    public int damage = 1;
    
    private GameObject playerTrigger;
    private Renderer renderer;
    private Color originalColor;
    public Color damageColor;
    // Start is called before the first frame update
    void Start()
    {
        renderer = this.gameObject.GetComponent<SpriteRenderer>();
        originalColor = renderer.material.color;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            playerTrigger = other.gameObject;
            //StartCoroutine("Attack");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("TriggerExit");
        if(other.gameObject == playerTrigger)
        {
            playerTrigger = null;
            //StopAllCoroutines();
        }
    }

    IEnumerator Attack()
    {
        if(playerTrigger != null)
        {
            playerTrigger.SendMessage("receiveHit", damage);
        }

        yield return new WaitForSeconds(2);
    }

    public void receiveHit(int amount)
    {
        health -= amount;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
        Debug.Log("Enemy hp : " + health);
        renderer.material.color = damageColor;
        Invoke("RestoreMaterial", 0.05f);
    }

    private void RestoreMaterial()
    {
        renderer.material.color = originalColor;
    }
}
