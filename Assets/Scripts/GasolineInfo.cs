using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GasolineInfo : MonoBehaviour
{
    public static float currentGasoline = 10f;
    public float countDown;
    public float baseInterval = 1f;
    public TextMeshProUGUI gasDisplay;
    bool isCarMoving;


    
    void Start()
    {
        isCarMoving = true;
        countDown = baseInterval;
    }

  
    void Update()
    {
        if (ObjectSpawner.Instance._gameEnded == true) return;
        if (currentGasoline <=0)
        {
             ObjectSpawner.Instance._gameEnded= true;
            return;
        }
        if (isCarMoving)
        {
            if (countDown > 0)
            {
                countDown -= Time.deltaTime;
            }
            else
            {
                countDown = baseInterval;
                currentGasoline -= 1f;
            }
            
        }
         
        gasDisplay.text = "Gasoline: " + currentGasoline + " L";
    }
}
