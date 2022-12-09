using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slice : MonoBehaviour
{
    public Camera mainCamera;
    private Collider sliceCollider;
    public TrailRenderer sliceTrail;
    public float minSliceVelocity = 0.01f;

    public Vector3 Direction { get; private set; }
    public float sliceForce = 5f;

    private FetchHandData handDataScript;

    public GameObject blade;
    public GameObject grab;

    private void Start()
    {
        transform.position = new Vector3(0f, 0f, 0f);
        this.gameObject.SetActive(true);
        handDataScript = GameObject.Find("GameManager").GetComponent<FetchHandData>();
        sliceCollider = GetComponent<Collider>();
        Debug.Log(sliceCollider.enabled);
    }

    void Update()
    {
        transform.position = handDataScript.handPos;
        // Hand Mode
        if (handDataScript.handType == 0)
        {
            if (true)
            {
                StartSlicing();
            }
            // StopSlicing doesn't happen
            //if (isSlicing)
            //{
            //    ContinueSlicing();
            //}
        }
        else if (handDataScript.handType == 1)
        {
            StopSlicing();

        }
    }

    private void StartSlicing()
    {
        // Hand Mode
        blade.SetActive(true);
        grab.SetActive(false);
        
        //Debug.Log("Slice.cs handPos = " + handDataScript.handPos);


        sliceCollider.enabled = true;
        sliceTrail.enabled = true;
        //sliceTrail.Clear();
    }

    private void StopSlicing()
    {
        Debug.Log("stop slicing");
        blade.SetActive(false);
        grab.SetActive(true);
        //isSlicing = false;
        //sliceCollider.enabled = false;
        //sliceTrail.enabled = false;
    
    }
}
