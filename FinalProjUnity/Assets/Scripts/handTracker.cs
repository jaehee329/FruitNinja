// For temporary hand coordinate visualization, attached object shows the hand position
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handTracker : MonoBehaviour
{
    FetchHandData hand;
    private Vector3 handPos;

    void Start()
    {
        
    }

    void Update()
    {
        handPos = GameObject.Find("GameManager").GetComponent<FetchHandData>().handPos;
        Debug.Log("handTracker.cs handPos = " + handPos);
        transform.position = handPos;
    }
}
