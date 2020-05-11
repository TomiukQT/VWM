using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public int x;
    public int y;

    public Point(int _x, int _y)
    {
        x = _x;
        y = _y;
    }

    public int MinC()
    {
        return x <= y ? x : y;
    }

    // 1 this is dominating p; 0 is nothing; -1 this is dominated by p;
    public int Domination(Point p)
    {
        if (x < p.x && y < p.y)
            return 1;
        else if (p.x < x && p.y < y)
            return -1;
        else
            return 0;
    }
}
