using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public delegate void BaseEventNotifier();
    public static event BaseEventNotifier OnLostRound;
    public static event BaseEventNotifier OnWonRound;
    public static GameManager instance;
    public float currTime= 0;
    public float roundEndTime = 120;
    void Awake()
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
    public void WonRound()
    {

    }
    public void LostRound()
    {

    }
    public void Pause()
    {

    }
    public void Unpause()
    {

    }
    void EndRound()
    {
        Debug.Log("roundEnd");
    }
    void StartRound()
    {
        Time.timeScale = 1;
    }
    private void Update()
    {
        if(currTime >=roundEndTime)
        {
            EndRound();
            Time.timeScale = 0;
            //endRound
        }
        else
        {
            currTime += Time.deltaTime;
        }
    }
}
