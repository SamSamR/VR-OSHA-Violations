using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ObjectHighlightScript : MonoBehaviour
{
    [SerializeField] private Material outlineMaterial;
    [SerializeField] private Vector3 outlineScaleFactor;
    [SerializeField] private Color outlineColor;
    private Renderer outlineRenderer;

    [SerializeField] private GameObject outlineObject;
    [SerializeField] private bool selected = false; //has player selected an object?

    //violation scoreboard
    public GameObject endScreen;

    // Start is called before the first frame update
    void Start()
    {
        endScreen = GameObject.Find("End Screen"); //find end screen

        //dynamically set total violations in a scene
        if(this.gameObject.tag == "Violation")
        {
            endScreen.GetComponent<ObjectSelectedScript>().SetTotalViolations();
            int totalviolations = endScreen.GetComponent<ObjectSelectedScript>().TotalViolations;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }


    //called when player hovers over object
    public void HoverOutline()
    {
        if(selected == false) //check to see if object has been selected already
        {
            outlineRenderer = CreateOutline(outlineMaterial, outlineScaleFactor, outlineColor);
            outlineRenderer.enabled = true;
        }      
    }

    //creates outline around object. Needs: Material, size of outline, color of outline
    Renderer CreateOutline(Material outlineMat, Vector3 ScaleFactor, Color color)
    {
        //Instantiates a copy of object to turn it into the outline
        outlineObject = Instantiate(this.gameObject, transform.localPosition, this.transform.localRotation, transform);        
        Renderer Rend = outlineObject.GetComponent<Renderer>(); //gets renderer of the outline object

        Rend.material = outlineMat; //Sets outline objects material
        Rend.material.SetColor("OutlineColor", color); //Sets outline objects color

        //scale outline based on object size (Default scale factor / scale of parent = scale factor)
        Vector3 defaultScale = Rend.material.GetVector("OutlineScale");
        Vector3 parentScale = new Vector3(this.gameObject.transform.localScale.x, this.gameObject.transform.localScale.y, this.gameObject.transform.localScale.z);
        ScaleFactor = new Vector3(defaultScale.x / parentScale.x, defaultScale.y / parentScale.y, defaultScale.z / parentScale.z);

        Rend.material.SetVector("OutlineScale", ScaleFactor); //Sets outline objects size

        Rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off; //Turns off shadows for outline object


        //disables outline objects ObjectHighlight script
        if (outlineObject.GetComponent<ObjectHighlightScript>() == true)
        {
            outlineObject.GetComponent<ObjectHighlightScript>().enabled = false;
        }
        
        //disables outline objects collider
        if(outlineObject.GetComponent<Collider>() == true)
        {
            outlineObject.GetComponent<Collider>().enabled = false;
        }
        

        Rend.enabled = false;

        return Rend;
    }


    //destroys outline object when player is no longer hovered over object
    public void HoverOutlineDelete()
    {
        if(selected == false)
        {
            Destroy(outlineObject);
        }  
    }


    //changes outline color of selected objects
    public void SelectedOutline()
    {
        string tag = this.gameObject.tag; //get tag

        //make sure object has a tag
        if(tag == "Untagged")
        {
            Debug.Log("object has no tag: " + this.gameObject.name);
        }

        if(selected == false) //check to see if object has been selected before
        {
            //if tag is violation turn color green
            if (tag == "Violation")
            {
                Color color = new Vector4(0, 1, 0, 1); //green
                Renderer Rend = outlineObject.GetComponent<Renderer>();
               // SkinnedMeshRenderer Rend = outlineObject.GetComponent<SkinnedMeshRenderer>();
                Rend.material.SetColor("OutlineColor", color); //Sets outline objects color

                selected = true; //keeps highlight from being destroied

                //count violations found 
                endScreen.GetComponent<ObjectSelectedScript>().ViolationSelected();

                
            }
        }
        

        //if tag is nonviolation turn color red
        if (tag == "NonViolation")
        {
            Color color = new Vector4(1, 0, 0, 1); //red
            Renderer Rend = outlineObject.GetComponent<Renderer>();
            Rend.material.SetColor("OutlineColor", color); //Sets outline objects color
        }
    }
}
