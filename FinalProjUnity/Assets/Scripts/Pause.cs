using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Pause : MonoBehaviour, IPointerClickHandler
{
    public GameObject modalObject;

    public void OnPointerClick(PointerEventData eventData)
    {
        // game playing 중 일 때만 동작함 
        if (GameManager.isPlaying)
        {
            // pause 버튼 클릭 시 시간을 멈추고 화면 중간에 모달을 띄움
            modalObject.SetActive(true);
            Time.timeScale = 0;
            GameManager.isPlaying = false;
        }
    }

}
