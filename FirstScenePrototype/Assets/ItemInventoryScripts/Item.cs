using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item : ScriptableObject {

	public enum ItemType
	{
		Food,
		Weapon
	}
	public string itemName = "";
	public int itemID = 0;
	public string itemDescription = "";
	public int itemBaseWeight = 0;
	public int itemHeight;
	public int itemWidth;
	public ItemType itemType;
	public Texture itemIcon;

	public Item()
	{}

	public void ConfigureItem(string name, int ID, string description, int baseWeight, int height, int width, ItemType type)
	{
		itemName = name;
		itemID = ID;
		itemDescription = description;
		itemBaseWeight = baseWeight;
		itemHeight = height;
		itemWidth = width;
		itemType = type;
		itemIcon = Resources.Load("ItemIcons/" + name, typeof(Texture)) as Texture;
	}
}





