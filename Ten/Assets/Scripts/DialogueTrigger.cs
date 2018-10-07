using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class DialogueTrigger : MonoBehaviour,IInteractable {
    public Dialogue dialogue;
    public delegate void endConversation();
    public event endConversation OnEndConversationHandler;
    public GameObject interactableGUI;
    public bool triggerOnAwake = false;
    public AudioSource audioSource;
    float currTime = 3;
    public GameObject activatableGO;
    bool calledDialogue = false;
    bool isInDialogue = false;
    bool conversationEnded = false;
    [SerializeField] float triggerDistance = 4f;
    GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
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
    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, triggerDistance);
    }
    void Update()
    {
        //if player is near and button pressed trigger
        if(Vector3.Distance(player.transform.position,transform.position)<triggerDistance && Input.GetKeyDown(KeyCode.E) )
        {
            interactableGUI.SetActive(true);
            TriggerDialogue();
        }
        if(Vector3.Distance(player.transform.position, transform.position)<triggerDistance)
        {
            interactableGUI.SetActive(true);

        }
        else
        {
            interactableGUI.SetActive(false);

        }
        //trigger conversation
        if (conversationEnded)
        {
            currTime += Time.deltaTime;
        }

    }
    public void interact()
    {
        if(!isInDialogue && currTime > .2f)
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
        conversationEnded = true;
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
        conversationEnded = false;
        currTime = 0;
        DialogueManager.instance.StartDialogue(dialogue);
        //if(DoEndBehavior)
        DialogueManager.OnEndConversationHandler += EndConversationBehavior;
        DialogueManager.OnEndConversationHandler += UpdateDialogue;


    }

}
