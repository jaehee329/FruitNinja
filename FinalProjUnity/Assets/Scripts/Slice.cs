using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slice : MonoBehaviour
{
    public Camera mainCamera;
    private Collider sliceCollider;
    private TrailRenderer sliceTrail;
    private bool isSlicing;
    public float minSliceVelocity = 0.01f;

    public Vector3 Direction { get; private set; }
    public float sliceForce = 5f;

    private void Awake()
    {
        sliceCollider = GetComponent<Collider>();
        Debug.Log(sliceCollider.enabled);
        sliceTrail = GetComponentInChildren<TrailRenderer>();
    }

    private void OnEnable()
    {
        StopSlicing();
    }

    private void OnDisable()
    {
        StopSlicing();
    }

    void Update()
    {
        if (GameManager.isSliceMode)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartSlicing();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                StopSlicing();
            }
            else if (isSlicing)
            {
                ContinueSlicing();
            }
        }
    }

    private void StartSlicing()
    {
        Debug.Log("start slicing");
        Vector3 newPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPos.z = 0f;
        Debug.Log(newPos.ToString());

        transform.position = newPos;

        isSlicing = true;
        sliceCollider.enabled = true;
        sliceTrail.enabled = true;
        sliceTrail.Clear();
    }

    private void StopSlicing()
    {
        Debug.Log("stop slicing");
        isSlicing = false;
        sliceCollider.enabled = false;
        sliceTrail.enabled = false;
    }

    private void ContinueSlicing()
    {
        //Vector3 mp;
        //Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        //if (Physics.Raycast(ray, out RaycastHit raycastHit))
        //{

        //    mp = raycastHit.point;
        //    mp.z = -1f;
        //    Direction = mp - transform.position;

        //    float velocity = Direction.magnitude / Time.deltaTime;
        //    sliceCollider.enabled = velocity > minSliceVelocity;

        //    transform.position = mp;
        //}
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 21f;
        Vector3 newPos = mainCamera.ScreenToWorldPoint(mousePos);
        Direction = newPos - transform.position;
        Debug.Log(Direction.ToString());

        float velocity = Direction.magnitude / Time.deltaTime;
        sliceCollider.enabled = velocity > minSliceVelocity;

        transform.position = newPos;


    }
}
