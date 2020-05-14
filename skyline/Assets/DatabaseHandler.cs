using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MySql.Data;
using MySql.Data.MySqlClient;
using System;

public class DatabaseHandler : MonoBehaviour
{

    DataManager manager;
    DatabaseWindow database;

    MySqlConnection connection;
    
    List<Record> records;
    MySqlConnection Connect()
    {
        string connString = "server=sql7.freemysqlhosting.net;database=	sql7339576;uid=	sql7339576;pwd=STmPNXBdXl;";
        return new MySqlConnection(connString);
    }

    
    private void Awake()
    {

        records = new List<Record>();
        manager = GameObject.Find("DataManager").GetComponent<DataManager>();
        database = GameObject.Find("DatabaseWindow").GetComponent<DatabaseWindow>();

        connection = Connect();
        
    }

    private void Start()
    {
        try
        {
            Debug.Log("Connecting to sql");
            connection.Open();
            MySqlDataReader res = GetData();

            if (res != null)
            {
                while (res.Read())
                {
                    //Debug.Log(res.GetString(0));
                    //Debug.Log(res.GetString(1));
                    Record r = new Record(res.GetInt32(0), res.GetString(1), res.GetString(2), res.GetString(3),
                        res.GetInt32(4), res.GetInt32(5), res.GetInt32(6), res.GetString(7), res.GetInt32(8), res.GetInt32(9));
                    records.Add(r);
                }

            }
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
        connection.Close();
        Debug.Log("Deone.");
        manager.DataReady();
        database.DataReady();
    }

    private MySqlDataReader GetData()
    {
        if (connection == null)
            return null;
        string sql = "SELECT * FROM cars";
        MySqlCommand cmd = new MySqlCommand(sql, connection);
        return cmd.ExecuteReader();
    }

    public List<Record> GetRecords()
    {
        return records;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
