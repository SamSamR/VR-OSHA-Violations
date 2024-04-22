using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{

    //get values from player UI
    public GameObject UIScreen;
    public Text otherTimer;

    public int ViolationsFound;
    public int ViolationsMissed;
    public int TotalViolations;
    public Text Violations;
    public Text VMissed;
    public Text timerText;

    public GameObject AllFoundText;    //text for all violations found
    public GameObject MissedText;    //text for not all violations found

    //world height of camera
    public GameObject mainCamera;



    void Start()
    {
        
    }


    void Update()
    {
        //if this panel is activated 
        if(this.gameObject.activeSelf == true)
        {
            activateEndScreen();
        }
    }


    //activate endscreen
    public void activateEndScreen()
    {
        this.gameObject.SetActive(true);
        this.gameObject.transform.position = new Vector3(this.transform.position.x, mainCamera.transform.position.y, this.transform.position.z);


        //set player info text
        //violations
        ViolationsFound = UIScreen.GetComponent<ObjectSelectedScript>().ViolationsFound;
        Violations.text = ViolationsFound.ToString();

        //violations missed
        TotalViolations = UIScreen.GetComponent<ObjectSelectedScript>().TotalViolations;
        ViolationsMissed = TotalViolations - ViolationsFound;
        VMissed.text = ViolationsMissed.ToString();

        //timer
        timerText.text = otherTimer.text.ToString();


        //set text
        //all violations found
        if (TotalViolations == ViolationsFound)
        {
            AllFoundText.SetActive(true);
            MissedText.SetActive(false);
        }
        //not all violations found
        else
        {
            MissedText.SetActive(true);
            AllFoundText.SetActive(false);
        }

    }


    //deactivate end screen
    public void deactivateEndScreen()
    {
        this.gameObject.SetActive(false);
    }


    //restart level
    public void RestartLevel()
    {
        //loads scene by name
        SceneManager.LoadScene("VrTest");
    }

    //Close program
    public void CloseProgram()
    {
        Application.Quit();
    }
}
