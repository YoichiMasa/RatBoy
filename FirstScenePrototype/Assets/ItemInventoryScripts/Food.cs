using UnityEngine;
using System.Collections;


[System.Serializable]
public class Food: Item
{
	public readonly int FOOD_ID = 200;
	public int numFood = -1;
	public int baseHeal = 10;
	ItemManager im;

	void onStart()
	{
		im = ItemManager.getInstance;
	}
	public void ConfigureFood(string name, int ID, string description, int baseWeight, int height, int width, ItemType type, int heal)
	{
		base.ConfigureItem (name, ID, description, baseWeight, height, width, type);
		itemName = name;
		itemID = ID;
		itemDescription = description;
		itemBaseWeight = baseWeight;
		itemHeight = height;
		itemWidth = width;
		baseHeal = heal;
	}

//	public System.Collections.Generic.IEnumerable<int> foodCount()
//	{
//		int ID = FOOD_ID;
//		for(int i = 0; i < im.itemDatabase.Count; i++)
//		{
//			if(im.itemDatabase[i].itemType == ItemType.Food)
//			{
//				numFood++;
//				ID += numFood;
//				yield return ID;
//			}
//		}
//
//	}
}
