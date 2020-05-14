using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class WindowManager : MonoBehaviour
{
    [SerializeField] private RectTransform databaseWindow;
    [SerializeField] private RectTransform graphWindow;

    private Vector2 databaseWindowPosition;
    private Vector2 graphWindowPosition;
    private Vector2 offScreen;

    [SerializeField] private TextMeshProUGUI btnText;

    private void Awake()
    {
        offScreen = new Vector2(-10000, -10000);
        databaseWindowPosition = databaseWindow.anchoredPosition;
        graphWindowPosition = graphWindow.anchoredPosition;
    }

    private void Start()
    {
        Toggle(1);
    }

    public void Toggle(int mn = 999)
    {
        if(databaseWindow.anchoredPosition == databaseWindowPosition || mn != 999)
        {
            databaseWindow.anchoredPosition = offScreen;
            graphWindow.anchoredPosition = graphWindowPosition;
            btnText.text = "Database";
        }
        else
        {
            graphWindow.anchoredPosition = offScreen;
            databaseWindow.anchoredPosition = databaseWindowPosition;
            btnText.text = "Graph";
        }
    }



}
