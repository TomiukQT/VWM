using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

using TMPro;
public class RecordComponent : MonoBehaviour, IPointerClickHandler
{
    public Record r;

    [SerializeField] private GameObject detailWindowPrefab;
    private GameObject detailWindow = null;

    private void Awake()
    {
        detailWindowPrefab = Resources.Load<GameObject>("DetailWindow");
    }

    private void ToggleDetail()
    {
        if (detailWindow == null)
        {
            detailWindow = Instantiate(detailWindowPrefab, gameObject.transform);
            detailWindow.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            SetText();
        }
        else
        {
            Destroy(detailWindow);
            detailWindow = null;
        }
    }

    private void SetText()
    {
        detailWindow.transform.Find("text").GetComponent<TextMeshProUGUI>().text = string.Format(
        "Name: {0}\nBrand: {1}\nColor: {2}\nPrice: {3}\nDriven: {4}\n" +
        "Performance: {5}\nConsumption: {6}\nYear: {7}\n", r.carName, r.brand, r.color, r.price, r.driven,
        r.performance, r.consumption, r.year
            );
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // OnClick code goes here ...
        ToggleDetail();
    }
}
