using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using MiniJSON;

public class FetchHandData : MonoBehaviour
{
    public float frequency = 10f;
    public double handX;
    public double handY;
    public double handZ;
    public int handType;
    private string URI = "http://localhost:8000/hand";
    // private string URI = "http://localhost:8000/";
    private bool isDelay;

    void Start()
    {
    }

    void Update()
    {
        if(!isDelay)
        {
            isDelay = true;
            StartCoroutine(GetRequest(this.URI));
        }
    }

    private IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(uri))
        {
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log("Get Request Failure");
                Debug.Log(request.error);
            }
            else
            {
                Debug.Log("Get Request Success");
                Dictionary<string, object> response = Json.Deserialize(request.downloadHandler.text) as Dictionary<string, object>;

                handX  = Convert.ToDouble(((List<object>) response["hand_coor"])[0]);
                handY  = Convert.ToDouble(((List<object>) response["hand_coor"])[1]);
                handZ  = Convert.ToDouble(((List<object>) response["hand_coor"])[2]);
            }
        }
        yield return new WaitForSeconds(1/frequency);
        isDelay = false;
    }
}