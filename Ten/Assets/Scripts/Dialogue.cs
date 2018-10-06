using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[System.Serializable]
public class Dialogue{
    public string name;
    [TextArea(0, 10)]
    public string[] sentences;
    public string optionText;
    public GameObject activatableGO;
    public Dialogue[] options;
    public Dialogue(string[] tempSentences)
    {
        sentences = tempSentences;
        options = new Dialogue[0];
    }

}
