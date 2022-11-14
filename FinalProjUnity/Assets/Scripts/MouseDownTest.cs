using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MouseDownTest : MonoBehaviour
{
    Vector3 newPosition;

    void Start()
    {
        newPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("mouse down");
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                newPosition = hit.point;
                transform.position = newPosition;
            }
        }
    }
}
