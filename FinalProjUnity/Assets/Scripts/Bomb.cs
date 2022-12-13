using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.isSliceMode)
        {
            if (other.CompareTag("hand"))
            {
                Destroy(this.gameObject);
                GameManager.isGameOver = true;
            }
        }
    }
}
