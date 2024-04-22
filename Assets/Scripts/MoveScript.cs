using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class MoveScript : MonoBehaviour
{
    public Rigidbody myRig;
    [SerializeField] private float speed;
    [SerializeField] private int i = 0;
    [SerializeField] private GameObject GoalList;
    [SerializeField] private Transform[] GoalArray;
    [SerializeField] private Transform NextGoal;
    


    void Start()
    {
        myRig = this.gameObject.GetComponent<Rigidbody>();

        if (myRig == null)
        {            
            throw new System.Exception("Could not find Ridgedbody"); //throw exception
        }


        //find list of goals and put into array
        GoalList = GameObject.Find("GoalList");
        GoalArray = GoalList.GetComponentsInChildren<Transform>(); //index 0 = parent object
        //transform.position //world space

        NextGoal = GoalArray[1].transform;
        i++;
        Debug.Log("Goal: " + NextGoal.position);
    }

    
    void Update()
    {
        
        if(i<= 62)
        {
            var step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(NextGoal.position.x, NextGoal.position.y, NextGoal.position.z), step);

            if (Vector3.Distance(transform.position, NextGoal.position) < 0.6f)
            {
                NextGoal = GoalArray[i].transform;
                Debug.Log("Goal: " + NextGoal.position);
                i++;
            }
        }

    }
}
