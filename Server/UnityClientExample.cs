using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Cube : MonoBehaviour
{
    string URL = "http://127.0.0.1:5000/hand";
    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(FetchHandData());
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    IEnumerator FetchHandData()
    {
        UnityWebRequest r = UnityWebRequest.Get(URL);
        r.SetRequestHeader("Content-Type", "application/json");
        r.SetRequestHeader("User-Agent", "DefaultBrowser");
        yield return r.SendWebRequest();

        if (r.result == UnityWebRequest.Result.ConnectionError || r.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(r.error);
        }
        else {
            Debug.Log(r.downloadHandler.text);
            var data = JsonUtility.FromJson<HandData>(r.downloadHandler.text);
            Debug.Log(data);
        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(FetchHandData());
    }

    public class HandData
    {
        public float[] coor;
        public float type;
    }



}
