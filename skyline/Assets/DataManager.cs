using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    DatabaseHandler dbHandler;

    List<Record> records = null;
    List<Point> points;

    WindowGraph graph;

 
    int maxX = 0;
    int maxY = 0;

    int att1 = -1;
    int att2 = -1;





    private void Awake()
    {
        dbHandler = GameObject.Find("DatabaseHandler").GetComponent<DatabaseHandler>();
        graph = GameObject.Find("Window_Graph").GetComponent<WindowGraph>();
    }

    public void DataReady()
    {
        records = new List<Record>(dbHandler.GetRecords());
        att1 = 4;
        att2 = 5;
        GetAllPoints(att1, att2);
        //points = new List<Point>() {new Point(1,9),
        //new Point(2,10),
        //new Point(4,8),
        //new Point(5,6),
        //new Point(6,7),
        //new Point(9,10),
        //new Point(9,1),
        //new Point(3,2),
        //new Point(6,2),
        //new Point(4,3),
        //new Point(8,3),
        //new Point(10,4),
        //new Point(7,5)};

        List<Point> skyline;
        skyline = GetSkyline();
        skyline.Sort((p1, p2) => p1.x.CompareTo(p2.x));
        GraphInit();
        graph.ShowGraph(skyline);
        graph.ShowCircles(points.Except(skyline).ToList());
    }


    private void GraphInit()
    {
        float xMax = 0;
        float yMax = 0;
        float xMin = 1000000;
        float yMin = 1000000;
        for (int i = 0; i < points.Count; i++)
        {
            xMax = Mathf.Max(xMax, points[i].x);
            yMax = Mathf.Max(yMax, points[i].y);
            xMin = Mathf.Min(xMin, points[i].x);
            yMin = Mathf.Min(yMin, points[i].y);
        }
        graph.InitMax(xMax, yMax, xMin, yMin);
    }

    private int Domination(Point p1, Point p2)
    {   return p1.Domination(p2);}

    //4 5 6 8 9
    private void GetAllPoints(int i1, int i2)
    {
        points = new List<Point>();
        foreach (Record r in records)
            points.Add(new Point(r.GetValue(i1), r.GetValue(i2),r));
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

    public List<Point> LocalSkyline(List<Point> toTest)
    {
        List<Point> sky = new List<Point>();
        if (toTest.Count == 1)
            return toTest;
        for (int i = 0; i < toTest.Count; i++)
        {
            bool domination = true;
            for (int j = 0; j < toTest.Count; j++)
            {
                if (i != j)
                {
                    if (Domination(toTest[i], toTest[j]) != 1)
                        domination = false;
                }
            }
            if (domination)
            {
                sky.Add(toTest[i]);
            }
        }

        return sky;
    }

    public List<Point> CheckSkyline(List<Point> skyline, List<Point> toCheck)
    {
        if (toCheck == null)
            return skyline;
        foreach (Point p in toCheck)
        {
            bool valid = true;
            foreach (Point s in skyline)
            {
                if (Domination(s, p) == 1)
                    valid = false;
            }
            if (valid)
                skyline.Add(p);
        }

        return skyline;
    }


    public List<Point> GetSkyline() // index method
    {
        List<Point> skyline = new List<Point>();
        SortedList<int, List<Point>> list1 = new SortedList<int, List<Point>>();
        SortedList<int, List<Point>> list2 = new SortedList<int, List<Point>>();

        foreach (Point p in points)
        {
            int min = p.MinC();
            SortedList<int, List<Point>> toInsert;
            if (min == p.x)
                toInsert = list1;
            else
            {
                Debug.Log("Inserting: " + p.ToString() + " to list2");
                toInsert = list2;
            }
            if (!toInsert.ContainsKey(min))
            {
                toInsert.Add(min, new List<Point>());
            }
            toInsert[min].Add(p);

        }

        foreach (KeyValuePair<int, List<Point>> kvp in list1)
        {
            for (int i = 0; i < kvp.Value.Count; i++)
            {
                Debug.Log(kvp.Key + " : " + kvp.Value[i].y);
            }
        }


        List<Point> batch1 = list1.Values[0];
        List<Point> batch2 = list2.Values[0];

        while (true)
        {
            Debug.Log("Iter " + list1.Count + ";" + list2.Count);
            List<Point> toTest;
            if (batch1[0].x < batch2[0].y)
            {
                skyline = CheckSkyline(skyline, LocalSkyline(batch1));
                list1.RemoveAt(0);
                if (list1.Count == 0 || list2.Count == 0)
                    break;
                batch1 = list1.Values[0];
            }
            else
            {
                skyline = CheckSkyline(skyline, LocalSkyline(batch2));
                list2.RemoveAt(0);
                if (list1.Count == 0 || list2.Count == 0)
                    break;
                batch2 = list2.Values[0];
            }


        }

        //skyline = CheckSkyline(skyline, LocalSkyline(toTest));
        //if (list1.Count == 0 || list2.Count == 0)
        //    break;
        //batch1 = list1.Values[0];
        //batch2 = list2.Values[0];

        Debug.Log("Skyline:");
        foreach(Point p in skyline)
        {
            Debug.Log(p.x + ";" + p.y);
        }

        return skyline;
    }


    public bool SwitchAtt(int att)
    {
        if(att1 == att)
        {
            att1 =
        }

    }
}
