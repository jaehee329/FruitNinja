using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartPlay : MonoBehaviour
{
    public Button playBtn;

    private void Start()
    {
        playBtn.onClick.AddListener(Play);    
    }

    private void Play()
    {
        GameObject.FindGameObjectWithTag("bgm").GetComponent<AudioSource>().Stop();
        SceneManager.LoadScene(2);
    }
}
