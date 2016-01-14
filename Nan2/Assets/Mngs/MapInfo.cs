using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HexInfo
{
    public int MapPosX;
    public int MapPosY;
    public int MapPosZ;
    
    public bool Passable;
}
public class MapInfo {
    
    public int MapSizeX;
    public int MapSizeY;
    public int MapSizeZ;
    
    public List<HexInfo> HexInfos = new List<HexInfo>();
}
