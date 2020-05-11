using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MySql.Data;
using MySql.Data.MySqlClient;
using System;

public class DatabaseHandler : MonoBehaviour
{

    MySqlConnection connection;

    MySqlConnection Connect()
    {
        string connString = "server=sql7.freemysqlhosting.net;database=	sql7339576;uid=	sql7339576;pwd=STmPNXBdXl;";
        return new MySqlConnection(connString);
    }

    
    void Start()
    {
        connection = Connect();
        try
        {
            Debug.Log("Connecting to sql");
            connection.Open();

            string sql = "SELECT * FROM cars";
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            MySqlDataReader res = cmd.ExecuteReader();
            if (res != null)
            {
                res.Read();
                Debug.Log(res.GetString(0));
                res.Read();
                Debug.Log(res.GetString(1));
            }
        }
        catch(Exception e)
        {
            Debug.Log(e.ToString());
        }
        connection.Close();
        Debug.Log("Deone.");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
