using UnityEngine;
using System.Collections;

public class EffectManager : Singleton<EffectManager>
{

    public GameObject GO_Attack_effect;
    public GameObject GO_Damage;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ShowEffect(Hex hex)
    {
        GameObject go = (GameObject)Instantiate(GO_Attack_effect, hex.transform.position, hex.transform.rotation);
    }
    public void ShowDamage(Hex hex, int damage)
    {
        GameManager.instance.damagedHex = hex;
        GameManager.instance.damage = damage;
        GameManager.instance.StartCoroutine("ShowDamage");
    }

}
