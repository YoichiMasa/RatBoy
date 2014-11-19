/*using UnityEngine;
using UnityEditor;
using System.Collections;

public class ItemDatabaseManager : EditorWindow{

	[MenuItem ("Item Database Manager/Item Database Editor")]
	static void Init()
	{
		ItemDatabaseManager window = (ItemDatabaseManager)EditorWindow.GetWindow (typeof(ItemDatabaseManager));
	window.Show();

	}

	public enum ItemType
		{
			Item,
			Food,
			Weapon,
			KeyItem
		}
	public ItemType currentItemType = ItemType.Item;
	public ItemManager itemManager;
	
	string newItemName = "";
	string newItemDescription = "";
	int newBaseWeight = 1;
	int newBaseHeal = 0;
	int newWpnBaseDamage = 10;
	int newWpnBaseDurability = 10;

	void OnGUI()
	{
		itemManager = EditorGUILayout.ObjectField("Item Manager", itemManager, typeof(ItemManager), true) as ItemManager;

		if (itemManager != null) 
		{
			currentItemType = (ItemType)EditorGUILayout.EnumPopup (currentItemType);
			newItemName = EditorGUILayout.TextField ("Item Name: ", newItemName);
			newItemDescription = EditorGUILayout.TextField ("Item Description: ", newItemDescription);
			newBaseWeight = EditorGUILayout.IntField ("Weight: ", newBaseWeight);

			switch(currentItemType)
			{
			case ItemType.Food:
				newBaseHeal = EditorGUILayout.IntField ("Heal: ", newBaseHeal);
				break;
			case ItemType.Weapon:
				newWpnBaseDamage = EditorGUILayout.IntField ("Damage: ", newWpnBaseDamage);
				newWpnBaseDurability = EditorGUILayout.IntField ("Durability: ", newWpnBaseDurability);
				break;
			case ItemType.KeyItem:

				break;
			}
			if (GUILayout.Button ("Add New Item")) 
			{
				switch(currentItemType)
				{
				case ItemType.Food:
					Food newFood = (Food)ScriptableObject.CreateInstance<Food> ();
					newFood.itemName = newItemName;
					newFood.itemDescription = newItemDescription;
					newFood.itemBaseWeight = newBaseWeight;
					itemManager.itemDatabase.Add (newFood);
					break;
				case ItemType.Weapon:
					Weapon newWeapon = (Weapon)ScriptableObject.CreateInstance<Weapon> ();
					newWeapon.itemName = newItemName;
					newWeapon.itemDescription = newItemDescription;
					newWeapon.itemBaseWeight = newBaseWeight;
					itemManager.itemDatabase.Add (newWeapon);
					break;
				case ItemType.KeyItem:
					KeyItem newKeyItem = (KeyItem)ScriptableObject.CreateInstance<KeyItem> ();
					newKeyItem.itemName = newItemName;
					newKeyItem.itemDescription = newItemDescription;
					newKeyItem.itemBaseWeight = newBaseWeight;
					itemManager.itemDatabase.Add (newKeyItem);
					break;
				}
			}
		}
	}
}*/