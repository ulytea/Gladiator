using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Path // A* 알고리즘
{
    public Path Parent;
    public Hex curHex;
    public int F; // H + G 
    public int G; // start to cur dis
    public int H; // cur to arr dis
    public Path(Path parent, Hex hex, int g, int h)
    {
        Parent = parent;
        curHex = hex;
        G = g;
        H = h;
        F = H + G;
    }
}
public class MapManager
    : Singleton<MapManager>
{
    public GameObject Go_hex;
    public float HexW;
    public float HexH;

    public int MapSizeX;
    public int MapSizeY;
    public int MapSizeZ;

    public Point[] Dirs;
    Hex[][][] Map;

    void Awake()
    {
        SetHexSize();
        initDirs();
    }

    public void initDirs()
    { // 헥사 주변의 타일들의 정보를 가져옴
        Dirs = new Point[6];
        Dirs[0] = new Point(1, -1, 0); // right
        Dirs[1] = new Point(1, 0, -1); // up right
        Dirs[2] = new Point(0, 1, -1); // up left
        Dirs[3] = new Point(-1, 1, 0); // left
        Dirs[4] = new Point(-1, 0, 1); // down left
        Dirs[5] = new Point(0, -1, 1); // down right
    }
    public void SetHexSize()
    {
        HexW = Go_hex.transform.GetComponent<Renderer>().bounds.size.x;
        HexH = Go_hex.transform.GetComponent<Renderer>().bounds.size.z;

    }

    public Vector3 GetWorldPos(int x, int y, int z)
    {
        float X, Z;
        X = x * HexW + (z * HexW * 0.5f);
        Z = (-z) * HexH * 0.75f;
        return new Vector3(X, 0, Z);
    }

    public void CreateMap(MapInfo info)
    {

        MapSizeX = info.MapSizeX;
        MapSizeY = info.MapSizeY;
        MapSizeZ = info.MapSizeZ;

        Map = new Hex[info.MapSizeX * 2 + 1][][];
        GameObject map = new GameObject("Map");
        for (int x = -MapSizeX; x <= MapSizeX; x++)
        {
            Map[x + MapSizeX] = new Hex[MapSizeY * 2 + 1][];

            for (int y = -MapSizeY; y <= MapSizeY; y++)
            {
                Map[x + MapSizeX][y + MapSizeY] = new Hex[MapSizeZ * 2 + 1];
            }
        }
        foreach (HexInfo hexinfo in info.HexInfos)
        {
            GameObject hex = (GameObject)GameObject.Instantiate(Go_hex);
            hex.transform.parent = map.transform;
            int x = hexinfo.MapPosX;
            int y = hexinfo.MapPosY;
            int z = hexinfo.MapPosZ;
            Map[x + MapSizeX][y + MapSizeY][z + MapSizeZ] = hex.GetComponent<Hex>();
            Map[x + MapSizeX][y + MapSizeY][z + MapSizeZ].transform.position = GetWorldPos(x, y, z);
            Map[x + MapSizeX][y + MapSizeY][z + MapSizeZ].SetMapPos(x, y, z);
            Map[x + MapSizeX][y + MapSizeY][z + MapSizeZ].Passable = hexinfo.Passable;
            if (hexinfo.Passable)
            {
                Debug.Log("Passable true");
                Map[x + MapSizeX][y + MapSizeY][z + MapSizeZ].GetComponent<Renderer>().material.color = Color.white;
                Map[x + MapSizeX][y + MapSizeY][z + MapSizeZ].oriColor = Color.white;
            }
            else
            {
                Debug.Log("Passable false");
                Map[x + MapSizeX][y + MapSizeY][z + MapSizeZ].GetComponent<Renderer>().material.color = Color.yellow;
                Map[x + MapSizeX][y + MapSizeY][z + MapSizeZ].oriColor = Color.yellow;
                Map[x + MapSizeX][y + MapSizeY][z + MapSizeZ].gameObject.SetActive(false);
            }
        }
        // 		Map = new Hex[MapSizeX * 2 + 1][][];
        //         GameObject map = new GameObject("Map");
        // 		for (int x = -MapSizeX; x <= MapSizeX; x++) 
        // 		{
        // 			Map[x+MapSizeX] = new Hex[MapSizeY*2+1][];
        // 
        // 			for(int y = -MapSizeY; y<= MapSizeY;y++)
        // 			{
        // 				Map[x+MapSizeX][y+MapSizeY] = new Hex[MapSizeZ * 2 +1];
        // 				for(int z= -MapSizeZ; z<= MapSizeZ;z++)
        // 				{
        // 
        // 					if(x+y+z == 0)
        // 					{
        //                         GameObject hex = (GameObject)Instantiate(Go_hex);
        //                         hex.transform.parent = map.transform;
        // 						Map[x+MapSizeX][y+MapSizeY][z+MapSizeZ] = hex.GetComponent<Hex>();
        // 						Vector3 pos = GetWorldPos(x,y,z);
        // 						Map[x+MapSizeX][y+MapSizeY][z+MapSizeZ].transform.position =pos;
        // 						Map[x+MapSizeX][y+MapSizeY][z+MapSizeZ].SetMapPos(x,y,z);
        // 						
        // 					}
        // 				}
        // 				
        // 			}
        // 		}
    }

    public Hex GetPlayerHex(int x, int y, int z)
    {
        return Map[x + MapSizeX][y + MapSizeY][z + MapSizeZ];
    }
    public int GetDis(Hex h1, Hex h2)
    {
        Point pos1 = h1.MapPos;
        Point pos2 = h2.MapPos;
        return (Mathf.Abs(pos1.X - pos2.X) + Mathf.Abs(pos1.Y - pos2.Y) + Mathf.Abs(pos1.Z - pos2.Z)) / 2;

    }
    public bool isReachAble(Hex start, Hex dest, int moveRange)
    {
        List<Hex> path = GetPath(start, dest);
        if (path.Count == 0 || path.Count > moveRange)
        {
            return false;
        }
        return true;
    }
    List<Path> OpenList;
    List<Path> ClosedList;
    public List<Hex> GetPath(Hex start, Hex dest)
    {
        OpenList = new List<Path>();
        ClosedList = new List<Path>();

        List<Hex> rtnVal = new List<Hex>();

        int H = GetDis(start, dest);
        Path p = new Path(null, start, 0, H);

        ClosedList.Add(p);

        Path result = Recursive_FindPath(p, dest);

        if (result == null)
        {
            return rtnVal;
        }
        while (result.Parent != null)
        {
            rtnVal.Insert(0, result.curHex);
            result = result.Parent;
        }
        return rtnVal;
    }
    public Path Recursive_FindPath(Path parent, Hex dest)
    {
        if (parent.curHex.MapPos == dest.MapPos)
        {
            return parent; // find path
        }
        List<Hex> neibhors = GetNeibhors(parent.curHex);

        foreach (Hex h in neibhors)
        {
            Path newP = new Path(parent, h, parent.G + 1, GetDis(h, dest));
            AddToOpenList(newP);
        }

        Path bestP;

        if (OpenList.Count == 0)
        {
            return null; // path is not 
        }
        bestP = OpenList[0];
        foreach (Path p in OpenList)
        {
            if (p.F < bestP.F)
            {
                bestP = p;
            }
        }
        OpenList.Remove(bestP);
        ClosedList.Add(bestP);
        return Recursive_FindPath(bestP, dest);
    }
    public void AddToOpenList(Path p)
    {
        foreach (Path inP2 in ClosedList)
        {
            if (p.curHex.MapPos == inP2.curHex.MapPos)
            {
                return;
            }
        }
        foreach (Path inP in OpenList)
        {
            if (p.curHex.MapPos == inP.curHex.MapPos)
            {
                if (p.F < inP.F)
                {
                    OpenList.Remove(inP);
                    OpenList.Add(p);
                    return;
                }
            }
        }
        OpenList.Add(p);
    }

    public List<Hex> GetNeibhors(Hex pos)
    {
        List<Hex> rtn = new List<Hex>();
        Point cur = pos.MapPos;

        if (pos.Passable == false)
        {
            return rtn;
        }
        foreach (Point p in Dirs)
        {
            Point tmp = p + cur;

            if (tmp.X + tmp.Y + tmp.Z == 0)
            {
                Hex hex = GetHex(tmp.X, tmp.Y, tmp.Z);

                if (hex != null)
                    rtn.Add(hex);
            }

        }
        return rtn;
    }

    public Hex GetHex(int x, int y, int z)
    {
        if ((x + MapSizeX) < 0 || (y + MapSizeY) < 0 || (z + MapSizeZ) < 0)
            return null;
        if((MapSizeX + 1 <= x ) ||  (MapSizeY + 1 <= y ) || (MapSizeZ + 1 <= z ))
            return null;
        return Map[x + MapSizeX][y + MapSizeY][z + MapSizeZ];
    }

    public bool HillightAtkRange(Hex start, int atkRange)
    {
        PlayerManager pm = PlayerManager.instance;
        int highLightedCount = 0;
        for (int x = -MapSizeX; x <= MapSizeX; x++)
        {
            for (int y = -MapSizeY; y <= MapSizeY; y++)
            {
                for (int z = -MapSizeZ; z <= MapSizeZ; z++)
                {
                    if (x + y + z == 0 && Map[x + MapSizeX][y + MapSizeY][z + MapSizeZ].Passable == true)
                    {
                        int dist = GetDis(start, Map[x + MapSizeX][y + MapSizeY][z + MapSizeZ]);

                        if (dist <= atkRange && dist != 0)
                        {
                            if ((x + MapSizeX) < 0 || (y + MapSizeY) < 0 || (z + MapSizeZ) < 0)
                                continue;
                            bool isExist = false;

                            foreach (PlayerBase pb in pm.Players)
                            {
                                if (pb is AIPlayer)
                                {
                                    if (pb.CurHex.MapPos == Map[x + MapSizeX][y + MapSizeY][z + MapSizeZ].MapPos)
                                    {
                                        GameManager.instance.atkedobj = pb;
                                        isExist = true;
                                        break;
                                    }
                                }

                            }
                            if (isExist == true)
                            {
                                if (isReachAble(start, Map[x + MapSizeX][y + MapSizeY][z + MapSizeZ], atkRange))
                                {
                                    Map[x + MapSizeX][y + MapSizeY][z + MapSizeZ].transform.GetComponent<Renderer>().material.color = Color.red;
                                    highLightedCount++;
                                }
                            }

                        }
                    }
                }

            }
        }
        if (highLightedCount == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public void CreateTestMap()
    {
        MapInfo info = FileManager.GetInst().LoadMap();
        if (info == null)
        {
            Debug.Log("info is null");
        }
        if (info.HexInfos == null)
        {
            Debug.Log("info.HexInfo is null");
        }
        CreateMap(info);
    }
    public bool HillightMoveRange(Hex start, int moverange)
    {
        int highLightedCount = 0;
        for (int x = -MapSizeX; x <= MapSizeX; x++)
        {
            for (int y = -MapSizeY; y <= MapSizeY; y++)
            {
                for (int z = -MapSizeZ; z <= MapSizeZ; z++)
                {
                    if (x + y + z == 0 && Map[x + MapSizeX][y + MapSizeY][z + MapSizeZ].Passable == true)
                    {
                        int dist = GetDis(start, Map[x + MapSizeX][y + MapSizeY][z + MapSizeZ]);

                        if (dist <= moverange && dist != 0)
                        {
                            if ((x + MapSizeX) < 0 || (y + MapSizeY) < 0 || (z + MapSizeZ) < 0)
                                continue;

                            if (isReachAble(start, Map[x + MapSizeX][y + MapSizeY][z + MapSizeZ], moverange))
                            {
                                Map[x + MapSizeX][y + MapSizeY][z + MapSizeZ].transform.GetComponent<Renderer>().material.color = Color.green;
                                highLightedCount++;
                            }
                        }
                    }
                }

            }
        }
        if (highLightedCount == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void ResetMapColor()
    {
        for (int x = -MapSizeX; x <= MapSizeX; x++)
        {
            for (int y = -MapSizeY; y <= MapSizeY; y++)
            {
                for (int z = -MapSizeZ; z <= MapSizeZ; z++)
                {
                    if (x + y + z == 0)
                    {
                        Map[x + MapSizeX][y + MapSizeY][z + MapSizeZ].transform.GetComponent<Renderer>().material.color = Map[x + MapSizeX][y + MapSizeY][z + MapSizeZ].oriColor;

                    }
                }

            }
        }
    }
    public void ResetMapColor(Point pos)
    {
        Map[pos.X + MapSizeX][pos.Y + MapSizeY][pos.Z + MapSizeZ].transform.GetComponent<Renderer>().material.color = Map[pos.X + MapSizeX][pos.Y + MapSizeY][pos.Z + MapSizeZ].oriColor;
    }
    public void SetHexColor(Hex hex, Color color)
    {
        hex.transform.GetComponent<Renderer>().material.color = color;
    }
}