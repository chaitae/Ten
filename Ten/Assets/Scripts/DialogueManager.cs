using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
    private Queue<string> sentences;
    public static DialogueManager instance;
    bool inDialogue = false;
    public Text text;
    public GameObject dialogueBox;
    public Dialogue currDialogue;
    public GameObject optionButton;
    public GameObject optionButtonParent;
    public delegate void endConversation();
    public static event endConversation OnEndConversationHandler;
    public delegate void startConversation();
    public static event startConversation OnStartConversationHandler;
    public delegate void madeChoice();
    public static event madeChoice OnMadeChoiceHandler;
    float timePassed = 0;
    bool optionsShowing = false;
    //AudioSource audio;
    // Use this for initialization
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
        }
        else
        {
            //audio = this.GetComponent<AudioSource>();
            instance = this;
        }
    }
    public void SetOptions()
    {
        for(int i = 0; i<currDialogue.options.Length;i++)
        {
            GameObject temp = Instantiate(optionButton, optionButtonParent.transform);
            Text tempText = temp.GetComponentInChildren<Text>();
            Button tempButton = temp.GetComponent<Button>();
            Dialogue dialogueTemp = currDialogue.options[i];
            tempText.text = currDialogue.options[i].optionText;
            tempButton.onClick.AddListener(delegate{ OnOptionClick(dialogueTemp); });
        }
    }
    void OnOptionClick(Dialogue dialogue)
    {
        if (OnMadeChoiceHandler != null)
            OnMadeChoiceHandler();
        DestroyOptionButtons();
        StartDialogue(dialogue);
        if(dialogue.activatableGO != null)
        {
            dialogue.activatableGO.GetComponent<IActivatable>().Activate();
        }
        //if (currDialogue.dialogAction == DialogueAction.ChooseCatApppearance)
        //{
        //    Debug.Log("chose to be a cat");
        //}
    }
    void DestroyOptionButtons()
    {
        foreach (Transform child in optionButtonParent.transform)
        {
            Destroy(child.gameObject);
        }
    }
    public void StartDialogue(Dialogue dialogue)
    {
        DestroyOptionButtons();
        currDialogue = dialogue;
        timePassed = 0;
        //audio.Play();
    
        if(OnStartConversationHandler != null)
        {
            OnStartConversationHandler();
        }
        dialogueBox.SetActive(true);
        inDialogue = true;
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }
    void DisplayNextSentence()
    {
        if (currDialogue.options.Length > 0 && sentences.Count == 1)
        {
            optionsShowing = true;
            SetOptions();
        }
        else if(currDialogue.options.Length <=0)
        {
            optionsShowing = false;
        }
        if (sentences.Count ==0)
        {
            EndDialogue();
            return;
        }
        string sentence =sentences.Dequeue();
        text.text = sentence;
    }
    void EndDialogue()
    {
        if(OnEndConversationHandler != null)
        {
            OnEndConversationHandler();
        }
        dialogueBox.SetActive(false);
        inDialogue = false;
    }
    void Start () {
        sentences = new Queue<string>();
	}
	
	// Update is called once per frame
	void Update () {
		if(inDialogue)
        {
            timePassed += Time.deltaTime;
            if(Input.GetButtonDown("Jump"))
            {
                DisplayNextSentence();
            }
            if(Input.GetMouseButtonDown(0) && timePassed >= .2f && !optionsShowing)
            {
                DisplayNextSentence();
            }
        }
	}
    
}
