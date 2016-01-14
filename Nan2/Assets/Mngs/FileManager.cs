using UnityEngine;
using System.Collections;
using System.Xml;
public class FileManager
{
    private static FileManager inst = null;
    public static FileManager GetInst()
    {
        if(inst == null)
        {
            inst = new FileManager();
        }
        return inst;
    }
    
    public MapInfo LoadMap()
    {
        Debug.Log("LoadMap Start");
        MapInfo mapinfo = new MapInfo();
        
        XmlDocument xmlFile = new XmlDocument();
        xmlFile.Load("test.xml");
        XmlNode mapSize = xmlFile.SelectSingleNode("MapInfo/MapSize");
        string mapSizeString = mapSize.InnerText;
        string[] sizes = mapSizeString.Split( ' ' );
        
        int mapSizeX = mapinfo.MapSizeX = int.Parse(sizes[0]);
        int mapSizeY = mapinfo.MapSizeY = int.Parse(sizes[1]);
        int mapSizeZ = mapinfo.MapSizeZ = int.Parse(sizes[2]);
        
        XmlNodeList hexes = xmlFile.SelectNodes("MapInfo/Hex");
        
        foreach(XmlNode hex in hexes)
        {
            string mapposStr = hex["MapPos"].InnerText;
            string[] mapposes = mapposStr.Split( ' ' );
            int MapPosX = int.Parse(mapposes[0]);
            int MapPosY = int.Parse(mapposes[1]);
            int MapPosZ = int.Parse(mapposes[2]);
            
            string passableStr = hex["Passable"].InnerText;
            bool passable = passableStr == "True";
            
            HexInfo info = new HexInfo();
            info.MapPosX = MapPosX;
            info.MapPosY = MapPosY;
            info.MapPosZ = MapPosZ;
            info.Passable = passable;
            mapinfo.HexInfos.Add(info);            
        }
       return mapinfo;
    }
}
