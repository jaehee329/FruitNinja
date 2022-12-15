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
    private Fruit SliceModeScript;

    private void Awake()
    {
        fruitRigidBody = GetComponent<Rigidbody>();
        fruitCollider = GetComponent<Collider>();
        SliceModeScript = gameObject.GetComponent<Fruit>();
        SliceModeScript.enabled = true;
    }

    private void Update()
    {
        if (!GameManager.isSliceMode)
        {
            // Grab mode 일 때
            SliceModeScript.enabled = false;
        }
        else
        {
            // Slice mode 일 때
            SliceModeScript.enabled = true;
        }
    }

    private void Slice(Vector3 direction, Vector3 contactPoint, float force)
    {
        // Slice 하는 순간 구 모양을 숨기고 두개의 반구 모양을 enable 함 
        whole.SetActive(false);
        sliced.SetActive(true);

        fruitCollider.enabled = false;

        // 벤 각도를 계산하여 그 방향으로 돌림
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        sliced.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        // bottom 과 top 두 반구를 가지고 와 velocity 와 force 를 가해줌
        Rigidbody[] slices = sliced.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody slice in slices)
        {
            slice.velocity = fruitRigidBody.velocity;
            slice.AddForceAtPosition(direction * force, contactPoint, ForceMode.Impulse);
        }

        // 베는 효과음 재생
        GetComponent<AudioSource>().Play();

        // Game scene 일 때 베는 순간 점수를 증가시킴
        if (SceneManager.GetActiveScene().name.Equals("Game"))
        {
            GameManager.IncreaseScore();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 과일에 손 커서가 닿았을 때
        if (other.CompareTag("hand"))
        {
            if (GameManager.isSliceMode)
            {
                // slice 모드이면 과일을 베는 모션을 실행함
                Debug.Log("slice fruit");
                Slice slice = other.GetComponent<Slice>();
                Slice(slice.Direction, slice.transform.position, slice.sliceForce);

                // start scene 에서 쓰이는 수박 과일의 경우 베었을 때 다음 씬으로 넘어가야함
                if (SceneManager.GetActiveScene().buildIndex == 0)
                {
                    Invoke("MoveToHowToScene", 1.5f);
                }
            }
        }

        // 과일에 쓰레기통 영역이 닿았을 때
        if (other.CompareTag("DropArea"))
        {
            // Raycast 를 사용하여 쓰레기통과 닿은 지점을 파악하고 그 지점을 기준으로 과일에 force 를 가함
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                Debug.Log("Point of contact: " + hit.point);
                Slice(new Vector3(0, -1, 0), hit.point, 4f);

                // 쓰레기통에 걸려서 갈라졌을 때에는 3초 뒤에 과일을 삭제함
                Destroy(this.gameObject, 3f);
            }
        }
    }

    private void MoveToHowToScene()
    {
        SceneManager.LoadScene(1);
    }
}
