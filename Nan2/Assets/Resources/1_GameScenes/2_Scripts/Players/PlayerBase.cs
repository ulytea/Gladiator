using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public enum ACT
{
	IDLE,
	MOVEHILIGHT,
	MOVING,
	ATTACKHIGHLIGHT,
    ATTACKING,
   DIYING
}

public class PlayerBase : MonoBehaviour {
    public Animator anim;
	public PlayerStatus Status;
	public Hex CurHex;
	public ACT act;
	public List<Hex> MoveHexes;
    
    public float removeTime = 0f;
    public float damagedTime = 0f;
	void Awake()
	{
		// act = ACT.IDLE;
		// Status = new PlayerStatus ();
        // anim = GetComponent<Animator>();
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	// if (act == ACT.MOVING)
	// 	{
	// 		Hex nextHex = MoveHexes[0];
	// 		float dis = Vector3.Distance(transform.position, nextHex.transform.position);
	// 		if(dis > 0.1f)
	// 		{
	// 			transform.position += (nextHex.transform.position - transform.position).normalized * Status.MoveSpeed * Time.smoothDeltaTime;
	// 		}
	// 		else
	// 		{
	// 			transform.position = nextHex.transform.position;
	// 			MoveHexes.RemoveAt(0);
	// 			if(MoveHexes.Count == 0)
	// 			{
	// 				act = ACT.IDLE;
	// 				CurHex = nextHex;
	// 				PlayerManager.instance.TurnOver();
	// 			}
	// 		}
	// 	}
	}

	public void GetDamage(int dam)
	{
		Status.CurHp -= dam;
		if (Status.CurHp <= 0) {
			Debug.Log ("died");
            anim.SetTrigger("Die");
           act = ACT.DIYING;
           removeTime =+ Time.deltaTime;
			//PlayerManager.instance.RemovePlayer(this);
		}
        else
        {
            anim.SetTrigger("Hited");
        }
	}
}
