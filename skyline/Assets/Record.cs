using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Equipment { BASIC, NORMAL, LUXURY, UNKNOWN};

public class Record : MonoBehaviour
{
    public int id;
    public string carName;
    public string brand;
    public string color;
    public int price;
    public int driven;
    public int performance;
    public Equipment equipment;
    public int consumption;
    public int year;

    public Record(int _id, string _carName, string _brand, string _color,
        int _price, int _driven, int _performance, string _equipment,
        int _consumption, int _year)
    {
        id = _id;
        carName = _carName;
        brand = _brand;
        color = _color;
        price = _price;
        driven = _driven;
        performance = _performance;
        equipment = EFromString(_equipment);
        consumption = _consumption;
        year = _year;
    }

    public int GetValue(int i)
    {
        return i == 0 ? id : i == 4 ? price : i == 5 ? driven : i == 6 ? performance : i == 8 ? consumption : i == 9 ? year : -1;
    }
    
    private Equipment EFromString(string s)
    {
        return s == "basic" ? Equipment.BASIC : s == "normal" ? Equipment.NORMAL : s == "luxury" ? Equipment.LUXURY : Equipment.UNKNOWN;
    }


}
