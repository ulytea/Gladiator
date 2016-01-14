using UnityEngine;
using System.Collections;

public class UserPlayer : PlayerBase
{


    void Awake()
    {
        anim = GetComponent<Animator>();
        act = ACT.IDLE;
        Status = new PlayerStatus();
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlayerManager pm = PlayerManager.instance;


        if (act == ACT.IDLE)
        {
              if (pm.Players[pm.CurTurnIdx] == this)
            {
                MapManager.instance.SetHexColor(CurHex, Color.grey);
                anim.SetBool("Attack" , false);
        
            }
        }
        if (act == ACT.MOVING)
        {
            anim.SetBool("Run", true);
            Hex nextHex = MoveHexes[0];
            float dis = Vector3.Distance(transform.position, nextHex.transform.position);
            if (dis > 0.1f)
            {
                transform.position += (nextHex.transform.position - transform.position).normalized * Status.MoveSpeed * Time.smoothDeltaTime;

                transform.rotation = Quaternion.LookRotation((nextHex.transform.position - transform.position).normalized);
            }
            else
            {
                transform.position = nextHex.transform.position;
                MoveHexes.RemoveAt(0);
                if (MoveHexes.Count == 0)
                {
                    act = ACT.IDLE;
                    CurHex = nextHex;
                    anim.SetBool("Run", false);
                    PlayerManager.instance.TurnOver();
                }
            }
        }
    }
 
}
