using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosion;

    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.isSliceMode)
        {
            
            if (other.CompareTag("hand"))
            {
                Debug.Log("slice has touched the bomb");
                Destroy(this.gameObject);
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
