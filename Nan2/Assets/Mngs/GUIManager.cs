using UnityEngine;
using System.Collections;

public class GUIManager : Singleton<GUIManager>
{
    private PlayerManager pm = null;

    // Use this for initialization
    void Awake()
    {
        pm = PlayerManager.instance;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DrawGUI()
    {
        pm = PlayerManager.instance;

        if (pm.Players != null)
        {
            if (pm.Players.Count > 0)
            {
                DrawStatus(pm.Players[pm.CurTurnIdx]);
                DrawCommand(pm.Players[pm.CurTurnIdx]);
            }
        }
    }

    public void DrawCommand(PlayerBase pb)
    {
        int cmdCnt = 3;
        float cmdW = 150f;
        float btnW = 100f;
        float btnH = 50f;

        GUILayout.BeginArea(new Rect(Screen.width - cmdW, Screen.height - cmdCnt * btnH,
         cmdW, cmdCnt * btnH), "Command", GUI.skin.window);
        if (GUILayout.Button("Move"))
        {
            if (MapManager.instance.HillightMoveRange(pb.CurHex, pb.Status.MoveRange) == true)
            {
                pb.act = ACT.MOVEHILIGHT;
            }
        }
        if (GUILayout.Button("Attack"))
        {
            if (MapManager.instance.HillightAtkRange(pb.CurHex, pb.Status.AtkRange) == true)
            {
                pb.act = ACT.ATTACKHIGHLIGHT;
            }
        }
        if (GUILayout.Button("Turn Over"))
        {
            PlayerManager.instance.TurnOver();
        }
        GUILayout.EndArea();
    }
    public void DrawStatus(PlayerBase pb)
    {
        float btnW = 100f;
        float btnH = 50f;

        GUILayout.BeginArea(new Rect(0, Screen.height / 2, 150f, Screen.height / 2) , "Player Info",GUI.skin.window);
        GUILayout.Label( "Name" + pb.Status.Name);
        GUILayout.Label( "HP" + pb.Status.CurHp);
        GUILayout.Label( "MoveRange" + pb.Status.MoveRange);
        GUILayout.Label( "AtkRange" + pb.Status.AtkRange);
        GUILayout.EndArea();
    }
    //     public void DrawCommand(PlayerBase pb)
    //     {
    //         float btnH = 50f;
    //         float btnW = 100f;
    // 
    //         Rect rect = new Rect(0, Screen.height / 2, btnW, btnH);
    //         if (GUI.Button(rect, "Move"))
    //         {
    //             Debug.Log("Move");
    //             if (MapManager.instance.HillightMoveRange(pb.CurHex, pb.Status.MoveRange) == true)
    //             {
    //                 pb.act = ACT.MOVEHILIGHT;
    //             }
    //         }
    //         rect = new Rect(0, Screen.height / 2 + btnH, btnW, btnH);
    //         if (GUI.Button(rect, "Attack"))
    //         {
    //             Debug.Log("Attack");
    //             if (MapManager.instance.HillightAtkRange(pb.CurHex, pb.Status.AtkRange) == true)
    //             {
    //                 pb.act = ACT.ATTACKHIGHLIGHT;
    //             }
    //         }
    //         rect = new Rect(0, Screen.height / 2 + btnH * 2, btnW, btnH);
    //         if (GUI.Button(rect, "Turn Over"))
    //         {
    //             Debug.Log("Turn Over");
    //             PlayerManager.instance.TurnOver();
    //         }
    // 
    //     }
}
