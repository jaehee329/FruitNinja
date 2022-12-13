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
    public float sliceForce;

    private FetchHandData handDataScript;

    public GameObject blade;
    public GameObject grab;

    private void Start()
    {
        transform.position = new Vector3(0f, 0f, 0f);
        this.gameObject.SetActive(true);
        handDataScript = GameObject.Find("GameManager").GetComponent<FetchHandData>();
        sliceCollider = GetComponent<Collider>();
    }

    void Update()
    {
        Direction = handDataScript.handPos - transform.position;
        transform.position = handDataScript.handPos;

        if (handDataScript.handType == 0)
        {
            StartSlicing();
        }
        else if (handDataScript.handType == 1)
        {
            StopSlicing();
        }
    }

    private void StartSlicing()
    {
        blade.SetActive(true);
        grab.SetActive(false);
        
        sliceCollider.enabled = true;
        sliceTrail.enabled = true;
    }

    private void StopSlicing()
    {
        blade.SetActive(false);
        grab.SetActive(true);

        sliceCollider.enabled = false;
        sliceTrail.enabled = false;
    }
}
