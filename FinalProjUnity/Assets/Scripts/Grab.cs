using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    private FetchHandData handDataScript;
    private bool isGrabing;
    private Collider grabingObj;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        handDataScript = GameObject.Find("GameManager").GetComponent<FetchHandData>();
        //this.gameObject.SetActive(false);
        //transform.position = new Vector3(0f, 0f, 0f);
    }

    // Update is called once per frame
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
        Debug.Log("grabbed an object");
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
