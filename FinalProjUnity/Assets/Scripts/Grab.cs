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
            // slice 모드일 때 grab object 의 collider 의 isTrigger 를 비활성화해줌
            this.gameObject.GetComponent<Collider>().isTrigger = false;

            // grab 하는 상태가 아니며, 잡고 있는 오브젝트는 없음
            isGrabing = false;
            grabingObj = null;
        }
        else
        {
            // Grab 모드
            if (isGrabing)
            {
                this.gameObject.GetComponent<Collider>().isTrigger = true;
                if (grabingObj)
                {
                    // 잡고 있는 오브젝트의 위치를 손 위치와 동일하게 함
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
