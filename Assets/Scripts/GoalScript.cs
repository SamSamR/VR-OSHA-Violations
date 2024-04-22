using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    public GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.Find("VR Rig").transform.GetChild(0).GetChild(2).GetChild();
        waitOneSec();
        player = GameObject.Find("Right Hand Presence(Clone)");
    }

    IEnumerator waitOneSec()
    {
        yield return new WaitForSeconds(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
