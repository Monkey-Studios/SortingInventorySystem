using System;
using System.Collections.Generic;
using System.Text;

namespace CS_Sort
{
	class SortAlgorithm
	{
		public static void Injection(ref int[] array)
		{
			int n = array.Length;

			// creates new array with the length of the passed in array
			int[] newArray = new int[n];

			// loops through and assigns each value a position in the new array equal to its value
			for (int i = 0; i < n; i++)
			{
				newArray[array[i]] = array[i];
			}

			// overwrites the old array with the sorted one
			array = newArray;
		}

		public static void Histogram(ref int[] array)
		{
			int n = array.Length;

			// finds maxmium value in the array passed in
			int maxValue = 0;
			for (int i = 0; i < n; i++)
			{
				if (array[i] > maxValue)
				{
					maxValue = array[i];
				}
			}
			maxValue++;

			// initialise histogram
			List<int>[] histogram = new List<int>[maxValue];
			for (int i = 0; i < maxValue; i++)
			{
				histogram[i] = new List<int>();
			}

			// adding array elements into the histogram
			for (int i = 0; i < n; i++)
			{
				histogram[array[i]].Add(array[i]);
			}

			// populate new array with sorted values
			List<int> newArrayAsList = new List<int>();
			for (int i = 0; i < maxValue; i++)
			{
				newArrayAsList.AddRange(histogram[i]);
			}

			//converts the list made above into a static array
			array = newArrayAsList.ToArray();
		}

		public static void Bubble(ref int[] array)
		{
			int n = array.Length;

			// checks whether has been swapped
			// set to true to start in order to begin search
			bool swapped = true;

			while (swapped)
			{
				swapped = false;
				for (int i = 0; i < n - 1; i++)
				{
					// checks next item in array
					if (array[i] > array[i + 1])
					{
						// if needs sorting
						// swaps via a temporary variable
						int temp = array[i];
						array[i] = array[i + 1];
						array[i + 1] = temp;

						swapped = true;
					}
				}
			}
		}

		public static void Shaker(ref int[] array)
		{
			int n = array.Length;

			// searches through half of the array
			for (int i = 0; i < n/2; i++)
            {
				bool swapped = false;

				// left to right
				for (int j = i; j < n - i - 1; j++)
                {
					// checks the index above the current one
					if (array[j] > array[j + 1])
                    {
						int temp = array[j];
						array[j] = array[j + 1];
						array[j + 1] = temp;

						swapped = true;
                    }
                }

				// right to left
				for (int k = n - i - 2; k > i; k--)
                {
					// checks the index under the current one
					if (array[k - 1] > array[k])
                    {
						int temp = array[k - 1];
						array[k - 1] = array[k];
						array[k] = temp;

						swapped = true;
                    }
                }

				// if swapped is still false, array is complete
				if (swapped == false)
                {
					break;
                }
            }

		}

		public static void Selection(ref int[] array)
		{
			int n = array.Length;

			for (int i = 0; i < n - 1; i++)
			{
				// "minimum index" = the lowest point in array
				// an anchor to the bottom
				int mindex = i;

				// sets j to starting value of i+1
				// second f!loop which checks if it is smaller
				for (int j = i + 1; j < n; j++)
				{
					// if j value is smaller than smallest value in array
					if (array[j] < array[mindex])
					{
						// sets a new "minimum index"
						mindex = j;
					}
				}
				// checks if mindex has changed
				if (mindex != i)
				{
					// swaps the value of i with the mindex
					// because it is no longer the smallest
					int temp = array[i];
					array[i] = array[mindex];
					array[mindex] = temp;
				}
			}
		}

		public static void Insertion(ref int[] array)
		{
			int n = array.Length;

			// initial loop
			for (int i = 0; i < n - 1; i++)
			{
				// second loop, starting from next value up
				// goes backwards through the loop so far
				for (int j = i + 1; j > 0; j--)
				{
					if (array[j - 1] > array[j])
					{
						// if needs sorting
						// swaps via a temporary variable
						int temp = array[j - 1];
						array[j - 1] = array[j];
						array[j] = temp;
					}
				}
			}
		}

