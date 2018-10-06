using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class VacuumOnOffSwitch : MonoBehaviour
{
    public GameObject vacuumZone;
    private MeshRenderer vacuumZoneRenderer;
    private VacuumCollisionHandler vacuum;
    private AudioSource vacuumAudioSource;
    // Use this for initialization
    void Start()
    {
        vacuumAudioSource = GetComponent<AudioSource>();
        vacuum = vacuumZone.GetComponent<VacuumCollisionHandler>();
        vacuumZoneRenderer = vacuumZone.GetComponent<MeshRenderer>();
        vacuum.ison = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            vacuum.ison = !vacuum.ison;
            if (vacuum.ison)
            {
                vacuumAudioSource.Play();
                vacuumZoneRenderer.enabled = true;
            }
            else
            {
                vacuumAudioSource.Stop();
                vacuumZoneRenderer.enabled = false;
            }
        }
    }

}
