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
<<<<<<< HEAD

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
=======
    IInteractable vaccuumInteractable;
    private void Start()
    {
        Debug.Log("Vacuum Collision Handler Instantiated!");
    }
    //Handle the collision 
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("entered");
        vaccuumInteractable = other.GetComponent<IInteractable>();
        if (vaccuumInteractable != null)
        {
            //vaccuumInteractable.interact();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        IInteractable temp = other.GetComponent<IInteractable>();
        if(temp != null)
        {
            vaccuumInteractable = null;
        }
    }
    public void Update()
    {
        if(Input.GetMouseButton(0) && vaccuumInteractable != null)
        {
            vaccuumInteractable.interact();
>>>>>>> d1ece5362cd20fee926a910387ce10a35b9c4020
        }
    }
}
