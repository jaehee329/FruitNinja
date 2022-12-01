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
        if (GameManager.isSliceMode)
        {
            
            if (other.CompareTag("slice"))
            {
                Debug.Log("slice has touched the bomb");
                GameManager.isGameOver = true;
            }

        }
        else
        {
            // Grab mode 일 때
            if (other.CompareTag("grab"))
            {
                Debug.Log("Grab object has grabbed the bomb");
                // change bomb position according to the position of the Grab object
            }
        }
        
    }
}
