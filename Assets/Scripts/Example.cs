using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Example : MonoBehaviour
{
    
    public Animator[] m_Animator;
    public Text animatorSpeedText;

    
    public float m_MySliderValue;

    void Update()
    {
        animatorSpeedText.text = m_MySliderValue.ToString();
        foreach(Animator anim in m_Animator){
        anim.SetFloat("SpeedMultiplier", m_MySliderValue);

        }
    }

    void OnGUI()
    {
        
        GUI.Label(new Rect(0, 25, 40, 60), "Speed");
        
        m_MySliderValue = GUI.HorizontalSlider(new Rect(45, 25, 200, 60), m_MySliderValue, 0.0F, 10.0F);
       
        
    }
}