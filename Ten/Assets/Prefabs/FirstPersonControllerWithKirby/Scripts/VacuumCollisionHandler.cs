//VacuumCollisionHandler
//Programmer: Rob Garner (rgarner011235@gmail.com)
//Date: 6 Oct 2018
//Purpose: Handle collisions when vacuum cleaner encounters objects.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Place this class in the collision box of the vacuum.
/// </summary>
public class VacuumCollisionHandler : MonoBehaviour {
    private void Start()
    {
        Debug.Log("Vacuum Collision Handler Instantiated!");
    }
    //Handle the collision 
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("entered");
        VacuumTargetScript targetManager = other.GetComponent<VacuumTargetScript>();       

        if (targetManager != null)
        {
            other.gameObject.transform.SetParent(this.gameObject.transform);
            targetManager.VacuumedAction();
        }
    }
}
