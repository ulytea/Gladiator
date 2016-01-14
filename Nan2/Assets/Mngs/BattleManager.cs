using UnityEngine;
using System.Collections;

public class BattleManager : Singleton<BattleManager>
{

    private float normalAttackTime = 0f;
    private PlayerBase attackPlayer = null;
    private PlayerBase defender = null;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (normalAttackTime != 0)
        {
            normalAttackTime += Time.smoothDeltaTime;

            if (normalAttackTime >= 0.3f)
            {
                normalAttackTime = 0f;

                Debug.Log("Attack" + attackPlayer.Status.Name + " to " + defender.Status.Name);
                defender.GetDamage(10);
                EffectManager.instance.ShowDamage(defender.CurHex , 10);
              //  EffectManager.instance.ShowEffect(defender.gameObject);
               // SoundeManager.instance.PlayAttackSound();
                PlayerManager.instance.SetTurnOverTime(2f);
            }
        }
    }
    public void AttackAtoB(PlayerBase a, PlayerBase b)
    {
        a.transform.rotation = Quaternion.LookRotation((b.CurHex.transform.position - a.transform.position).normalized);
        a.anim.SetBool("Attack", true);
        a.act = ACT.ATTACKING;
        normalAttackTime = Time.smoothDeltaTime;
        attackPlayer = a;
        defender     = b;
    }
}
