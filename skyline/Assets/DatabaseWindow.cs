using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class DatabaseWindow : MonoBehaviour
{
    DatabaseHandler dbHandler;

    [SerializeField] private GameObject recordPrefab;
    [SerializeField] private Transform contentParent;

    List<Record> originalRecords;

    private void Awake()
    {
        dbHandler = GameObject.Find("DatabaseHandler").GetComponent<DatabaseHandler>();
    }

    public void DataReady()
    {
        originalRecords = dbHandler.GetRecords();
        Show();
    }


    private void Show()
    {
        Clear();
        List<Record> records = new List<Record>(originalRecords);
        records.Sort((p1, p2) => p1.id.CompareTo(p2.id));
        foreach(Record r in records)
        {
            GameObject rec = Instantiate(recordPrefab,contentParent);
            SetRecordText(rec, r);
        }
    }

    private void SetRecordText(GameObject rec, Record r)
    {
        rec.transform.Find("id").GetComponent<TextMeshProUGUI>().text = r.id.ToString();
        rec.transform.Find("carName").GetComponent<TextMeshProUGUI>().text = r.carName.ToString();
        rec.transform.Find("brand").GetComponent<TextMeshProUGUI>().text = r.brand.ToString();
        rec.transform.Find("color").GetComponent<TextMeshProUGUI>().text = r.color.ToString();
        rec.transform.Find("price").GetComponent<TextMeshProUGUI>().text = r.price.ToString();
        rec.transform.Find("driven").GetComponent<TextMeshProUGUI>().text = r.driven.ToString();
        rec.transform.Find("performance").GetComponent<TextMeshProUGUI>().text = r.performance.ToString();
        rec.transform.Find("consumption").GetComponent<TextMeshProUGUI>().text = r.consumption.ToString();
        rec.transform.Find("year").GetComponent<TextMeshProUGUI>().text = r.year.ToString();
    }

    private void Clear()
    {
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }
    }

}
