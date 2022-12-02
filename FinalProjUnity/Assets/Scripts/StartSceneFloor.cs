using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneFloor : MonoBehaviour
{
    public GameObject watermelon;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        watermelon.transform.position = new Vector3(0f, 15f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("fruit"))
        {
            watermelon.transform.position = new Vector3(0f, 15f, 0f);
        }
    }
}
