using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fruit : MonoBehaviour
{
    public GameObject whole;
    public GameObject sliced; // parent object

    private Rigidbody fruitRigidBody;
    private Collider fruitCollider;
    private DragDrop GrabModeScript;
    private Fruit SliceModeScript;

    private void Awake()
    {
        fruitRigidBody = GetComponent<Rigidbody>();
        fruitCollider = GetComponent<Collider>();
        //GrabModeScript = gameObject.GetComponent<DragDrop>();
        SliceModeScript = gameObject.GetComponent<Fruit>();
        //GrabModeScript.enabled = false;
        SliceModeScript.enabled = true;
    }

    private void Update()
    {
        if (!GameManager.isSliceMode)
        {
            // Grab mode 일 때
            //GrabModeScript.enabled = true;
            SliceModeScript.enabled = false;
        }
        else
        {
            // Slice mode 일 땡
            SliceModeScript.enabled = true;
        }
    }

    private void Slice(Vector3 direction, Vector3 contactPoint, float force)
    {
        whole.SetActive(false);
        sliced.SetActive(true);

        fruitCollider.enabled = false;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        sliced.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Rigidbody[] slices = sliced.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody slice in slices)
        {
            slice.velocity = fruitRigidBody.velocity;
            slice.AddForceAtPosition(direction * force, contactPoint, ForceMode.Impulse);
        }
        GetComponent<AudioSource>().Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("slice"))
        {
            Slice slice = other.GetComponent<Slice>();
            Slice(slice.Direction, slice.transform.position, slice.sliceForce);
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                Invoke("MoveToHowToScene", 1.5f);
            }
        }

        if (other.CompareTag("DropArea"))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                Debug.Log("Point of contact: " + hit.point);
                Slice(new Vector3(0, -1, 0), hit.point, 4f);
                Destroy(this.gameObject, 3f);
            }
        }
        if (other.CompareTag("grab"))
        {
            if (!GameManager.isSliceMode)
            {
                Debug.Log("Grab mode + grab object has grabbed the fruit");
                // update fruit position according to the grab object's position
            }
        }
            
    }

    private void MoveToHowToScene()
    {
        SceneManager.LoadScene(1);
    }
}
