using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
	public Texture defaultTexture;
	public List<Item> myInventory = new List<Item> ();
	public int weightLimit = 100;
	public int currentWeight = 0;
	int gridWidth = 10;
	int gridHeight = 10;
	int defaultScreenX = 1366;
	int defaultScreenY = 768;
	Vector2 screen;
	private Vector2 scale;
	private ItemManager im;
	private bool showInventory = false;
	private bool showTooltip = false;
	private string toolTip;


	void Start()
	{
		//myInventory = new List<Item> ();
		for (int i = 0; i < (gridWidth * gridHeight); i++) 
		{
			//slots.Add ((Item)ScriptableObject.CreateInstance<Item> ());
			myInventory.Add ((Item) ScriptableObject.CreateInstance<Item>());
		}
			//[gridWidth*gridHeight];// I like to use a single dimension array.
		im = ItemManager.getInstance;
		im.createDatabase();
		defaultTexture = Resources.Load<Texture> ("ItemIcons/Name");
		AddItemToInventory (200);
//		AddItemToInventory (101);
//		AddItemToInventory (200);
//		AddItemToInventory (100);
//		AddItemToInventory (101);
//		AddItemToInventory (200);
//		AddItemToInventory (100);
//		AddItemToInventory (101);
//		AddItemToInventory (101);
//		AddItemToInventory (200);
//		AddItemToInventory (100);
//		AddItemToInventory (101);
//		AddItemToInventory (101);
//		AddItemToInventory (200);
//		AddItemToInventory (100);
//		AddItemToInventory (101);
//		for(int i = 0; i < myInventory.Count; i++)
//		{
//			//print (myInventory[i].itemID);
//			if(myInventory[i].itemID != 0)
//			{
//				print (i);
//				myInventory[i].isFilled = true;
//			}
//			//print (myInventory[i].itemName);
//			
//		}
	}

	void Update()
	{
		if(Input.GetButtonDown("Inventory"))
		{
			showInventory = !showInventory;
		}
		//print ("Current Weight: " + currentWeight);
	}
	
	public int AddItemToInventory(int itemID)
	{
		int intialIndexLocation = -1;   //Set the initial position out of the inventory range. This will get set to the first empty slot we encounter and then reset if there aren't enough slots from that point
		//Add item in
		if(itemID != 0)
		{
			Item temp =  im.SearchNewItem(itemID);      //Set up a temporary item to compare with
			if(checkWeight(temp))
			{
				if(SlotsRemaining() >= temp.itemHeight*temp.itemWidth )         //Simple check to see if you have slots left.
				{
					bool canFit = false;                                //will be set to true later if the item fits
					bool firstFoundLocation = true;                     //if this becomes false, we have found our first empty location, it gets reset if there aren't enough spaces
					//between the item's height and width from the intialIndexLocation
					int count = 0;                                      //This gets incremented each time we encounter an empty space.
					//It gets reset if there aren't enough empty spaces between the item's height and width from the intialIndexLocation
					for(int i = 0; i < gridWidth-(temp.itemWidth-1); i ++)  //Iterate through the width of the grid, excluding spaces width-1 from the edge
					{
						for(int t = 0; t < (gridHeight)-(temp.itemHeight-1); t++)       //Iterate through the height of the grid, excluding spaces height-1 from the edge
						{
							//print ("ItemWidth: " + temp.itemWidth);	
//							print ("Location: " + i+(gridHeight*t));
							if(myInventory[i+(gridHeight*t)].itemID == 0 && !canFit)      //If the slot at i(itemWidth) + (totalheight * t(heightLoop) is null  canFit is still false (This goes down the columns first, then goes to the next row)
							{
								int neededCount = temp.itemHeight*temp.itemWidth;           //The amount of slots needed for the item to fit
								for(int j = 0; j < temp.itemWidth; j++)                 //Loop through the required spaces for the item width
								{
									if(myInventory[i+(gridHeight*t) + j].itemID == 0 && !canFit)  //If the space at i(widthLoop) + j(itemWidthLoop) + (Totalheight * t(heightLoop) + k(itemHeightLoop)) is null
										//and we aren't sure if we can fit yet
									{
										//valid width for the location
										for( int k = 0; k < temp.itemHeight; k++)                   //Loop through the require spaces for the item height
										{
											if(myInventory[i+(gridHeight*(t+k)) + j].itemID == 0)   //If the slot at i(widthLoop) + j(itemWidthLoop) + (Totalheight * t(heightLoop) + k(itemHeightLoop)) is null...
											{
												if(firstFoundLocation)                          //If this is the first found empty space...
												{
													firstFoundLocation = false;                 //Turn off firstFoundLocation for now so we don't continually reset our intialIndexLocation.
													//We'll turn this off later if it turns out that there aren't enough spaces
													intialIndexLocation = i+(gridWidth*t);      //Set our initial position. If it turns out that there aren't enough spaces, we'll reset it to -1 below
												}
												//Valid Height for the location.. Place it here.
												count+=1;                                       //Add 1 to our count for each empty space we find
												if(count == neededCount)
												{
													
													canFit = true;                              //If the item is a 1x1, just set canFit to true now
													break;
												}
											}
										}
									}
								}
								if(count >= neededCount)
								{
									//intialIndexLocation = i+(gridWidth*t);
									canFit = true;
									break;
								} else if(count < neededCount){
									canFit = false;
									firstFoundLocation = true;
									count = 0;
									intialIndexLocation = -1;
								}
							}
						}  
					}
				}
				if(SlotsRemaining() == myInventory.Count)  //If the inventory is empty, just place is at 0,0
				{
					intialIndexLocation = 0;
				}
				if(intialIndexLocation > -1)    //If our initialindexlocation is in range, set the spaces equal the the item
				{
					for(int i = 0; i < temp.itemWidth; i++)
					{
						for(int t = 0; t < temp.itemHeight; t++)
						{
							if(i == 0 && t == 0)
							{
								Item clone = (Item)Instantiate(temp);
								clone.itemIcon = im.getIcon(clone.itemName);
								myInventory[intialIndexLocation+i+(gridHeight*t)] = clone;
								currentWeight = currentWeight + clone.itemBaseWeight;
							}
							else if (i > 0 || t > 0)
							{
								//temp.isFilled = true;
								myInventory[intialIndexLocation+i+(gridHeight*t)] = temp;
							}
						}
					}
				}
			}
			else
			{
				print ("Inventory is FULL");
			}
		}
		return intialIndexLocation;
	}
	
	public int RemoveItemFromInventory(Item item)
	{
		int intialIndexLocation = -1;   //Set the initial position out of the inventory range
		//Add item in
		if(item != null)
		{
			Item temp =   item;      //Set up a temporary item to compare with
			
			for(int i = 0; i < gridWidth; i ++)                 //Iterate through the width of the grid
			{
				for(int t = 0; t < (gridHeight); t++)   //Iterate through the height of the grid
				{
					if(myInventory[i+(gridHeight*t)] == item)   //If the slot at widthLoop + (heightTotal * t(heightLoop) is null  canFit is still false (This goes down the columns first, then goes to the next row)
					{
						intialIndexLocation = i+(gridHeight*t); //We found the first slot for the item, so break out of the loop
						break;
						
					}
				}
				
				if(intialIndexLocation > -1){                   //If we found the first item slot, break out of the first loop as well
					break;
				}
			}
			
			if(intialIndexLocation > -1)    //If our initialindexlocation is in range, set the spaces equal to the item width and height
			{
				for(int i = 0; i < temp.itemWidth; i++) //Loop through the spaces from the initalIndexLocation to the width of the item
				{
					for(int t = 0; t < temp.itemHeight; t++)    //Loop through the spaces from the initalIndexLocation to the height of the item
					{
						myInventory[intialIndexLocation+i+(gridHeight*t)] = (Item) ScriptableObject.CreateInstance<Item>();   //And set those spaces equal to null
						currentWeight = currentWeight - item.itemBaseWeight;
					}
				}
			}

		}
		//DrawInventory();
		return intialIndexLocation;
	}
	
	/*string txtWidth = "";
	string txtHeight = "";*/
	
	void OnGUI()
	{	
		toolTip = "";
		if(showInventory)
		{
			DrawInventory();
			if (showTooltip) 
			{
				GUI.Box (new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 200, 200), toolTip);
			}
		}
	}
		
	void DrawInventory()
	{
//		for(int i = 0; i < myInventory.Count; i++)
//		{
//
//			if(myInventory[i].itemID != 0)
//			{
//				if(myInventory[i].isFilled == false)
//				{
//					myInventory[i].isFilled = false;
//				}
//			}
//
//		}
		screen = new Vector2 (Screen.width, Screen.height);
		scale = new Vector2 (screen.x / defaultScreenX, screen.y / defaultScreenY);
		float sfactor = Screen.height * .06f;
		float offSetX = Screen.width / 2 - Screen.width * .139f;
		float offSetY = Screen.height / 2 - Screen.height * .29f;
		for(int i = 0; i < gridWidth; i ++)  //Don't need to check slots that can't handle the width
		{  
			for(int t = 0; t < gridHeight; t++)   //Don't need to check slots that can't handle the height.
			{
				Rect slotRect = new Rect((offSetX+(i*sfactor)), (offSetY+(t*sfactor)), sfactor, sfactor);
				//slots[i+(gridHeight*t)] = myInventory[i+(gridHeight*t)];
				if(myInventory[i+(gridHeight*t)].itemID == 0)
				{
					GUI.DrawTexture(slotRect, defaultTexture);
				}
				else
				{
					Item temp = myInventory[i+(gridHeight*t)];
					if(temp.name.Contains("(Clone)"))
					{
							int w = temp.itemWidth;
							int h = temp.itemHeight;
							Rect buttonRect = new Rect((offSetX+(i*sfactor)), offSetY+(t*sfactor), sfactor*w, sfactor*h);
							if(GUI.Button(buttonRect, temp.itemIcon))
							{
								RemoveItemFromInventory(myInventory[i+(gridHeight*t)]);
							}
							//temp.isFilled = true;
					}

				}
			
				Rect toolTipRect = new Rect((100+(i*30)), 100+(t*30), 30, 30);
				if(toolTipRect.Contains (Event.current.mousePosition) && myInventory[i+(gridHeight*t)].itemID != 0)
				{
					toolTip = CreateTooltip (myInventory[i+(gridHeight*t)]);
					showTooltip = true;
				}
			}
		}
	}

	string CreateTooltip(Item item)
	{
		string toolTip = "";
		if (item.GetType () == typeof(Weapon)) 
		{
			Weapon weapon = (Weapon)item;
			toolTip = weapon.itemName + "\n\n" + weapon.itemDescription + 
				"\n\n Weight: " + weapon.itemBaseWeight + 
					"\n\n Damage: " + weapon.wpnBaseDamage +
					"\n\n Durability: " + weapon.wpnBaseDurability;
		}
		else if (item.GetType () == typeof(Food)) 
		{
			Food food = (Food)item;
			toolTip = food.itemName + "\n\n" + food.itemDescription + 
				"\n\n Weight: " + food.itemBaseWeight + 
					"\n\n Heal: " + food.baseHeal;
		}
		else if (item.GetType () == typeof(KeyItem)) 
		{
			KeyItem keyItem = (KeyItem)item;
			toolTip = keyItem.itemName + "\n\n" + keyItem.itemDescription + 
				"\n\n Weight: " + keyItem.itemBaseWeight;
		}
		return toolTip;
	}

	public int SlotsRemaining()
	{
		int count = 0;
		for(int i = 0; i < myInventory.Count; i++)
		{
			if(myInventory[i].itemID == 0)
			{
				count++;
			}
		}
		return count;
	}
	public bool checkWeight(Item item)
	{
		bool notFull = false;
		if(currentWeight+item.itemBaseWeight < weightLimit)
		{
			notFull = true;
		}
		return notFull;
	}
}
