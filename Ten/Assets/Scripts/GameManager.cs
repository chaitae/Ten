using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public delegate void BaseEventNotifier();
    public static event BaseEventNotifier OnLostRound;
    public static event BaseEventNotifier OnWonRound;
    public static event BaseEventNotifier OnEndRound;
    public static event BaseEventNotifier OnStartRound;
    public static GameManager instance;
    public UnityStandardAssets.Characters.FirstPerson.FirstPersonController firstPersonController;
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
    public void ChangeScene(int i)
    {
        SceneManager.LoadScene(i);
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
        //Cursor.visible = true;
        //UnityStandardAssets.Characters.FirstPerson.FirstPersonController firstPersController = GameObject.FindObjectOfType<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
        currTime = 0;
        firstPersonController.DisableFPSMouse();
        if (OnEndRound != null)
        {
            OnEndRound();
        }
        Debug.Log("roundEnd");
        if(GhostManager.totalIntruders >0)
        {
            if(OnLostRound != null)
            {
                OnLostRound();
            }
            Debug.Log("yahlost");
        }
        else
        {
            if(OnEndRound != null)
            {
                OnEndRound();
            }
            Debug.Log("you wonnn");
        }
    }
    public void StartRound()
    {
        if (OnStartRound != null)
        {
            OnStartRound();
        }
        firstPersonController.EnableFPSMouse();
        GhostManager.instance.Reset();
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
