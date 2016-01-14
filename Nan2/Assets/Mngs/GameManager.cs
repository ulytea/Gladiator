using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager>
{
	
	private GameObject obj;
	public PlayerBase atkedobj;
    
    void Awake()
    {
        
    }
	void Start () {
		MapManager.instance.CreateTestMap ();
		PlayerManager.instance.GenPlayerTest ();
	}
	
	void Update()
	{
		CheckMouseZoom ();
		CheckMouseButtonDown ();
		if (Input.GetMouseButtonDown (0)) 
		{
			PlayerManager pm = PlayerManager.instance;
            PlayerBase pb = pm.Players[pm.CurTurnIdx];
            
			if(pm.Players[pm.CurTurnIdx].act == ACT.ATTACKHIGHLIGHT)
			{
                BattleManager.instance.AttackAtoB(pb,atkedobj);
			}
            
			obj = GetClickedObject() ;
            
			if(obj.tag == "hex")
			{
				obj.GetComponent<Hex>().HexHl();
			}

		}
	}
    void OnGUI()
    {
        GUIManager.instance.DrawGUI();
    }
	void CheckMouseZoom()
	{

		float mouse = Input.GetAxis ("Mouse ScrollWheel");
		float mouseY = Camera.main.transform.position.y + mouse * 12;
		//Debug.Log (mouseY);
		if (mouseY < 7) {
			mouseY = 7;
		} else if (mouseY > 12) {
			mouseY = 12;
		}
		Vector3 newPos = new Vector3 (Camera.main.transform.position.x, Camera.main.transform.position.y + mouse, Camera.main.transform.position.z);
		GetComponent<Camera>().transform.position = newPos;
	}
	void CheckMouseButtonDown()
	{
		if (Input.GetMouseButtonDown (1)) {
			PlayerManager.instance.MouseInputProc(1); 
		}
	}
	public void MoveCamPosToHex(Hex hex)
	{
		float destX = hex.transform.position.x;
		float destZ = hex.transform.position.z;

		Camera.main.transform.position = new Vector3 (destX + Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + destZ);
	}
	private GameObject GetClickedObject() 
	{
		RaycastHit hit;
		GameObject target = null; 
		
		
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //마우스 포인트 근처 좌표를 만든다. 
		
		
		if( true == (Physics.Raycast(ray.origin, ray.direction * 10, out hit)))   //마우스 근처에 오브젝트가 있는지 확인
		{
			target = hit.collider.gameObject; 
		} 

		return target; 
	} 
    public Hex damagedHex;
    public int damage;
    IEnumerator ShowDamage()
    {
        GameObject Go_Damage = (GameObject)Resources.Load("Damage");
        GameObject obj = (GameObject)GameObject.Instantiate(Go_Damage, damagedHex.transform.position, GameManager.instance.transform.rotation);
        TextMesh tm = obj.GetComponent<TextMesh>();
        tm.text = " " + damage;
        tm.color = Color.red;
        
        yield return new WaitForSeconds(0.5f);
        for(float i = 1; i >= 0 ; i -= 0.05f)
        {
            tm.color = new Vector4(255,0,0,i);
            yield return new WaitForFixedUpdate();
        }
        Destroy(obj);
    }
}
