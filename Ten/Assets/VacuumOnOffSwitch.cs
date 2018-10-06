using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumOnOffSwitch : MonoBehaviour
{
    private AudioSource _audio;
    private bool audiotoggle = false;
    // Use this for initialization
    void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            audiotoggle = !audiotoggle;

            if (audiotoggle)
            {
                _audio.Play();
            }
            else
            {
                _audio.Stop();
            }
        }
    }

}
