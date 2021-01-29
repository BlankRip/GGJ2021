using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour
{
    [SerializeField] GameObject sequence;
    TypeWriteText myDialogues;
    bool conversationInProgress;
    bool canTrigger;


    private void Start()
    {
        myDialogues = GetComponent<TypeWriteText>();
        conversationInProgress = false;
        canTrigger = true;
    }


    private void Update()
    {
        if (conversationInProgress && Input.GetKeyDown(KeyCode.E))
        {
            conversationInProgress = myDialogues.TextInteraction();
        }
    }

    public void Trigger()
    {
        sequence.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Check if player is triggering a conversation and repeat if the conversation is replayable
        if (canTrigger && other.CompareTag("Player"))
        {
            GameManager.instance.playerScript.LockMovement();
            canTrigger = myDialogues.StartDialogue();
            conversationInProgress = true;
        }
    }
}