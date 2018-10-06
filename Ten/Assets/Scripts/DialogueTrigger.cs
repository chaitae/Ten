using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class DialogueTrigger : MonoBehaviour,IInteractable {
    public Dialogue dialogue;
    public delegate void endConversation();
    public event endConversation OnEndConversationHandler;
    public bool triggerOnAwake = false;
    public AudioSource audioSource;
    float currTime = 0;
    public GameObject activatableGO;
    bool calledDialogue = false;
    bool isInDialogue = false;
    void OnEnable()
    {
        DialogueManager.OnEndConversationHandler += SetDialogueOff;
        DialogueManager.OnStartConversationHandler +=SetDialogueOn;
    }
    void SetDialogueOn()
    {
        isInDialogue = true;
    }
    void SetDialogueOff()
    {
        isInDialogue = false;
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            TriggerDialogue();
        }
        currTime += Time.deltaTime;
        if(currTime >=.3f && triggerOnAwake && !calledDialogue)
        {
            calledDialogue = true;
            TriggerDialogue();
        }

    }
    public void interact()
    {
        if(!isInDialogue)
        TriggerDialogue();
    }

    public void interact(KeyCode key)
    {
        audioSource = GetComponent<AudioSource>();
        if(audioSource != null)
        audioSource.Play();
        if(key == KeyCode.E)
        {
            TriggerDialogue();

        }
    }
    void UpdateDialogue()
    {
        dialogue = DialogueManager.instance.currDialogue;
        Dialogue[] bah = new Dialogue[0];
    }
    public void EndConversationBehavior()
    {
        if (OnEndConversationHandler != null)
            OnEndConversationHandler();
        if (activatableGO != null)
        {
            IActivatable[] activatables = activatableGO.GetComponents<IActivatable>();
            foreach(IActivatable activatable in activatables)
            {
                activatable.Activate();
            }

        }
        DialogueManager.OnEndConversationHandler -= EndConversationBehavior;
        DialogueManager.OnEndConversationHandler -= UpdateDialogue;
    }
    public void TriggerDialogue()
    {
        DialogueManager.instance.StartDialogue(dialogue);
        //if(DoEndBehavior)
        DialogueManager.OnEndConversationHandler += EndConversationBehavior;
        DialogueManager.OnEndConversationHandler += UpdateDialogue;


    }

}
