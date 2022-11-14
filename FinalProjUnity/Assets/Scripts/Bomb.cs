using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private DragDrop GrabModeScript;

    private void Awake()
    {
        GrabModeScript = gameObject.GetComponent<DragDrop>();
        GrabModeScript.enabled = false;
    }

    private void Update()
    {
        if (!GameManager.isSliceMode)
        {
            // Grab mode 일 때
            GrabModeScript.enabled = true;
        }
        else
        {
            // Slice mode 일 땡
            GrabModeScript.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("slice"))
        {
            Debug.Log("You've sliced the bomb!");
        }
    }
}