		public static void Merge(ref int[] array)
		{
			// checks to see if array can be sorted
			if (array.Length <= 1)
			{
				return;
			}

			// split into two separate lists
			int middle = array.Length / 2;

			// creates two new arrays to be appended to
			// sets their lengths at half the length of the original
			int[] leftArr = new int[middle];
			int[] rightArr = new int[middle];

			// creates a new list from the data on the left hand side
			// uses "middle" var to stop half way through
			for (int i = 0; i < middle; i++)
			{
				leftArr[i] = array[i];
			}
			// creates a new list from the data on the right hand side
			// uses "middle" var to stop half way through
			for (int i = middle; i < array.Length; i++)
			{
				rightArr[i] = array[i];
			}

			// recursively calls the function again
			// this allows it to run the above section again until each array is only 1 value long
			// at which point it will return and begin to re merge all the points
			Merge(ref leftArr);
			Merge(ref rightArr);

			// converts arrays into lists for later use
			List<int> _left = new List<int>(leftArr);
			List<int> _right = new List<int>(rightArr);

			// re merge lists into one array
			// new list to store results
			List<int> result = new List<int>();

			// while these lists still have length to them, cycles through
			while (_left.Count > 0 || _right.Count > 0)
			{
				// if either list has length, does a comparison
				if (_left.Count > 0 && _right.Count > 0)
				{
					if (_left[0] <= _right[0])
					{
						// if left is greater
						result.Add(_left[0]);
						_left.Remove(0);
					}
					else
					{
						// if right is greater
						result.Add(_right[0]);
						_right.Remove(0);
					}
				}
				// if only left has length
				else if (_left.Count > 0)
				{
					result.Add(_left[0]);
					_left.Remove(0);
				}
				// if only right has length
				else if (_right.Count > 0)
				{
					result.Add(_right[0]);
					_right.Remove(0);
				}
				// no else as there should be no need thanks to the while condition
			}

			// alters the array to be sorted like the "result" list
			array = result.ToArray();
			// this should alter the original array as it is passed in via reference
		}


		public static void Quick(ref int[] array)
		{
			// alternative to Quick sort where the bounds dont have to be pased in
			// bounds are automatically assigned to lower and upper ends of the array
			Quick(ref array, 0, array.Length - 1);
		}

		private static void Quick(ref int[] array, int low, int high)
		{
			// checks if the lower bound of the array is actually of a lower value
			if (low < high)
			{
				// calls the "Partition" function which returns a value into the pivot integer
				int pivot = Partition(array, low, high);

				if (pivot > 1)
				{
					// pivot passed in as higher bound to sort the left hand side
					Quick(ref array, low, pivot - 1);
				}
				if (pivot + 1 < high)
				{
					// pivot passed in as lower bound to sort the right hand side
					Quick(ref array, pivot + 1, high);
				}
			}
		}

		private static int Partition(int[] array, int low, int high)
		{
			// select a pivot point as the high bound
			int pivot = array[high];

			// creates a reference to the underneath of the low bound of the array
			int lowIndex = (low - 1);

			// sorts the array based on values lower than the pivot
			for (int i = low; i < high; i++)
			{
				// only sorts if current point is smaller than the pivot 
				if (array[i] <= pivot)
				{
					lowIndex++;

					int temp = array[lowIndex];
					array[lowIndex] = array[i];
					array[i] = temp;
				}
			}

			// puts the current pivot into its correct position in the array 
			int _temp = array[lowIndex + 1];
			array[lowIndex + 1] = array[high];
			array[high] = _temp;

			// returns the value ended up with to determine what to loop through next
			return lowIndex + 1;
		}
	
	}
}