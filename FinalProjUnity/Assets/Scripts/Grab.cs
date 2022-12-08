using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    private FetchHandData handDataScript;

    private void Awake()
    {
        handDataScript = GameObject.Find("GameManager").GetComponent<FetchHandData>();
        this.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.isSliceMode)
        {
            this.enabled = true;
            transform.position = handDataScript.handPos;
        } else
        {
            this.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!GameManager.isSliceMode)
        {
            Debug.Log("grabbed an object");
            other.transform.position = this.transform.position;
            // other rigid body 없애주기?
        }
    }
}
