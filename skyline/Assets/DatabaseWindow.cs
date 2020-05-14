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


    public void Show(int i = 1)
    {

        List<Record> records = new List<Record>(originalRecords);
        records.Sort((p1, p2) => i * p1.id.CompareTo(p2.id));
        UpdateTable(records);
        
    }

    public void ShowByPrice(int i = 1)
    {
        List<Record> records = new List<Record>(originalRecords);
        records.Sort((p1, p2) => i * p1.price.CompareTo(p2.price));
        UpdateTable(records);
    }

    public void ShowByDriven(int i = 1)
    {
        List<Record> records = new List<Record>(originalRecords);
        records.Sort((p1, p2) => i * p1.driven.CompareTo(p2.driven));
        UpdateTable(records);
    }

    public void ShowByPerformance(int i = 1)
    {
        List<Record> records = new List<Record>(originalRecords);
        records.Sort((p1, p2) => i * p1.performance.CompareTo(p2.performance));
        UpdateTable(records);
    }

    public void ShowByCons(int i = 1)
    {
        List<Record> records = new List<Record>(originalRecords);
        records.Sort((p1, p2) => i * p1.consumption.CompareTo(p2.consumption));
        UpdateTable(records);
    }

    public void ShowByYear(int i = 1)
    {
        List<Record> records = new List<Record>(originalRecords);
        records.Sort((p1, p2) => i * p1.year.CompareTo(p2.year));
        UpdateTable(records);
    }

    public void UpdateTable(List<Record> records)
    {
        Clear();
        foreach (Record r in records)
        {
            GameObject rec = Instantiate(recordPrefab, contentParent);
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
