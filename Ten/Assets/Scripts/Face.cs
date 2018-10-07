using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Face  { 
    public Sprite eyeR;
    public Sprite eyeL;
    public Sprite body;
    public Sprite mouth;
    public Face(Sprite _eyeR,Sprite _eyeL, Sprite _body,Sprite _mouth)
    {
        eyeR = _eyeR;
        eyeL = _eyeL;
        body = _body;
        mouth = _mouth;
    }
    
}
