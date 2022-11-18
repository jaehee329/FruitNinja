using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameCommands : MonoBehaviour
{
    public Button resumeBtn;
    public Button quitBtn;
    public GameObject modalObject;

    void Start()
    {
        // 플레이 도중엔 UI 모달이 보이지 않아야함
        modalObject.SetActive(false);

        // 버튼에 EventListener 부착 - onclick 이벤트
        resumeBtn.onClick.AddListener(onResume);
        quitBtn.onClick.AddListener(OnQuit);
    }

    private void onResume()
    {
        // resume 버튼 눌렀을 때 멈춰뒀던 시간을 다시 흐르게 함
        Time.timeScale = 1;
        GameManager.isPlaying = true;

        // 모달은 다시 숨겨줌
        modalObject.SetActive(false);
    }

    private void OnQuit()
    {
        // quit 버튼 누르면 메인 화면으로 이동
        SceneManager.LoadScene(0);
    }
}
