using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject cycler;
    Vector3 dsp = new Vector3(-7,6,0);
    Quaternion rot = new Quaternion(12,0,0,0);
    // Update is called once per frame
    void Update()
    {
        transform.position = cycler.transform.position + dsp;
        transform.LookAt(cycler.transform);
    }
}
