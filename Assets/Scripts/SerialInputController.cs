using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;
using UnityEngine.UI;


public class SerialInputController : MonoBehaviour
{
    public Animator[] animationControllers;
    public GameObject endscr;
    public Text tmr;
    public Text dst;
    public Text enddst;

    public AudioSource audioSource;
    public AudioClip clip;
    public JSONManager jsonman;
    public const string portName = "COM12";

    private SerialPort serialPort;
    private List<byte> inputBuffer;
    private int messageLength = 4;
    private bool isSerialPortOpen = false;

    public float remtime = 60;
    public float dstcov = 1;

    private const float minSpeed = 1f;
    private const float maxSpeed = 12f;
    private const float transitionDuration = 0.5f;
    public float fnalspeed;
    public saver sav;
    private bool initialized = false;

    private void Start()
    {
        audioSource.clip = clip;

    }
    private void Update()
    {
        if (!initialized)
        {
            OpenSerialPort(portName);
            inputBuffer = new List<byte>(1000);
            endscr.SetActive(false);
            initialized = true;
        }

        if (isSerialPortOpen && serialPort.IsOpen)
        {
            ReadSerialData();
            ProcessSerialData();
            
            remtime -= Time.deltaTime;
            tmr.text = ((int)remtime).ToString();
            dst.text = dstcov.ToString() + " m";

            if ((int)remtime == 0)
            {
                UpdateAnimationSpeed(0);
                audioSource.Stop();
                CloseSerialPort();
                endscr.SetActive(true);
                sav.setSer(dstcov);
                enddst.text = "distance covered:" + dstcov.ToString() + " m";
            }
        }
    }

    public float SetDistCov()
    {   //if((int)remtime == 0)
        return dstcov;
    }
    public void WriteJSONFile(string playerName)
    {
        jsonman.AddPlayer(playerName, dstcov);
    }

    private void OnDestroy()
    {
        CloseSerialPort();
    }

    private void OpenSerialPort(string portName)
    {
        try
        {
            serialPort = new SerialPort(portName, 57600);
            serialPort.Open();
            isSerialPortOpen = true;
            audioSource.Play();
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error opening serial port: " + e.Message);
        }
    }

    private void CloseSerialPort()
    {
        if (isSerialPortOpen)
        {
            serialPort.Close();
            isSerialPortOpen = false;
        }
    }

    private void ReadSerialData()
    {
        try
        {
            while (serialPort.BytesToRead >= messageLength)
            {
                byte[] buffer = new byte[messageLength];
                serialPort.Read(buffer, 0, messageLength);
                inputBuffer.AddRange(buffer);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error reading serial input: " + e.Message);
        }
    }

    private void ProcessSerialData()
    {
        while (inputBuffer.Count > 0)
        {
            int newlineIndex = inputBuffer.IndexOf((byte)'\n');

            if (newlineIndex != -1)
            {
                byte[] buffer = new byte[newlineIndex];
                inputBuffer.CopyTo(0, buffer, 0, newlineIndex);
                inputBuffer.RemoveRange(0, newlineIndex + 1);

                string asciiString = System.Text.Encoding.ASCII.GetString(buffer);

                string[] numbers = asciiString.Split('-');

                List<int> integerValues = new List<int>();
                foreach (string number in numbers)
                {
                    int value;
                    if (int.TryParse(number, out value))
                    {
                        integerValues.Add(value);
                    }
                }

                foreach (int speed in integerValues)
                {
                    float targetSpeed = MapSerialInputToSpeed(speed);
                    fnalspeed = targetSpeed;
                    UpdateAnimationSpeed(targetSpeed);
                }

                if (fnalspeed != 0)
                {
                    dstcov += (fnalspeed * (60 - remtime)) / 10;
                }
            }
            else
            {
                break;
            }
        }
    }

    private void UpdateAnimationSpeed(float targetSpeed)
    {
        foreach (Animator animController in animationControllers)
        {
            animController.SetFloat("SpeedMultiplier", targetSpeed);
        }
    }

    private float MapSerialInputToSpeed(int serialInput)
    {
        if (serialInput == 0)
        {
            return 0f;
        }
        else
        {
            // Debug.Log(Mathf.Lerp(minSpeed, maxSpeed, Mathf.Clamp01((float)serialInput / 4.0f)));
            return Mathf.Lerp(minSpeed, maxSpeed, Mathf.Clamp01((float)serialInput / 4.0f));
        }
    }
}
