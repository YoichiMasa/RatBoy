	using UnityEngine;
using System.Collections;

public class HealthStaminaController : MonoBehaviour {

	public PlayerMovement player;
	public PlayerInventory inven;
	public GUISkin HpStam;
	GameObject hpSys;
	public float HP = 100f;
	public float maxHP = 100f;
	public float stamina = 1000f;
	public float maxStamina = 1000f;
	public float minStamina = -1000f;

	//Base Values for Stamina Consumption
	public float baseIdle = 1f;
	public float baseMove = 0.05f;
	public float baseSprint = 0.1f;
	public float baseJump = 2f;
	public float baseClimb = 0.1f;
	public float baseAttack;

	//visuals
	public Texture2D HPBar;
	public Material mat1;
	public Texture2D StaminaBar;
	public Material mat2;
	public Texture2D backing;
	public Material mat3;

	//screen coodinates and dimensions
	public float x1 = 0;
	public float y1 = 0;
	public float w1;
	public float h1;

	public float x2 = 0;
	public float y2 = 0;
	public float w2;
	public float h2;

	//Update Draw Animator things
	private float healthy;
	private float energized;

	//GUITexts
	public GUIText HealthGUI;
	public GUIText StaminaGUI;

	int defaultScreen = 1200;
	

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMovement> ();
		inven = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerInventory> ();
		hpSys = GameObject.FindGameObjectWithTag ("HPSYS");
	}
	
	// Update is called once per frame
	void Update () {
		if (player.alive == true) {
						//for Animating Health and Stamina Bars
						healthy = (1 - (HP / maxHP));
						//		if(healthy == 0f){
						//			healthy = 0.1f;
						//		}
						mat1.SetFloat ("_Cutoff", healthy);
			
						energized = (1 - (stamina / maxStamina));
						//		if(energized == 0f){
						//			energized = 0.1f;
						//		}
						mat2.SetFloat ("_Cutoff", energized);

						//Moving
						if (player.rigidbody.velocity != Vector3.zero) 
						{
							stamina = stamina - ((float)((baseMove + baseMove*(inven.currentWeight/inven.weightLimit)))*Time.deltaTime);
						}
						if (stamina <= 0) {
								HP -= .01f;
						}
				}


	}

	void OnGUI() {
		GUI.skin = HpStam;
		GUIStyle hp = GUI.skin.GetStyle ("HP");
		if(Event.current.type.Equals(EventType.Repaint))
		{
			Rect box3 = new Rect (Screen.width/2 - Screen.width*x1, Screen.height - Screen.height *y1, Screen.width*w1, Screen.height*h1);
			Graphics.DrawTexture (box3, backing, mat3);
			
			Rect box4 = new Rect (Screen.width/2 - Screen.width*x2, Screen.height - Screen.height *y2, Screen.width*w2, Screen.height*h2);
			Graphics.DrawTexture (box4, backing, mat3);



			Rect box1 = new Rect(Screen.width/2 - Screen.width*x1, Screen.height - Screen.height *y1, Screen.width*w1, Screen.height*h1);
			Graphics.DrawTexture(box1, HPBar, mat1);

			Rect box2 = new Rect(Screen.width/2 - Screen.width*x2, Screen.height - Screen.height *y2, Screen.width*w2, Screen.height*h2);
			Graphics.DrawTexture(box2, StaminaBar, mat2);


			//Health
			hp.fontSize = (int)(getScale() * 60f);
			Rect hpText = new Rect(Screen.width/2 - Screen.width*.49f, Screen.height - Screen.height *.164f, Screen.width*w1, Screen.height*h1);
			if(HP == maxHP)
			{
				GUI.Label(hpText, ""+HP+" |", HpStam.GetStyle("HP"));
			}
			else if(HP < 100 && HP > 9)
			{
				GUI.Label(hpText, "0"+HP+" |", HpStam.GetStyle("HP"));
			}
			else if(HP <= 9)
			{
				GUI.Label(hpText, "00"+HP+" |", HpStam.GetStyle("HP"));
			}

			//Stamina

			if(stamina == maxStamina)
			{
				StaminaGUI.text = ""+((stamina/maxStamina)*100)+".0% |";
			}
			else
			{
				StaminaGUI.text = ""+((stamina/maxStamina)*100)+"% |";
			}
		}
	}
	float getScale()
	{
		return Screen.height / defaultScreen;
	}
}
