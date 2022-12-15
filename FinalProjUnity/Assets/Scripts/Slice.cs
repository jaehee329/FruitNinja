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
        // 서버에서 받아온 손의 좌표와 현재 위치를 통해 방향성 계산
        Direction = handDataScript.handPos - transform.position;

        // 현재 위치를 가장 최근의 손 위치로 업데이트
        transform.position = handDataScript.handPos;

        // Slice 모드
        if (handDataScript.handType == 0)
        {
            StartSlicing();
        }
        else if (handDataScript.handType == 1)
        {
            // Grab 모드
            StopSlicing();
        }
    }

    private void StartSlicing()
    {
        // slice 모드에서는 검 커서만 활성화함
        blade.SetActive(true);
        grab.SetActive(false);
        
        sliceCollider.enabled = true;
        sliceTrail.enabled = true;
    }

    private void StopSlicing()
    {
        // grab 모드에서는 손 커서만 활성화함
        blade.SetActive(false);
        grab.SetActive(true);

        sliceCollider.enabled = false;
        sliceTrail.enabled = false;
    }
}
