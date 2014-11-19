using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(ItemManager))]
public class ItemManagerInspector : Editor{

	bool showingWeapons = false;
	bool showingFood = false;
	bool showingKeyItems = false;

	public override void OnInspectorGUI()
	{
		ItemManager im = ItemManager.getInstance;

		List<Weapon> weapons = new List<Weapon> ();
		List<Food> food = new List<Food> ();
		List<KeyItem> keyItems = new List<KeyItem> ();

		for (int i = 0; i < im.itemDatabase.Count; i++) 
		{
			if(im.itemDatabase[i].GetType () == typeof(Weapon))
			{
				Weapon w = (Weapon) im.itemDatabase[i];
				w.itemID = 100 + weapons.Count;
				weapons.Add (w);
			}

			if(im.itemDatabase[i].GetType () == typeof(Food))
			{
				Food f = (Food) im.itemDatabase[i];
				f.itemID = 200 + food.Count;
				food.Add  (f);

			}

			if(im.itemDatabase[i].GetType () == typeof(KeyItem))
			{
				KeyItem ki = (KeyItem) im.itemDatabase[i];
				ki.itemID = 300 + keyItems.Count;
				keyItems.Add (ki);
				//keyItems[i].itemID = 300 + keyItems.Count;
			}
		}

		showingWeapons =EditorGUILayout.Foldout (showingWeapons, "Weapons");
		if(showingWeapons)
		{	
			EditorGUI.indentLevel = 1;
			//Displays Weapons
			for (int i = 0; i < weapons.Count; i++) 
			{
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField(weapons[i].itemName);
				if(GUILayout.Button ("Remove"))
				   {
						im.itemDatabase.Remove (weapons[i]);
					}
				EditorGUILayout.EndHorizontal();

				EditorGUI.indentLevel = 2;
				weapons[i].itemName = EditorGUILayout.TextField ("Name: ", weapons[i].itemName);
				weapons[i].itemID = EditorGUILayout.IntField ("Item ID: " , weapons[i].itemID);
				weapons[i].itemDescription = EditorGUILayout.TextField ("Description: ", weapons[i].itemDescription);
				weapons[i].itemBaseWeight = int.Parse (EditorGUILayout.TextField ("Weight: ", weapons[i].itemBaseWeight.ToString()));
				weapons[i].itemIcon = (Texture) EditorGUILayout.ObjectField("Script: ", weapons[i].itemIcon, typeof(Texture), true);
				weapons[i].wpnBaseDamage = int.Parse (EditorGUILayout.TextField ("Damage: ", weapons[i].wpnBaseDamage.ToString()));
				weapons[i].wpnBaseDurability = int.Parse (EditorGUILayout.TextField ("Durability: ", weapons[i].wpnBaseDurability.ToString()));
				weapons[i].itemHeight = int.Parse (EditorGUILayout.TextField ("Height: ", weapons[i].itemHeight.ToString()));
				weapons[i].itemWidth = int.Parse (EditorGUILayout.TextField ("Width: ", weapons[i].itemWidth.ToString()));
				EditorGUI.indentLevel = 2;
				EditorGUILayout.Space();
			}
			if(GUILayout.Button("Add New Weapon"))
			{
				Weapon newWeapon = (Weapon)ScriptableObject.CreateInstance<Weapon> ();
				//newWeapon.ConfigureItem("New Weapon", "", 0, 1, 1);
				//newWeapon.itemName = "New Weapon";
				//newWeapon.itemDescription = "";
				//newWeapon.itemBaseWeight = 1;
				newWeapon.wpnBaseDamage = 1;
				newWeapon.wpnBaseDurability = 1;
				im.itemDatabase.Add (newWeapon);
			}

			EditorGUI.indentLevel = 0;
		}

		showingFood =EditorGUILayout.Foldout (showingFood, "Food");
		if(showingFood)
		{	
			EditorGUI.indentLevel = 1;
			//Displays Food
			for (int i = 0; i < food.Count; i++) 
			{
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField(food[i].itemName);
				if(GUILayout.Button ("Remove"))
				{
					im.itemDatabase.Remove (food[i]);
				}
				EditorGUILayout.EndHorizontal();

				EditorGUI.indentLevel = 2;
				food[i].itemName = EditorGUILayout.TextField ("Name: ", food[i].itemName);
				food[i].itemID = EditorGUILayout.IntField ("Item ID: " , food[i].itemID);
				food[i].itemDescription = EditorGUILayout.TextField ("Description: ", food[i].itemDescription);
				food[i].itemBaseWeight = int.Parse (EditorGUILayout.TextField ("Weight: ", food[i].itemBaseWeight.ToString()));
				food[i].itemIcon = (Texture) EditorGUILayout.ObjectField("Script: ", food[i].itemIcon, typeof(Texture), true);
				food[i].baseHeal = int.Parse (EditorGUILayout.TextField ("Heal: ", food[i].baseHeal.ToString()));
				food[i].itemHeight = int.Parse (EditorGUILayout.TextField ("Height: ", food[i].itemHeight.ToString()));
				food[i].itemWidth = int.Parse (EditorGUILayout.TextField ("Width: ", food[i].itemWidth.ToString()));
				EditorGUI.indentLevel = 2;
				EditorGUILayout.Space();
			}
			if(GUILayout.Button("Add New Food"))
			{
				Food newFood = (Food)ScriptableObject.CreateInstance<Food> ();
				//newFood.ConfigureItem("New Food", "", 0, 1, 1);
				//newFood.itemDescription = "";
				//newFood.itemBaseWeight = 1;
				newFood.baseHeal = 10;
				im.itemDatabase.Add (newFood);
			}

			EditorGUI.indentLevel = 0;
		}

		showingKeyItems =EditorGUILayout.Foldout (showingKeyItems, "Key Items");
		if(showingKeyItems)
		{
			EditorGUI.indentLevel = 1;
			//Displays Key Items
			for (int i = 0; i < keyItems.Count; i++) 
			{
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField(keyItems[i].itemName);
				if(GUILayout.Button ("Remove"))
				{
					im.itemDatabase.Remove (keyItems[i]);
				}
				EditorGUILayout.EndHorizontal();

				EditorGUI.indentLevel = 2;
				keyItems[i].itemName = EditorGUILayout.TextField ("Name: ", keyItems[i].itemName);
				keyItems[i].itemID = EditorGUILayout.IntField ("Item ID: " , keyItems[i].itemID);
				keyItems[i].itemDescription = EditorGUILayout.TextField ("Description: ", keyItems[i].itemDescription);
				keyItems[i].itemBaseWeight = int.Parse (EditorGUILayout.TextField ("Weight: ", keyItems[i].itemBaseWeight.ToString()));
				keyItems[i].itemIcon = (Texture) EditorGUILayout.ObjectField("Script: ", keyItems[i].itemIcon, typeof(Texture), true);
				keyItems[i].itemHeight = int.Parse (EditorGUILayout.TextField ("Height: ", keyItems[i].itemHeight.ToString()));
				keyItems[i].itemWidth = int.Parse (EditorGUILayout.TextField ("Width: ", keyItems[i].itemWidth.ToString()));
				EditorGUI.indentLevel = 2;
				EditorGUILayout.Space();
			}
			if(GUILayout.Button("Add New Key Item"))
			{
				KeyItem newKeyItem = (KeyItem)ScriptableObject.CreateInstance<KeyItem> ();
				//newKeyItem.ConfigureItem("New Key Item", "", 0, 1, 1);
				//newKeyItem.itemName = "New Key Item";
				//newKeyItem.itemDescription = "";
				//newKeyItem.itemBaseWeight = 1;
				im.itemDatabase.Add (newKeyItem);
			}
		
			EditorGUI.indentLevel = 0;
		}
	}
}
