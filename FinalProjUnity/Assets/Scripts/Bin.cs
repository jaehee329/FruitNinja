using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bomb"))
        {
            GameManager.collectedBomb++;
            Destroy(other.gameObject);
        }
    }
}
