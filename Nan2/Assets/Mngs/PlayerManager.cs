using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager
    :Singleton<PlayerManager>
{	
	//private GameManager gm = null;
	public GameObject GO_AIplayer;
	public GameObject GO_Player;

	public List<PlayerBase> Players = new List<PlayerBase>();
	public int CurTurnIdx = 0;
    
    private float turnOverTime = 0f;
    private float curTurnOverTime = 0f;
    void Awake()
    {
        turnOverTime = 0f;
        curTurnOverTime = 0f;
    }
    void Update()
    {
        CheckTurnOver();
    }
    
    void CheckTurnOver()
    {
        if(curTurnOverTime != 0)
        {
            curTurnOverTime += Time.deltaTime;
            if(curTurnOverTime >= turnOverTime)
            {
                curTurnOverTime = 0;
                TurnOver();
            }
        }
    }

	public void GenPlayerTest()
	{
		UserPlayer player = ((GameObject)Instantiate (GO_Player)).GetComponent<UserPlayer> ();
		Hex hex = MapManager.instance.GetPlayerHex (0, 0, 0);
		player.CurHex = hex;
		player.transform.position = player.CurHex.transform.position;
		Players.Add (player);

		UserPlayer player2 = ((GameObject)Instantiate (GO_Player)).GetComponent<UserPlayer> ();
		hex = MapManager.instance.GetPlayerHex (1, 0, -1);
		player2.CurHex = hex;
		player2.transform.position = player2.CurHex.transform.position;
		Players.Add (player2);

		AIPlayer _iplayer = ((GameObject)Instantiate (GO_AIplayer)).GetComponent<AIPlayer> ();
		_iplayer.CurHex = hex;
		_iplayer.transform.position = _iplayer.CurHex.transform.position;
		Players.Add (_iplayer);

	//	AIPlayer _iplayer2 = ((GameObject)Instantiate (GO_AIplayer)).GetComponent<AIPlayer> ();
		//hex2 = MapManager.instance.GetPlayerHex (3,0,-2);
		//_iplayer2.CurHex = hex2;
	//	_iplayer2.transform.position = _iplayer2.CurHex.transform.position;
	//	Players.Add (_iplayer2);



//
//
		UserPlayer player3 = ((GameObject)Instantiate (GO_Player)).GetComponent<UserPlayer> ();
		hex = MapManager.instance.GetPlayerHex (2,0,-2);
		player3.CurHex = hex;
		player3.transform.position = player3.CurHex.transform.position;
		Players.Add (player3);


		}
	public void MovePlayer(Hex start,Hex dest)
	{
		PlayerBase pb = Players [CurTurnIdx];

		if (MapManager.instance.isReachAble (start, dest, pb.Status.MoveRange) == false) {
			return;
		}
		if (pb.act == ACT.MOVEHILIGHT)
		{
			int dis = MapManager.instance.GetDis (start, dest);
			if (dis <= pb.Status.MoveRange && dis != 0 && dest.Passable == true ) 
			{
				pb.MoveHexes = MapManager.instance.GetPath(start,dest);
				if(pb.MoveHexes.Count == 0)
				{
					return;
				}
				pb.act = ACT.MOVING;
				MapManager.instance.ResetMapColor();
			}
		}
	}
    public void SetTurnOverTime(float time)
    {
        turnOverTime = time;
        curTurnOverTime = Time.smoothDeltaTime;
    }
	public void TurnOver()
	{
    //    curTurnOverTime = Time.smoothDeltaTime;
		MapManager.instance.ResetMapColor ();
		PlayerBase pb = Players[CurTurnIdx];
		pb.act = ACT.IDLE;
		++CurTurnIdx;
		if(CurTurnIdx >= Players.Count)
		{
			CurTurnIdx = 0;
		}
		GameManager.instance.MoveCamPosToHex (Players[CurTurnIdx].CurHex);
        Debug.Log("SDADASD");
	}

	public void RemovePlayer(PlayerBase pb)
	{
		Players.Remove (pb);
		GameObject.Destroy (pb.gameObject);
	//	PlayerManager.instance.TurnOver ();
	}
	public void MouseInputProc(int btn)
	{

		if (btn == 1) {
			PlayerBase pb = Players[CurTurnIdx];

			if(pb is AIPlayer)
			{
				return;
			}

			ACT act = Players[CurTurnIdx].act;
			if(act == ACT.IDLE)
			{
				return;
			}
			if(act == ACT.MOVEHILIGHT || act == ACT.ATTACKHIGHLIGHT)
			{
				Players[CurTurnIdx].act = ACT.IDLE;
				MapManager.instance.ResetMapColor();
			}
		}
	}
}
