using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineMeter : MonoBehaviour
{
    private Image meter;
    // Start is called before the first frame update
    void Start()
    {
        meter = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        meter.fillAmount = 1 - (SliceLine.instance.lineLength / SliceLine.instance.maxLineLength);
    }
}
