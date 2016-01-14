using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AI
    :Singleton<AI>
{
	public void MoveAIToNearUserPlayer(PlayerBase aiPlayer)
	{
		PlayerBase nearUserPlayer = null;

		PlayerManager pm = PlayerManager.instance;
		MapManager mm = MapManager.instance;
		int nearDis = 1000;
		foreach (PlayerBase up in pm.Players)
		{
			if(up is UserPlayer)
			{
				int dis = mm.GetDis(up.CurHex,aiPlayer.CurHex);
				if(nearDis > dis)
				{
					nearUserPlayer = up;
					nearDis = dis;
				}
			}
		}

		if (nearUserPlayer != null)
		{
			List<Hex> path = mm.GetPath(aiPlayer.CurHex , nearUserPlayer.CurHex);

			if(path.Count > aiPlayer.Status.MoveRange)
			{
				path.RemoveRange(aiPlayer.Status.MoveRange , path.Count - aiPlayer.Status.MoveRange);
			}
			aiPlayer.MoveHexes = path;

			if(nearUserPlayer.CurHex.MapPos == aiPlayer.MoveHexes[aiPlayer.MoveHexes.Count-1].MapPos)
			{
				aiPlayer.MoveHexes.RemoveAt(aiPlayer.MoveHexes.Count -1);
			}
			if(aiPlayer.MoveHexes.Count == 0)
			{
				return;
			}
			aiPlayer.act = ACT.MOVING;
            MapManager.instance.ResetMapColor(aiPlayer.CurHex.MapPos);
//			aiPlayer.MoveHexes = mm.GetPath(aiPlayer.CurHex,nearUserPlayer.CurHex);
//
//
//			aiPlayer.MoveHexes.RemoveAt(aiPlayer.MoveHexes.Count-1);
//
//			if(aiPlayer.MoveHexes.Count != 0)
//				aiPlayer.act = ACT.MOVING;
//			else 
//				aiPlayer.act = ACT.IDLE;
		}
	}
	public void AtkAItoUser(PlayerBase aiPlayer)
	{
		PlayerManager pm = PlayerManager.instance;
		MapManager mm = MapManager.instance;
		PlayerBase nearUserPlayer = null;
		int nearDis = 1000;
		foreach (PlayerBase up in pm.Players)
		{
			if(up is UserPlayer)
			{
				int dis = mm.GetDis(up.CurHex,aiPlayer.CurHex);
				if(nearDis > dis)
				{
					nearUserPlayer = up;
					nearDis = dis;
				}
			}
		}
    if (nearUserPlayer != null) {
        
            BattleManager.instance.AttackAtoB(aiPlayer,nearUserPlayer);
            return;
			//nearUserPlayer.GetDamage(10);
			// Debug.Log ("aipalyer attack");
            // aiPlayer.transform.rotation = Quaternion.LookRotation((nearUserPlayer.CurHex.transform.position
            // - aiPlayer.transform.position).normalized);
          //  aiPlayer.anim.SetBool("Attack",true);
		}
		//pm.TurnOver ();
	}
}
