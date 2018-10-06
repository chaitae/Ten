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

    public bool ison=false;

    //Handle the collision 
    private void OnTriggerEnter(Collider other)
    {
        if (ison)
        {
            IInteractable vaccuumInteractable = other.GetComponent<IInteractable>();
            if (vaccuumInteractable != null)
            {
                vaccuumInteractable.interact();
            }
        }
    }
}
