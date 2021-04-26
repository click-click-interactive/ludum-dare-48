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
    public Conversation conversationInnerTheft;
    public Conversation conversationNewHero;
    public Conversation conversationInnerArrive;
    public Conversation conversationBattleOneWin;
    public Conversation conversationBattleOutro;
    public Conversation conversationCastleReturn;
    public Conversation conversationInnerArrive2;
    public Conversation conversationMagicDoorEnter;
    public Conversation conversationMagicDoorExit;
    public Conversation conversationBattleOutro2;
    public Conversation finalInnerTravel;
    public Conversation innerOpen;
    public Conversation demonKing;
    public Conversation lastWords;





    public BoolVariable playerCanControl;
    public BoolVariable enemyCanControl;
    public GameObject[] enemiesRoom1;
    public GameObject exitRoom1;
    public bool conversationInProgress;
    public Camera mainCamera;
    public Canvas canvas;

    public int step = 1;
    // Start is called before the first frame update
    void Start()
    {
        conversationInProgress = false;
        player.transform.position = new Vector3(-3.6f, 0.1f, 0);
        player.GetComponent<PlayerController>().gameManager = this;
        player.GetComponentInChildren<ActionListener>().canvas = canvas;
        player = Instantiate(player);


        conversationInProgress = false;
        //step = 1;

        waitState();
        progressState(step);
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

    public void startConversation(Conversation conversation)
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
        /*
            0.0_StoryIntroduction -> 0.0_StoryIntroduction->VISU 
            0.1_TeenagerChase -> 0.3_HeroIntroduction->VISU 
            0.4_InnerTheft -> 1.0_HeroBattle->BATTLE 1
            BATTLE 1 -> 1.1_NewHero->VISU
            1.1_NewHero -> 1.7.2_InvitetoCastle->VISU
            1.8_AtCastle -> 1.16_InnerStart->VISU
            2.0_InnerArrive -> 2.3_BattleIntro->BATTLE 2
            BATTLE 2 -> 2.3.1_BattleWin (dialogue in game)
            2.4 BattleOutro -> 2.5.3_Excuses.asset->VISU
            2.6_CastleReturn -> 2.6_CastleReturn->VISU
            3.0_InnerArrive -> 3.3_BattleIntro->BATTLE
            3.4_BattleOutro -> 3.5.7_SusSusMark->VISU
            3.6_CastleReturn->VISU
        4.0_InnerArrive -> 4.3_BattleIntro->BATTLE
        4.4_BattleOutro -> 4.5.9_InnerMark->VISU
        4.6_CastleReturn -> 4.6_CastleReturn->VISU
        5.0_InnerTravel -> 5.7_InnerOpen->VISU
        5.9_InnerBattle->BATTLE
        5.10_LastWords->FIN
        */
        Debug.Log("convo ended: " + conversationName);
        switch (conversationName)
        {
            
            case "0.0_StoryIntroduction":
                progressState(2);
                break;
            case "0.1_TeenagerChase":
                progressState(3);
                break;
            case "0.4_InnerTheft":
                progressState(4);
                break;
            case "1.1_NewHero":
                progressState(6);
                break;
            case "2.0_InnerArrive":
                progressState(7);
                break;
            case "2.3.1_BattleWin":
                progressState(9);
                break;
            case "2.4_BattleOutro":
                progressState(10);
                break;
            case "2.6_CastleReturn":
                progressState(11);
                break;
            case "3.0_InnerArrive":
                progressState(12);
                break;
            case "3.3.2_MagicDoorEnter":
                progressState(14);
                break;
            case "3.3.3_MagicDoorExit":
                progressState(15);
                break;
            case "3.4_BattleOutro":
                progressState(16);
                break;
            case "5.0_InnerTravel":
                progressState(17);
                break;
            case "5.7_InnerOpen":
                progressState(18);
                break;

        }
    }

    public void gameplayEventEnded(string eventName)
    {
        Debug.Log("Gameplay event " + eventName + " ended");
        switch(eventName)
        {
            case "kill_hero":
                progressState(5);
                break;
            case "win_circle_stepped":
                progressState(8);
                break;
            case "room_1_exit":
                progressState(10);
                break;
            case "magic_door_enter":
                progressState(13);
                break;
            case "trap_room_exit":
                progressState(15);
                break;
            case "demon_king":
                progressState(19);
                break;
        }
    }

    private void progressState(int step)
    {
        Debug.Log("Step " + step);
        if (step == 1)
        {
            startConversation(conversationIntroduction);
        }

        if (step == 2)
        {
            startConversation(conversationTeenagerChase);    
            
        }
        if(step == 3)
        {
            startConversation(conversationInnerTheft);
        }
        if (step == 4)
        {
            hero.transform.position = new Vector3(1.2f, 3.3f, 0);
            hero.GetComponent<EnemyBehavior>().gameManager = this;
            hero = Instantiate(hero);
            enemyCanControl.RuntimeValue = false;
        }

        if(step == 5)
        {
            startConversation(conversationNewHero);
        }

        if(step == 6)
        {
            startConversation(conversationInnerArrive);
        }
        if(step == 7)
        {
            waitState();
            player.transform.position = new Vector3(60.0f, 0f, player.transform.position.z);
            mainCamera.transform.position = new Vector3(60.0f, 0f, mainCamera.transform.position.z);

            foreach (GameObject e in enemiesRoom1)
            {
                e.SetActive(true);
            }
            // arrive dans la première salle
            runState();
        }
        if (step == 8)
        {
            foreach (GameObject e in enemiesRoom1)
            {
                e.GetComponent<EnemyBehavior>().speed = 0.0f;
                e.transform.eulerAngles = new Vector3(
                    e.transform.eulerAngles.x,
                    e.transform.eulerAngles.y,
                    e.transform.eulerAngles.y + 90);
                e.GetComponent<Animator>().enabled = false;
            }
            startConversation(conversationBattleOneWin);
        }
        if (step == 9)
        {
            exitRoom1.SetActive(true);

        }

        if (step == 10)
        {
            startConversation(conversationCastleReturn);
        }
        if(step == 11)
        {
            startConversation(conversationInnerArrive2);
        }
        if(step == 12)
        {
            waitState();
            player.transform.position = new Vector3(90.55f, 1.5f, player.transform.position.z);
            mainCamera.transform.position = new Vector3(98f, 0f, mainCamera.transform.position.z);

            // arrive dans la seconde salle
            runState();
        }
        if(step == 13)
        {
            startConversation(conversationMagicDoorEnter);
        }
        if(step== 14)
        {
            waitState();
            player.transform.position = new Vector3(103f, 3.0f, player.transform.position.z);
            startConversation(conversationMagicDoorExit);
            runState();
        }

        if(step==15)
        {
            startConversation(conversationBattleOutro2);
        }
        if(step == 16)
        {
            startConversation(finalInnerTravel);
        }
        if (step == 17)
        {
            startConversation(innerOpen);
        }
        if (step == 18)
        {
            Debug.Log("Spawn DemonKing and die !");
            waitState();
            mainCamera.transform.position = new Vector3(120f, 0f, mainCamera.transform.position.z);
            player.transform.position = new Vector3(120f, -1.7f, player.transform.position.z);
            startConversation(demonKing);
            runState();
            
        }
        if(step == 19)
        {
            player.GetComponent<PlayerController>().speed = 0.0f;
            player.transform.eulerAngles = new Vector3(
                player.transform.eulerAngles.x,
                player.transform.eulerAngles.y,
                player.transform.eulerAngles.y + 90);
            player.GetComponent<Animator>().enabled = false;
            startConversation(lastWords);
        }
    }


    // Update is called once per frame
    void Update()
    {
    }

    

    
}
