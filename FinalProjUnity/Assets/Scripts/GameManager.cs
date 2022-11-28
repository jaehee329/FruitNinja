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

    private FetchHandData handDataScript;
    private int curHandType;

    // Start is called before the first frame update
    void Start()
    {
        isPlaying = true;
        handDataScript = this.gameObject.GetComponent<FetchHandData>();
    }

    // Update is called once per frame
    void Update()
    {
        // Hand Mode
        curHandType = handDataScript.handType;
        Debug.Log("handType = " + curHandType);
        if (curHandType == 0) {
            isSliceMode = true;
            mode.text = "Slice";
        } else {
            isSliceMode = false;
            mode.text = "Grab";
        }
        // Mouse Mode

        // if (Input.GetKeyDown("space"))
        // {
        //     Debug.Log("Space bar pushed");
        //     isSliceMode = !isSliceMode;
        //     if (isSliceMode)
        //         mode.text = "Slice";
        //     else
        //         mode.text = "Grab";
        // }
    }

    public static void MoveScene(int sceneIdx)
    {
        SceneManager.LoadScene(sceneIdx);
    }
}
