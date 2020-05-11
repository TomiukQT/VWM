using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    DatabaseHandler dbHandler;

    List<Record> records = null;
    List<Point> points;

    int maxX = 0;
    int maxY = 0;

    private void Awake()
    {
        dbHandler = GameObject.Find("DatabaseHandler").GetComponent<DatabaseHandler>();
    }

    private void Start()
    {
        //while(records == null)
        records = dbHandler.GetRecords();

       // GetAllPoints(4, 9);
        points = new List<Point>() {new Point(1,9),
        new Point(2,10),
        new Point(4,8),
        new Point(5,6),
        new Point(6,7),
        new Point(9,10),
        new Point(9,1),
        new Point(3,2),
        new Point(6,2),
        new Point(4,3),
        new Point(8,3),
        new Point(10,4),
        new Point(7,5)};

        GetSkyline();

    }

    private int Domination(Point p1, Point p2)
    {   return p1.Domination(p2);}

    //4 5 6 8 9
    private void GetAllPoints(int i1, int i2)
    {
        points = new List<Point>();
        foreach (Record r in records)
            points.Add(new Point(r.GetValue(i1), r.GetValue(i2)));
    }

    private void GetMaxXY()
    {
        int mx = 0;
        int my = 0;
        foreach(Point p in points)
        {
            if (p.x > mx)
                mx = p.x;
            if (p.y > my)
                my = p.y;
        }
        maxX = mx;
        maxY = my;
    }

    public List<Point> GetSkyline() // index method
    {
        List<Point> skyline = new List<Point>();
        SortedList<int, List<Point>> list1 = new SortedList<int, List<Point>>();
        SortedList<int, List<Point>> list2 = new SortedList<int, List<Point>>();
        
        foreach(Point p in points)
        {
            int min = p.MinC();
            SortedList<int, List<Point>> toInsert;
            if (min == p.x)
                toInsert = list1;
            else
                toInsert = list2;
            if (!toInsert.ContainsKey(min))
            {
                toInsert.Add(min, new List<Point>());
            }
            toInsert[min].Add(p);

        }

        foreach(KeyValuePair<int,List<Point>> kvp in list1)
        {
            for (int i = 0; i < kvp.Value.Count; i++)
            {
                Debug.Log(kvp.Key + " : "  + kvp.Value[i].y);
            }
        }


        return skyline;
    }
}
