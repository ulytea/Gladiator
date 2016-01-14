using UnityEngine;
using System.Collections;

public class Point
{
    public int X;
    public int Y;
    public int Z;
    public Point(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }
    public override string ToString()
    {
        return "[" + X + " " + Y + " " + Z + " ] ";
    }
    public static Point operator +(Point p1, Point p2)
    {
        return new Point(p1.X + p2.X, p1.Y + p2.Y, p1.Z + p2.Z);
    }
    public static bool operator ==(Point p1, Point p2)
    {
        return (p1.X == p2.X && p1.Y == p2.Y && p2.Z == p2.Z);
    }
    public static bool operator !=(Point p1, Point p2)
    {
        return (p1.X != p2.X || p1.Y != p2.Y || p2.Z != p2.Z);
    }
}
public class Hex : MonoBehaviour
{
    public Point MapPos;
    public bool Passable = true;
    public Color oriColor = Color.white;



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void HexHl()
    {
        PlayerManager pm = PlayerManager.instance;
        PlayerBase pb = pm.Players[pm.CurTurnIdx];
        if (pb.act == ACT.IDLE)
        {
            if (Passable == true)
            {
                transform.GetComponent<Renderer>().material.color = Color.yellow;
                oriColor = Color.yellow;
            }
            else
            {
                transform.GetComponent<Renderer>().material.color = Color.white;
                oriColor = Color.white;
            }
            Passable = !Passable;
        }
        else if (pb.act == ACT.MOVEHILIGHT)
        {
            pm.MovePlayer(pm.Players[pm.CurTurnIdx].CurHex, this);
        }

    }
    public void SetMapPos(Point pos)
    {
        MapPos = pos;
    }
    public void SetMapPos(int x, int y, int z)
    {
        MapPos = new Point(x, y, z);
    }

}
