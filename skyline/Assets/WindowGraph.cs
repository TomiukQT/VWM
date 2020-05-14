using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WindowGraph : MonoBehaviour
{
    [SerializeField] private Sprite circleSprite;
    
    private RectTransform graphContainer;

    float yMaximum = 10f;
    float xMaximum = 10f;
    float xMinimum = 10f;
    float yMinimum = 10f;

    private void Awake()
    {
        graphContainer = transform.Find("graph_container").GetComponent<RectTransform>();

    }

    public void InitMax(float x, float y, float x1, float y1)
    {
        xMaximum = x;
        yMaximum = y;
        xMinimum = x1;
        yMinimum = y1;
    }

    private GameObject CreateCircle(Vector2 anchoredPosition, Color? color = null)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        gameObject.GetComponent<Image>().color = color ?? Color.white;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    public void ShowGraph(List<Point> valueList)
    {
        float graphHeight = graphContainer.sizeDelta.y - 20f;
        float graphWidth = graphContainer.sizeDelta.x - 20f;
        
        //float xSize = 50f;

        GameObject lastCircle = null;

        for (int i = 0; i < valueList.Count; i++)
        {
            float xPosition = (valueList[i].x / (xMaximum - xMinimum)) * graphWidth;
            float yPosition = (valueList[i].y / (yMaximum-yMinimum)) * graphHeight;
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition),Color.green);
            RecordComponent rc = circleGameObject.AddComponent<RecordComponent>();
            rc.r = valueList[i].record;

            if (lastCircle == null)
            {
                CreateDotConnection(new Vector2(circleGameObject.GetComponent<RectTransform>().anchoredPosition.x, 10000), circleGameObject.GetComponent<RectTransform>().anchoredPosition);
            }
            if (lastCircle != null)
            {
                CreateDotConnection(lastCircle.GetComponent<RectTransform>().anchoredPosition,
                    new Vector2( circleGameObject.GetComponent<RectTransform>().anchoredPosition.x, lastCircle.GetComponent<RectTransform>().anchoredPosition.y));
                CreateDotConnection(new Vector2(circleGameObject.GetComponent<RectTransform>().anchoredPosition.x, lastCircle.GetComponent<RectTransform>().anchoredPosition.y),
                    circleGameObject.GetComponent<RectTransform>().anchoredPosition);

            }
            if(i == valueList.Count-1)
            {
                CreateDotConnection(circleGameObject.GetComponent<RectTransform>().anchoredPosition, new Vector2(10000,circleGameObject.GetComponent<RectTransform>().anchoredPosition.y ));
            }
            lastCircle = circleGameObject;
        }
    }

    public void ShowCircles(List<Point> points)
    {
        float graphHeight = graphContainer.sizeDelta.y;
        float graphWidth = graphContainer.sizeDelta.x;
        for (int i = 0; i < points.Count; i++)
        {
            float xPosition = (points[i].x / (xMaximum - xMinimum)) * graphWidth;
            float yPosition = (points[i].y / (yMaximum - yMinimum)) * graphHeight;
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));
            RecordComponent rc = circleGameObject.AddComponent<RecordComponent>();
            rc.r = points[i].record;
        }
    }

    private float GetAngle(Vector2 dir)
    {
        return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }

    private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB)
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, .5f);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 3f);
        rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, GetAngle(dir));

    }
}
