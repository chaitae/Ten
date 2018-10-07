using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostManager : MonoBehaviour {
    public GameObject ghostPrefab;
    public Sprite[] bodies;
    public Sprite[] eyeR;
    public Sprite[] eyeL;
    public Sprite[] mouth;
    public const int vipCount = 10;
    public static int totalIntruders = 0;
    [SerializeField]
    public  List<Face> vipList;
    public GameObject[] portraits;
    public static GhostManager instance;
    // Use this for initialization
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
    void Start () {
        for(int i = 0; i<vipCount; i++)
        {
            SpawnVIPList();
        }
        SetPortraits();
        SpawnIntruders();
        StartCoroutine(SpawnIntrudersPeriodically());

    }
    public void Reset()
    {
        vipList.Clear();
        for (int i = 0; i < vipCount; i++)
        {
            SpawnVIPList();
        }
        SetPortraits();
        SpawnIntruders();
        StartCoroutine(SpawnIntrudersPeriodically());

    }
    void SetPortraits()
    {
        for(int i = 0; i<portraits.Length;i++)
        {
            GameObject temp = portraits[i];
            SpriteRenderer tempEyeL = temp.transform.Find("ghostEyeL").GetComponent<SpriteRenderer>();
            SpriteRenderer tempEyeR = temp.transform.Find("ghostEyeR").GetComponent<SpriteRenderer>();
            SpriteRenderer tempMouth = temp.transform.Find("Mouth").GetComponent<SpriteRenderer>();
            tempEyeR.sprite = vipList[i].eyeR;
            tempEyeL.sprite = vipList[i].eyeL;
            tempMouth.sprite = vipList[i].mouth;
        }
    }
    bool CheckVIPContains(Face face)
    {
        foreach(Face temp in vipList)
        {
            if(temp.body == face.body && temp.eyeL == face.eyeL &&temp.eyeR == face.eyeR && temp.mouth == face.mouth)
            {
                return true;
            }
        }
        return false;
    }
    public void SpawnVIPList()
    {
        int randEyeL = Random.Range(0, eyeR.Length);
        int randEyeR = Random.Range(0, eyeR.Length);
        int randBody = Random.Range(0, bodies.Length);
        int randMouth = Random.Range(0, mouth.Length);
        randEyeL = 0;
        randEyeR = 0;
        randBody = 0;
        randMouth = 0;
        Face temp = new Face(eyeR[randEyeL],eyeR[randEyeR],bodies[randBody],mouth[randMouth]);
        while (CheckVIPContains(temp))
        {
            randEyeR = Random.Range(0, eyeR.Length);
            randEyeL = Random.Range(0, eyeR.Length);
            randBody = Random.Range(0, bodies.Length);
            randMouth = Random.Range(0, mouth.Length);
            temp = new Face(eyeR[randEyeL],eyeR[randEyeR], bodies[randBody], mouth[randMouth]);
        }
        vipList.Add(temp);
        GameObject temp2=Instantiate(ghostPrefab);
        temp2.GetComponent<Ghost>().SetFace(eyeR[randEyeL],eyeR[randEyeR],bodies[randBody],mouth[randMouth]);
        temp2.GetComponent<Ghost>().isImposter = false;
    }
    public void SpawnIntruders()
    {
        int randEyeL = Random.Range(0, eyeR.Length);
        int randEyeR = Random.Range(0, eyeR.Length);
        int randBody = Random.Range(0, bodies.Length);
        int randMouth = Random.Range(0, mouth.Length);
        randEyeL = 0;
        randEyeR = 0;
        randBody = 0;
        randMouth = 0;
        Face temp = new Face(eyeR[randEyeL], eyeR[randEyeR], bodies[randBody], mouth[randMouth]);
        while (CheckVIPContains(temp))
        {
            randEyeR = Random.Range(0, eyeR.Length);
            randEyeL = Random.Range(0, eyeR.Length);
            randBody = Random.Range(0, bodies.Length);
            randMouth = Random.Range(0, mouth.Length);
            temp = new Face(eyeR[randEyeL], eyeR[randEyeR], bodies[randBody], mouth[randMouth]);
        }
        vipList.Add(temp);
        GameObject temp2 = Instantiate(ghostPrefab);
        temp2.GetComponent<Ghost>().SetFace(eyeR[randEyeL],eyeR[randEyeL], bodies[randBody], mouth[randMouth]);
        temp2.GetComponent<Ghost>().isImposter = true;
        totalIntruders++;
    }

	void GenerateIntruders()
    {
        StartCoroutine(SpawnIntrudersPeriodically());
    }
    IEnumerator SpawnIntrudersPeriodically()
    {
        yield return new WaitForSeconds(2.0f);
        SpawnIntruders();
    }
}
