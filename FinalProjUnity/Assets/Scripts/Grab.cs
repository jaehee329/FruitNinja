using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    private FetchHandData handDataScript;
    private bool isGrabing;
    private Collider grabingObj;

    void Start()
    {
        handDataScript = GameObject.Find("GameManager").GetComponent<FetchHandData>();
    }

    void Update()
    {
        if (GameManager.isSliceMode)
        {
            this.gameObject.GetComponent<Collider>().isTrigger = false;
            isGrabing = false;
            grabingObj = null;
        }
        else
        {
            if (isGrabing)
            {
                this.gameObject.GetComponent<Collider>().isTrigger = true;
                if (grabingObj)
                {
                    grabingObj.transform.position = handDataScript.handPos;
                }

            }
            else
            {
                isGrabing = false;
                grabingObj = null;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("fruit") || other.CompareTag("bomb"))
        {
            if (!GameManager.isSliceMode)
            {
                isGrabing = true;
                grabingObj = other;
            }
            
        }
    }
}
