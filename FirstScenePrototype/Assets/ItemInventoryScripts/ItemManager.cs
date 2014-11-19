using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemManager{
	public List<Item> itemDatabase;
	protected ItemManager()
	{
		itemDatabase = new List<Item>();
	}
	private static ItemManager instance = null;

	public static ItemManager getInstance
	{
		get
		{
			if (ItemManager.instance == null)
			{
				ItemManager.instance = new ItemManager();
			}
			return instance;
		}
	}
	//public List<Item> itemDatabase = new List<Item>();

	public void createDatabase()
	{
		instance = this;
		Food newFood = (Food)ScriptableObject.CreateInstance<Food> ();
		newFood.ConfigureFood ("Cheese", 200, "Oooh, Cheese!", 5, 3, 2, Item.ItemType.Food, 10);
		addItem (newFood);
		for (int i = 0; i < itemDatabase.Count; i++) 
		{
			itemDatabase[i].itemIcon = getIcon(itemDatabase[i].itemName);
		}


	}

	public void addItem(Item item)
	{
		itemDatabase.Add (item);
	}

	public Item SearchNewItem(int newItem)
	{
		Item toBeAdded = (Item)ScriptableObject.CreateInstance<Item> ();;

		for (int i = 0; i < itemDatabase.Count; i++) 
		{
			if(itemDatabase[i].itemID == newItem)
			{
				toBeAdded = itemDatabase[i];
			}
		}
		return toBeAdded;
	}

	public Texture2D getIcon(string name)
	{
		Texture2D icon = Resources.Load<Texture2D>("ItemIcons/" + name);

		return icon;
	}


	/*void OnGUI()
	{
		//GUILayout.Button ("Add New Item");
		//GUILayout.Button ("Add New Weapon");
		newItemName = GUILayout.TextField(newItemName);
		newItemDescription = GUILayout.TextField(newItemDescription);
		newBaseWeight = System.Convert.ToInt32(GUILayout.TextField(newBaseWeight.ToString()));
		if (GUILayout.Button ("Add New Item")) 
		{
			//isClicked = true;

			//if(isClicked == true)
			//{
				
				newItemName = GUILayout.TextField(newItemName);
				newItemDescription = GUILayout.TextField(newItemDescription);
				newBaseWeight = System.Convert.ToInt32(GUILayout.TextField(newBaseWeight.ToString()));
				//newItemID = System.Convert.ToInt32 (GUILayout.TextField (newItemID.ToString ()));
				//GUILayout.Button ("Confirm");

				
				//if(GUILayout.Button ("Confirm"))
				//{
					Item newItem = ScriptableObject.CreateInstance<Item>();
					
					newItem.itemName = newItemName;
					newItem.itemDescription = newItemDescription;
					newItem.itemBaseWeight = newBaseWeight;
					
					itemDatabase.Add (newItem);
					
				//}
			//}
		}*/



		/*if (GUILayout.Button ("Add New Weapon")) 
		{
			Weapon newWeapon = ScriptableObject.CreateInstance<Weapon>();
			newWeapon.itemName = newItemName;
			newWeapon.itemDescription = newItemDescription;
			newWeapon.itemBaseWeight = newBaseWeight;
			newWeapon.wpnBaseDurability =
		}
		for(int i = 0; i < itemDatabase.Count; i++)
		{
			GUILayout.Label(itemDatabase[i].itemName + " " + itemDatabase[i].itemDescription + " " + itemDatabase[i].itemBaseWeight);		}
	}*/

}
