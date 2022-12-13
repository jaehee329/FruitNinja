using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneFloor : MonoBehaviour
{
    public GameObject watermelon;

    void Start()
    {
        Time.timeScale = 1;
        watermelon.transform.position = new Vector3(0f, 15f, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("fruit"))
        {
            watermelon.transform.position = new Vector3(0f, 15f, 0f);
        }
    }
}
