using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostManager : MonoBehaviour {
    public GameObject ghostPrefab;
    public Sprite[] bodies;
    public Sprite[] eyes;
    public Sprite[] mouth;
    public const int vipCount = 10;
    public static int totalIntruders = 0;
    [SerializeField]
    public  List<Face> vipList;
    public GameObject[] portraits;
	// Use this for initialization
	void Start () {
        for(int i = 0; i<vipCount; i++)
        {
            SpawnVIPList();
        }
        SetPortraits();
        SpawnIntruders();
    }
    void SetPortraits()
    {
        for(int i = 0; i<portraits.Length;i++)
        {
            GameObject temp = portraits[i];
            SpriteRenderer tempEyes = temp.transform.Find("ghostEye").GetComponent<SpriteRenderer>();
            SpriteRenderer tempMouth= temp.transform.Find("Mouth").GetComponent<SpriteRenderer>();
            tempEyes.sprite = vipList[i].eyes;
            tempMouth.sprite = vipList[i].mouth;
            //SpriteRenderer
        }
    }
    bool CheckVIPContains(Face face)
    {
        foreach(Face temp in vipList)
        {
            if(temp.body == face.body && temp.eyes == face.eyes && temp.mouth == face.mouth)
            {
                return true;
            }
        }
        return false;
    }
    public void SpawnVIPList()
    {
        int randEye = Random.Range(0, eyes.Length);
        int randBody = Random.Range(0, bodies.Length);
        int randMouth = Random.Range(0, mouth.Length);
        randEye = 0;
        randBody = 0;
        randMouth = 0;
        Face temp = new Face(eyes[randEye],bodies[randBody],mouth[randMouth]);
        while (CheckVIPContains(temp))
        {
            randEye = Random.Range(0, eyes.Length);
            randBody = Random.Range(0, bodies.Length);
            randMouth = Random.Range(0, mouth.Length);
            temp = new Face(eyes[randEye], bodies[randBody], mouth[randMouth]);
        }
        vipList.Add(temp);
        GameObject temp2=Instantiate(ghostPrefab);
        temp2.GetComponent<Ghost>().SetFace(eyes[randEye], bodies[randBody],mouth[randMouth]);
        temp2.GetComponent<Ghost>().isImposter = false;
    }
    public void SpawnIntruders()
    {
        int randEye = Random.Range(0, eyes.Length);
        int randBody = Random.Range(0, bodies.Length);
        int randMouth = Random.Range(0, mouth.Length);
        randEye = 0;
        randBody = 0;
        randMouth = 0;
        Face temp = new Face(eyes[randEye], bodies[randBody], mouth[randMouth]);
        while (CheckVIPContains(temp))
        {
            randEye = Random.Range(0, eyes.Length);
            randBody = Random.Range(0, bodies.Length);
            randMouth = Random.Range(0, mouth.Length);
            temp = new Face(eyes[randEye], bodies[randBody], mouth[randMouth]);
        }
        vipList.Add(temp);
        GameObject temp2 = Instantiate(ghostPrefab);
        temp2.GetComponent<Ghost>().SetFace(eyes[randEye], bodies[randBody], mouth[randMouth]);
        temp2.GetComponent<Ghost>().isImposter = true;
        totalIntruders++;
    }

	void GenerateGhosts()
    {

    }
}
