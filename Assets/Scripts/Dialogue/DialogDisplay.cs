using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public class QuestionEvent : UnityEvent<Question> { }

public class DialogDisplay : MonoBehaviour
{
    public Conversation conversation;
    public QuestionEvent questionEvent;
    public GameObject speaker;
    //public BoolVariable playerCanControl;
    private SpeakerUI speakerUI;
    public GameObject gameManager;

    public bool conversationStarted;
    private string startedConversationName = "";

    private int activeLineIndex = 0;

    public void LaunchConversation(Conversation convo)
    {
        //Debug.Log("launch convo : " + convo.name);
        //playerCanControl.RuntimeValue = false;
        this.conversation = convo;
        this.startedConversationName = convo.name;
        advanceLine();
    }

    public void ChangeConversation(Conversation nextConversation)
    {
        conversationStarted = false;
        conversation = nextConversation;
        advanceLine();
    }
    
    void Start()
    {
        speakerUI = speaker.GetComponent<SpeakerUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(conversationStarted)
        {
            if (Input.GetKeyDown("space"))
            {
                advanceLine();
            }
            else if (Input.GetKeyDown("x"))
            {
                endConversation();
            }
        }
    }

    void advanceConversation()
    {
        if(conversation.question != null)
        {
            speakerUI.dialog.text = "";
            questionEvent.Invoke(conversation.question);
        }
        else if (conversation.nextConversation != null)
        {
            Debug.Log("changing converstation");
            Debug.Log("from : " + conversation.name);
            Debug.Log("to: " + conversation.nextConversation.name);
            ChangeConversation(conversation.nextConversation);
        } else
        {
            endConversation();
            gameManager.GetComponent<GameManager>().SendMessage("conversationEnded", this.startedConversationName);
            
        }
    }

    void endConversation()
    {
        conversation = null;
        conversationStarted = false;
        speakerUI.Hide();
        //playerCanControl.RuntimeValue = true;
    }

    void initialize()
    {
        conversationStarted = true;
        activeLineIndex = 0;
        speakerUI.Show();
    }

    private void advanceLine()
    {
        if(conversation == null)
        {
            return;
        }
        if(!conversationStarted)
        {
            initialize();
        }
        if(activeLineIndex < conversation.lines.Length)
        {
            displayLine();
        } else
        {
            advanceConversation();
        }
    }

    void displayLine()
    {
        Line line = conversation.lines[activeLineIndex];
        Character character = line.character;

        SetDialogContent(speakerUI, line);

        if (activeLineIndex < conversation.lines.Length - 1 && (conversation.question != null || conversation.nextConversation != null))
        {
            speakerUI.hint.SetActive(true);
        }
        else {
            speakerUI.hint.SetActive(false);
        }

        activeLineIndex++;
    }

    void SetDialogContent(SpeakerUI showSpeaker, Line line)
    {
        showSpeaker.Dialog = line.text;
        showSpeaker.fullName.text = line.character.fullName;
        showSpeaker.portrait.sprite = line.character.portrait;
    }
}
