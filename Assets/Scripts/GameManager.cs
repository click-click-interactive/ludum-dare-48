using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public GameObject dialogueManager;
    public Conversation conversationIntroduction;
    private string currentConversation;
    private bool canContinue;


    private EnemyBehavior enemyBehavior;

    public string state = "init";
    public string chapter = "intro";
    public int step = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (chapter == "intro" && state == "init")
        {
            Debug.Log(conversationIntroduction);
            canContinue = false;
            player.transform.position = new Vector3(0.8f, 0.5f, 0);
            Instantiate(player);
            //dialogueManager.GetComponent<DialogDisplay>().LaunchConversation(conversationIntroduction);

            enemy.transform.position = new Vector3(3.5f, 0.6f, 0);
            Instantiate(enemy);
            enemyBehavior = enemy.GetComponent<EnemyBehavior>();
            enemyBehavior.setCanMove(false);

            currentConversation = conversationIntroduction.name;
            state = "wait";
            step = 1;
        }
        
        
    }

    private void waitState()
    {
        // todo : player cannot move
        foreach (GameObject e in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            e.SendMessage("toggleMove", false);
        }
    }

    private void runState()
    {
        // todo : player can move
        foreach (GameObject e in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            e.SendMessage("toggleMove", true);
        }
    }

    private void chapterIntro()
    {
        if(step == 1)
        {
            waitState();
            if (Input.GetKeyDown(KeyCode.H))
            {
                Debug.Log("run");
                state = "run";
                step = 2;
            }
        }

        if(step == 2)
        {
            runState();
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies == null || enemies.Length == 0)
            {
                Debug.Log("step2 : you've slain all enemies ! going to step 3");
                step = 3;
            }
        }

        if(step == 3)
        {
            Debug.Log("3");
            chapter = "trial1";
            step = 0;
            state = "init";
        }
        
        
    }

    private void chapterTrial1()
    {
        Debug.Log("chapter : " + chapter);
        Debug.Log("step : " + step);
        Debug.Log("state : " + state);
    }

    // Update is called once per frame
    void Update()
    {
        if(chapter == "intro")
        {
            chapterIntro();
        }
        if(chapter == "trial1")
        {
            chapterTrial1();
        }
        
    }

    public void conversationEnded(string conversationName)
    {
        if(conversationName == currentConversation)
        {
            currentConversation = null;
            canContinue = true;
            state = "run";
        }
    }
}
