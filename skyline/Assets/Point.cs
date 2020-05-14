using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point
{
    public int x;
    public int y;

    public Record record;

    public Point(int _x, int _y, Record _record)
    {
        x = _x;
        y = _y;
        record = _record;
    }

    public int MinC()
    {
        return x <= y ? x : y;
    }

    // 1 this is dominating p; 0 is nothing; -1 this is dominated by p;
    public int Domination(Point p)
    {
        if (x <= p.x && y <= p.y)
            return 1;
        else if (p.x < x && p.y < y)
            return -1;
        else
            return 0;
    }

    public override string ToString()
    {
        return "(" + x + ";" + y + ")";
    }

    public Vector2 toVector2()
    {
        return new Vector2(x, y);
    }
}
