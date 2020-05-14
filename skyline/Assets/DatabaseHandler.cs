using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

using System;

public class DatabaseHandler : MonoBehaviour
{

    DataManager manager;
    DatabaseWindow database;

    
    List<Record> records;
   
    
    private void Awake()
    {

        records = new List<Record>();
        manager = GameObject.Find("DataManager").GetComponent<DataManager>();
        database = GameObject.Find("DatabaseWindow").GetComponent<DatabaseWindow>();

        
    }

    private void Start()
    {
        TextAsset file = Resources.Load("data") as TextAsset;
        Debug.Log(file.text);
        //Read the text from directly from the test.txt file
        StringReader reader = new StringReader(file.text);
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            string [] tokens  = line.Split(',');
            records.Add(
                new Record(Convert.ToInt32(tokens[0]), tokens[1], tokens[2], tokens[3],
                Convert.ToInt32(tokens[4]),
                Convert.ToInt32(tokens[5]),
                Convert.ToInt32(tokens[6]),
                tokens[7],
                Convert.ToInt32(tokens[8]),
                Convert.ToInt32(tokens[9])
                ));
        }
        reader.Close();
    


    Debug.Log("Deone.");
        manager.DataReady();
        database.DataReady();
    }

 

    public List<Record> GetRecords()
    {
        return records;
    }

}
