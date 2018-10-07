using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Face  { 
    public Sprite eyes;
    public Sprite body;
    public Sprite mouth;
    public Face(Sprite _eye, Sprite _body,Sprite _mouth)
    {
        eyes = _eye;
        body = _body;
        mouth = _mouth;
    }
    
}
