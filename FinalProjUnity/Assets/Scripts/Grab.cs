using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    private FetchHandData handDataScript;

    private void Awake()
    {
        handDataScript = GameObject.Find("GameManager").GetComponent<FetchHandData>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = handDataScript.handPos;
    }
}
