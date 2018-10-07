using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GUIManager : MonoBehaviour {
    public Text endRoundText;
    //public Text irritatedText;
    public GameObject endRoundGUI;
    public GameObject irritatedText;
    public GameObject vaccuumedText;
    public static GUIManager instance;
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
        GameManager.OnStartRound -= StartRound;
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
        Debug.Log("endgameset active false");
    }
    public void ShowVaccumedText()
    {
        StartCoroutine(showTExt(1F, vaccuumedText));

    }
    public void ShowAnnoyedtext()
    {
        StartCoroutine(showTExt(1F,irritatedText));

    }
    IEnumerator showTExt(float waitTime,GameObject temp)
    {
        temp.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        temp.SetActive(false);
        //print("WaitAndPrint " + Time.time);
    }

private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
