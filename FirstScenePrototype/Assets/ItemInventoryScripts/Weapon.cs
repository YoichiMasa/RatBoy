using UnityEngine;
using System.Collections;

[System.Serializable]
public class Weapon: Item
{
	public int wpnBaseDamage = 10;
	public int wpnBaseDurability = 10;

	public void ConfigureWeapon(string name, int ID, string description, int baseWeight, int height, int width, ItemType type, int wpnDamage,
	              	int wpnDurability)
	{
		base.ConfigureItem (name, ID, description, baseWeight, height, width, type);
		itemName = name;
		itemID = ID;
		itemDescription = description;
		itemBaseWeight = baseWeight;
		itemHeight = height;
		itemWidth = width;
		wpnBaseDamage = wpnDamage;
		wpnBaseDurability = wpnDurability;
	}
}
