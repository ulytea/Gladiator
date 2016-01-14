using UnityEngine;
using System.Collections;

public class SoundeManager : Singleton<SoundeManager> {
    public AudioClip AC_ATTACK;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public void PlayAttackSound()
    {
        AudioSource.PlayClipAtPoint( AC_ATTACK , this.transform.position);
    }
}
