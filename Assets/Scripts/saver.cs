using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class saver : MonoBehaviour
{
    public Text name;
    public float ser;
    public JSONManager jsonman;

    
    public void setSer(float x)
    {
        ser = x;
    }
    public void onclicksubmit()
    {
        
        jsonman.AddPlayer(name.text,ser);
    }
}
