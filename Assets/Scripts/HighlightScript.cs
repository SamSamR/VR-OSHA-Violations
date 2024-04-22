using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightScript : MonoBehaviour
{
    /*Psudo Code 
     * Purpose: To high light a violation or non violation that has child objects that also need to be highlighted
     * Code needs to:
     * Create a hover outline
     *      First:  Check if object has not been selected
     *      Second: give it an outline
     * 
     * Delete hover outline if objects not selected
     *      First:  Check if object has not been selected
     *      Second: If not selected Destroy outline
     * 
     * Change color of outline if selected
     *      First:  Get parents tag
     *      Second: Check if object has been selected before
     *                  If not check parents tag
     *                  If tag = Violation set outline color to green
     *                      set selected = true
     *                      add violation found to end screen
     *                  If tag = NonViolation set outline color to red
     */

    //violation scoreboard
    public GameObject endScreen;

    [SerializeField] private bool selected = false;
    [SerializeField] private Outline outline;


    // Start is called before the first frame update
    void Start()
    {
        endScreen = GameObject.Find("UI Screen"); //find UI screen

        //dynamically set total violations in a scene
        if (this.gameObject.tag == "Violation")
        {
            endScreen.GetComponent<ObjectSelectedScript>().SetTotalViolations();
            int totalviolations = endScreen.GetComponent<ObjectSelectedScript>().TotalViolations;

           // Debug.Log("Name of violations: " + this.gameObject.name);
        }
    }


    //called when player hovers over object
    public void HoverOutline()
    {
        if (selected == false)
        {
            //add outline component
            if(gameObject.GetComponent<Outline>() == false)
            {
                gameObject.AddComponent<Outline>();
                outline = gameObject.GetComponent<Outline>();
            }
            
            if(gameObject.GetComponent<Outline>() == true && outline.enabled == false)
            {
                outline.enabled = true;
            }

            //change its mode
            outline.OutlineMode = Outline.Mode.OutlineVisible;

            //change its width
            outline.OutlineWidth = 7f;

            //change its color
            outline.OutlineColor = Color.yellow;
        }

    }

    //destroys outline object when player is no longer hovered over object
    public void HoverOutlineDelete()
    {
        if (selected == false)
        {
            outline.enabled = false;
        }
    }


    //changes outline color of selected objects
    public void SelectedOutline()
    {
        string tag = this.gameObject.tag;
        if (selected == false)
        {
            if (tag == "Violation")
            {
                Debug.Log("VIolation");
                //change its color
                outline.OutlineColor = Color.green;

                selected = true;

                //count violations found 
                endScreen.GetComponent<ObjectSelectedScript>().ViolationSelected();
            }
        }

        if (tag == "NonViolation")
        {
            //change its color
            outline.OutlineColor = Color.red;
        }
    }

}
