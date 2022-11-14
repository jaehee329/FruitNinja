using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceLine : MonoBehaviour
{
    public static SliceLine instance;

    [SerializeField] GameObject linePrefab;
    private GameObject currentLine;

    private LineRenderer lineRenderer;
    private EdgeCollider2D edgeCollider;
    [SerializeField] private List<Vector2> mousePos;

    private float _lineLength;
    public float lineLength
    {
        get
        {
            return _lineLength;
        }
    }

    [SerializeField] private float _maxLineLength;
    public float maxLineLength
    {
        get
        {
            return _maxLineLength;
        }
    }

    private int currentPoint;

    void Start()
    {
        instance = this;   
    }

    void Update()
    {
        if (_lineLength < _maxLineLength)
        {
            if (Input.GetMouseButtonDown(0))
            {
                CreateLine();
            }
            if (Input.GetMouseButton(0))
            {
                Vector2 tempMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (Vector2.Distance(tempMousePos, mousePos[mousePos.Count - 1]) > .25f)
                {
                    UpdateLine(tempMousePos);
                }
            }
        }
    }

    void CreateLine()
    {
        currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        edgeCollider = currentLine.GetComponent<EdgeCollider2D>();
        mousePos.Clear();
        mousePos.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        mousePos.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        lineRenderer.SetPosition(0, mousePos[0]);
        lineRenderer.SetPosition(1, mousePos[1]);
        edgeCollider.points = mousePos.ToArray();
        currentPoint = 2;

    }

    void UpdateLine(Vector2 newMousePos)
    {
        mousePos.Add(newMousePos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newMousePos);
        edgeCollider.points = mousePos.ToArray();

        _lineLength += Vector2.Distance(mousePos[currentPoint], mousePos[currentPoint - 1]);
    }
}
