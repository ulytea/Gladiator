using UnityEngine;
using System.Collections;
public class AIPlayer : PlayerBase
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
        if(removeTime != 0)
        {
            removeTime += Time.deltaTime;
            if(removeTime >= 1.5f )
            {
                PlayerManager.instance.TurnOver();
                PlayerManager.instance.RemovePlayer(this);
            }
        }
        PlayerManager pm = PlayerManager.instance;
        

        if (act == ACT.IDLE)
        {
            anim.SetBool("Attack",false);
            if (pm.Players[pm.CurTurnIdx] == this)
            {
                MapManager.instance.SetHexColor(CurHex, Color.grey);

            }
            if (pm.Players[pm.CurTurnIdx] == this)
            {
                aiProc();
            }
        }
        else if (act == ACT.MOVING)
        {
            anim.SetBool("Run",true);
            if (MoveHexes.Count == 0)
            {
                act = ACT.IDLE;
                PlayerManager.instance.TurnOver();
                return;
            }
            Hex nextHex = MoveHexes[0];
            float dis = Vector3.Distance(transform.position, nextHex.transform.position);
            if (dis > 0.1f)
            {
                transform.position += (nextHex.transform.position - transform.position).normalized * Status.MoveSpeed * Time.smoothDeltaTime;
            }
            else
            {
                transform.position = nextHex.transform.position;
                MoveHexes.RemoveAt(0);
                if (MoveHexes.Count == 0)
                {
                    anim.SetBool("Run",false);
                    act = ACT.IDLE;
                    CurHex = nextHex;
                    PlayerManager.instance.TurnOver();
                }
            }
        }
    }
    public void aiProc()
    {
        AI.instance.MoveAIToNearUserPlayer(this);

        if (act == ACT.IDLE)
        {
            AI.instance.AtkAItoUser(this);
        }
    }

}
