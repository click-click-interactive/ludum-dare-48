using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject hero;
    public GameObject teenager;
    public GameObject dialogueManager;
    public Conversation conversationIntroduction;
    public Conversation conversationTeenagerChase;
    public Conversation conversationBeastIntroduction;
    public Conversation conversationNewHero;
    public Conversation conversationBattleOutro;
    public BoolVariable playerCanControl;
    public BoolVariable enemyCanControl;
    public bool conversationInProgress;
    public Camera mainCamera;

    public int step = 0;
    // Start is called before the first frame update
    void Start()
    {
        conversationInProgress = false;
        player.transform.position = new Vector3(-3.6f, 0.1f, 0);
        player = Instantiate(player);

        conversationInProgress = false;
        step = 1;

        waitState();
        progressState(1);
        /*conversationInProgress = true;
        dialogueManager.GetComponent<DialogDisplay>().LaunchConversation(conversationIntroduction);*/
    }

    private void waitState()
    {
        enemyCanControl.RuntimeValue = false;
        playerCanControl.RuntimeValue = false;
        
    }

    private void runState()
    {
        playerCanControl.RuntimeValue = true;
        enemyCanControl.RuntimeValue = true;
    }

    private void startConversation(Conversation conversation)
    {
        if(conversationInProgress == false)
        {
            waitState();
            conversationInProgress = true;
            dialogueManager.GetComponent<DialogDisplay>().LaunchConversation(conversation);
        }
        
    }

    public void conversationEnded(string conversationName)
    {
        //Debug.Log("Conversation " + conversationName + " ended");
        conversationInProgress = false;
        runState();
        
        switch(conversationName)
        {
            case "0.0_StoryIntroduction":
                progressState(2);
                break;
            case "0.1_TeenagerChase":
                progressState(4);
                break;
            case "0.2_BeastIntroduction":
                progressState(6);
                break;
            case "1.1_NewHero":
                progressState(8);
                break;
            case "2.4_BattleOutro":
                progressState(10);
                break;
        }
    }

    public void gameplayEventEnded(string eventName)
    {
        Debug.Log("Gameplay event " + name + " ended");
        if(eventName == "kill_hero")
        {
            progressState(7);
        }
    }

    private void progressState(int step)
    {
        Debug.Log("Step " + step);
        if (step == 1)
        {
            startConversation(conversationIntroduction);
        }

        if(step == 2)
        {
            // blank step
            teenager.transform.position = new Vector3(0f, 0.8f, 0);
            teenager = Instantiate(teenager);
            teenager.GetComponent<NPCBehavior>().dialogueManager = dialogueManager;
            runState();
            //step = 3;
        }

        if (step == 3)
        {
            startConversation(conversationTeenagerChase);    
            
        }

        if (step == 4)
        {
            // blank step
            Destroy(teenager);
            step = 5;
        }

        if (step == 5)
        {
            startConversation(conversationBeastIntroduction);
            
        }
        if( step == 6)
        {
            hero.transform.position = new Vector3(1.2f, 3.3f, 0);
            hero.GetComponent<EnemyBehavior>().gameManager = this;
            hero = Instantiate(hero);
            enemyCanControl.RuntimeValue = false;
        }

        if(step == 7)
        {
            startConversation(conversationNewHero);
        }
        if (step == 8)
        {
            // blank step
            step = 9;
        }
        if (step == 9)
        {
            waitState();
            player.transform.position = new Vector3(63.0f, 0f, player.transform.position.z);
            mainCamera.transform.position = new Vector3(63.7f, -1.35f, mainCamera.transform.position.z);
            runState();
            
            startConversation(conversationBattleOutro);
            
        }
        if(step == 10)
        {
            runState();
        }
    }


    // Update is called once per frame
    void Update()
    {
    }

    

    
}
