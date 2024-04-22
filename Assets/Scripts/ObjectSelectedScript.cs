using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectSelectedScript : MonoBehaviour
{
    public int ViolationsFound;
    public int TotalViolations;
    public Text Violations;
    public Text VMissed;
    public Text timerText;
    public float timer;
    public GameObject FinishPanel;
    public Text ProgressText;

    public bool myTimer = true;


    void Start()
    {
        // FinishPanel = GameObject.FindGameObjectWithTag("Finish");

    }

    
    void Update()
    {
        //timer
        RunTime();

        //check if no violations are missing
        if(VMissed.text == "Violations Missed: 0")
        {
            //stop timer
            myTimer = false;

            //activate finish panel & change game progress text
            FinishPanel.SetActive(true);
            ProgressText.text = "SIMULATION\nCOMPLETE";
        }
    }

    public void RunTime()
    {
        if(myTimer == true)
        {
            float t = Time.time;
            string min = ((int)t / 60).ToString();
            string sec = (t % 60).ToString("f0");
            timerText.text = "Total Time: " + min + ":" + sec;
        }
    }

    public void SetTotalViolations()
    {
        TotalViolations++;
        VMissed.text = "Violations Missed: " + TotalViolations.ToString();
    }

    public void ViolationSelected()
    {
        ViolationsFound++;
        Violations.text = "Violations Found: " + ViolationsFound.ToString();
        VMissed.text = "Violations Missed: " + (TotalViolations - ViolationsFound).ToString();
    }

}
