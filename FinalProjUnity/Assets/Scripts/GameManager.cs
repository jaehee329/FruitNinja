using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool isPlaying = false;
    public static bool isSliceMode = true;
    public static bool isGameOver = false;
    public static int chance = 3;
    public static int collectedBomb = 0;
    private static int score = 0;
    private AudioSource failAudio;

    public static TMP_Text scoreObj;
    public TMP_Text mode;
    public GameObject explosion;
    public GameObject GameOverModal;
    public RawImage[] failIcons;

    private FetchHandData handDataScript;
    private int curHandType;

    


    private void Awake()
    {
        Debug.Log("Awake");
        score = 0;
        scoreObj = GameObject.Find("Score").GetComponent<TMP_Text>();
        chance = 3;
        collectedBomb = 0;
        explosion.SetActive(false);
        GameOverModal.SetActive(false);
        isPlaying = true;
        isGameOver = false;
        isSliceMode = true;
        handDataScript = this.gameObject.GetComponent<FetchHandData>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        failAudio = failIcons[0].transform.parent.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckChance();

        if (isGameOver)
        {
            GameOver();
            return;
        }

        if (chance == 0)
        {
            StopGameWithGameOverModal();
        }

        // Hand Mode
        curHandType = handDataScript.handType;
        //Debug.Log("handType = " + curHandType);
        if (curHandType == 0) {
            Debug.Log("Slice");
            isSliceMode = true;
            mode.text = "Slice!";
        } else {
            Debug.Log("Grab");
            isSliceMode = false;
            mode.text = "Grab";
        }
       
    }

    public static void MoveScene(int sceneIdx)
    {
        SceneManager.LoadScene(sceneIdx);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("fruit"))
        {
            chance--;
            failAudio.Play();
            Destroy(other.gameObject);
        }
        if (other.CompareTag("bomb"))
        {
            Destroy(other.gameObject);
            Debug.Log("bomb has touched the floor");
            isGameOver = true;
            
        }
    }

    public void GameOver()
    {
        // 폭발 효과
        explosion.SetActive(true);
        StartCoroutine(DelayTimeAndEndGame(1));
        Debug.Log("Game Over");
    }

    IEnumerator DelayTimeAndEndGame(int sec)
    {
        yield return new WaitForSeconds(sec);
        StopGameWithGameOverModal();
    }

    public void StopGameWithGameOverModal()
    {
        MuteBomb();
        isPlaying = false;
        GameOverModal.SetActive(true);
        Time.timeScale = 0;
    }

    private void CheckChance()
    {
        switch (chance) {
            case 2:
                failIcons[0].material = null;
                break;
            case 1:
                failIcons[1].material = null;
                break;
            case 0:
                failIcons[2].material = null;
                break;
            default:
                break;
                
        }
    }

    private void MuteBomb()
    {
        GameObject[] bombs = GameObject.FindGameObjectsWithTag("bomb");
        foreach (GameObject bomb in bombs)
        {
            bomb.GetComponent<AudioSource>().Stop();
        }
    }

    public static void IncreaseScore()
    {
        score += 10;
        scoreObj.text = score.ToString();
    }
}
