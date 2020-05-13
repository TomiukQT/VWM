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

    private void Awake()
    {
        graphContainer = transform.Find("graph_container").GetComponent<RectTransform>();

    }

    public void InitMax(float x, float y)
    {
        xMaximum = x;
        yMaximum = y;
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
        float graphHeight = graphContainer.sizeDelta.y;
        float graphWidth = graphContainer.sizeDelta.x;
        
        //float xSize = 50f;

        GameObject lastCircle = null;

        for (int i = 0; i < valueList.Count; i++)
        {
            float xPosition = (valueList[i].x / xMaximum) * graphWidth;
            float yPosition = (valueList[i].y / yMaximum) * graphHeight;
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition),Color.green);
            if (lastCircle != null)
            {
                CreateDotConnection(lastCircle.GetComponent<RectTransform>().anchoredPosition,
                    new Vector2( circleGameObject.GetComponent<RectTransform>().anchoredPosition.x, lastCircle.GetComponent<RectTransform>().anchoredPosition.y));
                CreateDotConnection(new Vector2(circleGameObject.GetComponent<RectTransform>().anchoredPosition.x, lastCircle.GetComponent<RectTransform>().anchoredPosition.y),
                    circleGameObject.GetComponent<RectTransform>().anchoredPosition);

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
            float xPosition = (points[i].x / xMaximum) * graphWidth;
            float yPosition = (points[i].y / yMaximum) * graphHeight;
            CreateCircle(new Vector2(xPosition, yPosition));
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
