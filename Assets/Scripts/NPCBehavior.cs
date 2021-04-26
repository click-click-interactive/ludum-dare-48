using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehavior : MonoBehaviour
{
    public List<Conversation> conversations;
    private int conversationIndex = 0;
    public GameObject dialogueManager;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartConversation()
    {
        if (conversationIndex < conversations.Count) {
            dialogueManager.GetComponent<DialogDisplay>().LaunchConversation(conversations[conversationIndex]);
            conversationIndex++;
        }
    }
}
