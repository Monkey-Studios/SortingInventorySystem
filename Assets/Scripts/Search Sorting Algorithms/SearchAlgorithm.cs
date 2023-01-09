using System;

namespace CS_Sort
{
	class SearchAlgorithm
	{
		public static int Linear(int[] inventory, int value)
		{
			for (int i = 0; i < inventory.Length; i++)
            {
				// steps through one at a time until result is found
				if (inventory[i] == value)
                {
					Console.WriteLine("\n Linear Search: " + value + " is at index " + i + "\n");
					//Console.WriteLine("\n Linear Search: {0} is at index {1} \n", value, i);
					return i;
                }
            }

			Console.WriteLine("\n Item not found \n");
			return -1;
		}

		public static int Jump(int[] inventory, int value)
		{
			int n = inventory.Length;
			// square root of inventory length
			int m = (int)Math.Sqrt(n);
			// sqrt is representative of how many tiles are jumped
			int nextBlock = m;
			int index = 0;

			// checks to make sure we dont go outside the bounds of the array
			// or if we go past a certain value
			while (nextBlock < n && inventory[nextBlock] < value)
            {
				index = nextBlock;
				nextBlock += m;
				// if we finish searching (indicated by index being larger than the array)
				// return nothing as it was not found
				if (index >= n)
                {
					Console.WriteLine("\n Item not found \n");
					return -1;
				}
            }

			// if nextBlock is larger than the current size
			// resets it back down to the size of the array
			nextBlock = Math.Min(nextBlock, n);

			// now just performs a linear search on the array 
			for (int i = index; i < nextBlock; i++)
            {
				if (inventory[i] == value)
                {
					Console.WriteLine("\n Jump Search: " + value + " is at index " + i + "\n");
					//Console.WriteLine("\n Jump Search: {0} is at index {1} \n", value, i);
					return i;
				}
            }
			Console.WriteLine("\n Item not found \n");
			return -1;
		}

		public static int Binary(int[] inventory, int value)
		{
			//define minimum and maximum bounds
			int min = 0;
			int max = inventory.Length - 1;

			while (min <= max)
            {
				// defines a middle value using provided bounds
				int mid = (min + max) / 2;
				// checks if item corresponds to middle value
				if (inventory[mid] == value)
                {
					// if it does, return item
					Console.WriteLine("\n Binary Search: " + value + " is at index " + mid + "\n");
					//Console.WriteLine("\n Binary Search: {0} is at index {1} \n", value, mid);
					return mid;
				}
				// if item is smaller in value than our current one
				else if (inventory[mid] > value)
				{
					// discard the bottom half of the array
					// search again
					max = mid - 1;
                }
                else
                {
					// discard the top half of the array
					// loop back around
					min = mid + 1;
                }
            }

			// if not found, exit out
			Console.WriteLine("\n Item not found \n");
			return -1;
		}
	}
}
