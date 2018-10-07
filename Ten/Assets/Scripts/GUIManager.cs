using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GUIManager : MonoBehaviour {
    public Text endRoundText;
    public GameObject endRoundGUI;
    private void OnEnable()
    {
        GameManager.OnLostRound += LostRound;
        GameManager.OnWonRound += WonRound;
        GameManager.OnStartRound += StartRound;
    }
    private void OnDisable()
    {
        GameManager.OnLostRound -= LostRound;
        GameManager.OnWonRound -= WonRound;
        GameManager.OnStartRound += StartRound;
    }
    void LostRound()
    {
        endRoundText.text = "Lost Round";
        endRoundGUI.SetActive(true);
    }
    void WonRound()
    {
        endRoundText.text = "Won Round";
        endRoundGUI.SetActive(true);
    }
    void StartRound()
    {
        endRoundGUI.SetActive(false);
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
