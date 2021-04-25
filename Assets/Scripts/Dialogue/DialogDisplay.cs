using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]

public class QuestionEvent : UnityEvent<Question> { }

public class DialogDisplay : MonoBehaviour
{
    public Conversation conversation;
    public QuestionEvent questionEvent;
    public GameObject speakerLeft;
    public GameObject speakerRight;

    private SpeakerUI speakerUILeft;
    private SpeakerUI speakerUIRight;

    private bool conversationStarted = false;

    private int activeLineIndex = 0;

    public void ChangeConversation(Conversation nextConversation)
    {
        conversationStarted = false;
        conversation = nextConversation;
        advanceLine();
    }
    
    void Start()
    {
        speakerUILeft = speakerLeft.GetComponent<SpeakerUI>();
        speakerUIRight = speakerRight.GetComponent<SpeakerUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            advanceLine();
        } else if (Input.GetKeyDown("x"))
        {
            endConversation();
        }
    }

    void advanceConversation()
    {
        if(conversation.question != null)
        {
            Debug.Log("Invoke question");
            speakerUILeft.dialog.text = "";
            speakerUIRight.dialog.text = "";
            questionEvent.Invoke(conversation.question);
        }
        else if (conversation.nextConversation != null)
        {
            Debug.Log("Next convo");
            ChangeConversation(conversation.nextConversation);
        } else
        {
            endConversation();
        }
    }

    void endConversation()
    {
        conversation = null;
        conversationStarted = false;
        speakerUILeft.Hide();
        speakerUIRight.Hide();
    }

    void initialize()
    {
        conversationStarted = true;
        activeLineIndex = 0;
        speakerUILeft.Speaker = conversation.speakerLeft;
        speakerUIRight.Speaker = conversation.speakerRight;

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

        if(speakerUILeft.IsEqualsTo(character))
        {
            SetDialog(speakerUILeft, speakerUIRight, line.text);
        } else
        {
            SetDialog(speakerUIRight, speakerUILeft, line.text);
        }

        activeLineIndex++;
    }

    void SetDialog(SpeakerUI showSpeaker, SpeakerUI hideSpeaker, string text)
    {
        showSpeaker.Dialog = text;
        showSpeaker.Show();
        hideSpeaker.Hide();
    }
}
