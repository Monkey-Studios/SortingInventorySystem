using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySearchSort : MonoBehaviour
{
	//Used as the searching algorithum within the first task
	public int Linear(ref List<Item> itemList, string playerSearch)
	{
		//Starting timer to test efficency of the algorithm
		SearchSortTimer.BeginTimer();
		for (int i = 0; i < itemList.Count; i++)
		{
			//Steps through the inventory one at a time until the correct search result is found
			if (itemList[i].name == playerSearch)
			{
				//Once it has found the item the player has searched for it will bring the result up
				return i;
			}
		}
		//If the item the player has searched for doesnt exist it will return nothing
		return -1;
	}
	//This function is used to switch the sorting algorithm to sort by name instead of weight
	bool sortByName = false;
	//Used as the searching algorithum within the second task
	public int Binary(ref List<Item> itemList, string playerSearch)
	{
		//Starting timer to test efficency of the algorithm
		SearchSortTimer.BeginTimer();
		//Min and Max bounds are set
		int min = 0;
		int max = itemList.Count - 1;

		while (min <= max)
		{
			//Using these bounds we have just set a middle value is determined
			int mid = (min + max) / 2;
			//Checking for the value of the middle item to corrospond with the middle value
			if (itemList[mid].name == playerSearch)
			{
				//If it does corrospond then it returns the item
				return mid;
			}
			//If the item the system is looking at is a lower value than the current one then it will either get rid of the bottom half of the array or get rid of the top half of the array
			else if (itemList[mid].name.CompareTo(playerSearch)> 0)
			{

				max = mid - 1;
			}
			else
			{

				min = mid + 1;
			}
		}

		// If it returns nothing exit
		return -1;
	}
	public void Bubble(ref List<Item> itemList, bool sortType)
	{
		//Starting timer to test efficency of the algorithm
		SearchSortTimer.BeginTimer();
		//Giving reference to the sortByName within the inventory script
		sortByName = sortType;
		//Checks the count of the item list total within the inventory
		int n = itemList.Count;
		//Checks to see if it is swapped
		//Bool set to true so it may begin search
		bool swapped = true;
		//Start sorting method
		while (swapped)
		{
			swapped = false;
			for (int i = 0; i < n - 1; i++)
			{
				if(sortByName)
                {
					//Checks the item name against other item names and orders alphabetically
					if (itemList[i].name.CompareTo(itemList[i + 1].name) > 0)
                    {
						// if needs sorting
						// swaps via a temporary variable
						Item temp = itemList[i];
						itemList[i] = itemList[i + 1];
						itemList[i + 1] = temp;

						swapped = true;
					}
                }
                else
                {
					//Checks the item weight against other items within the list and orders in numerical increase
					if (itemList[i].weight > itemList[i + 1].weight)
					{
						// if needs sorting
						// swaps via a temporary variable
						Item temp = itemList[i];
						itemList[i] = itemList[i + 1];
						itemList[i + 1] = temp;

						swapped = true;
					}
				}
			}
		}
	}
	//Task 2 sorting method Merge
	public void Merge(ref List<Item> itemList, bool sortType)
	{
		//Starting timer to test efficency of the algorithm
		SearchSortTimer.BeginTimer();
		//Giving reference to the sortByName within the inventory script
		sortByName = sortType;
		//The list is checked to have a count of atleast 1 or higher to be sorted
		if (itemList.Count <= 1)
		{
			return;
		}

		//The list is then split into two different lists
		int middle = itemList.Count / 2;

		//Creats two new lists
		//Sets a new length for each new list
		List<Item> itemListLeft = new List<Item> (middle);
		List<Item> itemListRight = new List<Item> (middle);

		//From the data given it creates a list on the left and right and the middle Variable is used to stop this half way through
		for (int i = 0; i < middle; i++)
		{
			itemListLeft.Add(itemList[i]);
		}
		//From the data given it creates a list on the left and right and the middle Variable is used to stop this half way through
		for (int i = middle; i < itemList.Count; i++)
		{
			itemListRight.Add(itemList[i]);
		}

		//The two lists will be reSorted and merged back into one list
		Merge(ref itemListLeft, sortType);
		Merge(ref itemListRight, sortType);
		itemList = ListMerge(itemListLeft, itemListRight);
	}
	private List<Item> ListMerge(List<Item> left, List<Item> right)
	{
		List<Item> result = new List<Item>();

		//While the lists contiune to have an item count it will cycle through continusly
		while (left.Count > 0 || right.Count > 0)
		{
			//When either of the lists have an item count it does a comparison
			if (left.Count > 0 && right.Count > 0)
			{
				if (sortByName)
				{
					if (left[0].name.CompareTo(right[0].name) <= 0)
					{
						//When the left has a higher count
						result.Add(left[0]);
						left.RemoveAt(0);
					}
					else
					{
						//When the right has a higher count
						result.Add(right[0]);
						right.RemoveAt(0);
					}
				}
				else
				{
					if (left[0].weight <= right[0].weight)
					{
						//When the left has a higher count
						result.Add(left[0]);
						left.RemoveAt(0);
					}
					else
					{
						//When the right has a higher count
						result.Add(right[0]);
						right.RemoveAt(0);
					}
				}
			}
			//When the left is the only one with a count
			else if (left.Count > 0)
			{
				result.Add(left[0]);
				left.RemoveAt(0);
			}
			//When the right is the only one with a count
			else if (right.Count > 0)
			{
				result.Add(right[0]);
				right.RemoveAt(0);
			}
			//The while condition loop allows for there to be no need for an else
		}
		//This should alter the original array as it is passed in via reference
		return result;
	}
}
//Timer function used to record how long it takes each algorithm to complete its search or sort
public class SearchSortTimer
{
	static float start;
	public static void BeginTimer()
	{
		start = Time.realtimeSinceStartup;
	}
	public static void EndTimer()
	{
		Debug.Log("This process took = " + (Time.realtimeSinceStartup - start));
	}
}
