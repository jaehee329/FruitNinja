using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool isPlaying = false;
    public static bool isSliceMode = true;
    public TMP_Text mode;


    // Start is called before the first frame update
    void Start()
    {
        isPlaying = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("Space bar pushed");
            isSliceMode = !isSliceMode;
            if (isSliceMode)
                mode.text = "Slice";
            else
                mode.text = "Grab";

        }
    }

    public static void MoveScene(int sceneIdx)
    {
        SceneManager.LoadScene(sceneIdx);
    }
}
