//VacuumTargetScript
//Programmer: Rob Garner (rgarner011235@gmail.com)
//Date: 6 Oct 2018
//Purpose: Handle collisions when vacuum cleaner encounters objects.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumTargetScript : MonoBehaviour {


    //Method called when vacuum collides with target
    public void VacuumedAction()
    {
        Debug.Log(this.name + " vacuumed");
        StartCoroutine(WaitAndDestroy(3));        
    }

    private IEnumerator WaitAndDestroy(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
}
