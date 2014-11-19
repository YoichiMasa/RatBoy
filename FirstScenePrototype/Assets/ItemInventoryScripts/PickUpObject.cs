using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PickUpObject : MonoBehaviour {
	RaycastHit	hit;
	public bool hasCollided = false;
	string labelText = "";
	public PlayerInventory player;
	public ItemManager im;
	Item item;
	public int itemID;
	void OnStart()
	{
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerInventory>();
		im = ItemManager.getInstance;
	}
	void OnCollisionEnter(Collision col)
	{
		if(col.transform.tag == "Player")
		{
			hasCollided = true;
			labelText = "Pick up: E";
		}
	}

	// Update is called once per frame
	void Update () 
	{
		if(hasCollided == true)
		{
			if(Input.GetKeyDown(KeyCode.F))
			{
				pickUp();
			}
		}
	}

	void pickUp()
	{
		//item = im.SearchNewItem (itemID);
		player.AddItemToInventory (itemID);
		GameObject.Destroy(this.gameObject);
	}
}
