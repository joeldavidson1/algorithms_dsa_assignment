using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace Algorithms_Assessment1
{
    /// <summary>
    /// Class containing various sorting and searching algorithms.
    /// </summary>
    public static class Algorithms
    {
        /// <summary>
        /// Keeps track of the number of steps each algorithm takes.
        /// </summary>
        public static int StepCounter { get; private set; }
        
        /// <summary>
        /// Keeps track of the nearest number during searching.
        /// </summary>
        public static int NearestNum { get; private set; }
        
        /// <summary>
        /// Keeps track of the user's chosen input number during searching.
        /// </summary>
        public static int UserInputNum { get; private set; }

        /// <summary>
        /// Resets the StepCounter to 0.
        /// </summary>
        public static void ResetStepCounter()
        {
            StepCounter = 0;
        }

        /// <summary>
        /// Uses the Insertion sort algorithm to sort an array of integers.
        /// </summary>
        /// <param name="array">An unsorted array of integers.</param>
        public static void InsertionSort(int[] array)
        {
            // Loop through the array starting at second element as the first element is already sorted itself.
            for (int i = 1; i < array.Length; i++)
            {
                int currentNum = array[i];
                
                // Move left in the array until j reaches 0 and the value is greater than the current value variable
                int j = i - 1;
                while (j >= 0 && array[j] > currentNum)
                {
                    array[j + 1] = array[j];
                    j--;
                    StepCounter++;
                }
                
                // Save the value at array[j + 1] to the temporary currentNum.
                array[j + 1] = currentNum;
            }
        }
        
        
        // Overload method to allow for cleaner use (int[] array only]).
        /// <summary>
        /// Overload method to allow for cleaner use elsewhere as it only uses the int[] parameter.
        /// </summary>
        /// <param name="array">An unsorted array of integers.</param>
        public static void QuickSort(int[] array)
        {
            QuickSort(array, 0, array.Length - 1);
        }
        
        /// <summary>
        /// Uses the Quicksort algorithm to sort an array of integers.
        /// </summary>
        /// <param name="array">An unsorted array of integers.</param>
        /// <param name="leftIndex">The starting index.</param>
        /// <param name="rightIndex">The end index.</param>
        private static void QuickSort(int[] array, int leftIndex, int rightIndex)
        {   
            // If array only contains 1 element then it is already sorted.
            if (leftIndex >= rightIndex)
            {
                return;
            }
            
            // Choose random element between left and right index as a pivot point to achieve better
            // efficiency in the average case.
            int randomPivot = new Random().Next(rightIndex - leftIndex) + leftIndex;
            int pivot = array[randomPivot];
            
            // Swap random pivot to the right index.
            Swap(array, randomPivot, rightIndex);
            StepCounter++;

            int leftPointer = Partition(array, leftIndex, rightIndex, pivot);

            // Recursive call to left and right of pivot.
            QuickSort(array, leftIndex, leftPointer - 1);
            QuickSort(array, leftPointer + 1, rightIndex);
        }

        /// <summary>
        /// Partitions elements to the left and right of the pivot element into two sub-arrays. Used during the
        /// Quicksort algorithm.
        /// </summary>
        /// <param name="array">An unsorted array of integers.</param>
        /// <param name="leftIndex"> The starting index.</param>
        /// <param name="rightIndex">The end index.</param>
        /// <param name="pivot">The element used to pivot from.</param>
        /// <returns></returns>
        private static int Partition(int[] array, int leftIndex, int rightIndex, int pivot)
        {
            // Creating pointers to use for partitioning process.
            int leftPointer = leftIndex;
            int rightPointer = rightIndex;
            
            // Loop to move leftPointer and rightPointer together.
            while (leftPointer < rightPointer)
            {
                // From left to right find a number higher than the pivot or it's found the rightPointer.
                while (array[leftPointer] <= pivot && leftPointer < rightPointer)
                {
                    leftPointer++;
                }
                
                // From right to left find a number lower than the pivot or it's found the left pointer.
                while (array[rightPointer] >= pivot && leftPointer < rightPointer)
                {
                    rightPointer--;
                }
                
                // Swap elements at the left and right pointers.
                Swap(array, leftPointer, rightPointer);
                StepCounter++;
            }
            
            // Both pointers have met and checks the last value in case it is not in the correct order.
            if (array[leftPointer] > array[rightIndex])
            {
                Swap(array, leftPointer, rightIndex);
                StepCounter++;
            }
            else
            {
                leftPointer = rightIndex;
                StepCounter++;
            }

            return leftPointer;
        }
        
        /// <summary>
        /// Simple swap algorithm used to swap positions of two indexes.
        /// </summary>
        /// <param name="array">An array of integers.</param>
        /// <param name="firstIndex">The first index.</param>
        /// <param name="secondIndex">The second index.</param>
        private static void Swap(int[] array, int firstIndex, int secondIndex)
        {
            // Swaps the first and second index positions.
            (array[firstIndex], array[secondIndex]) = (array[secondIndex], array[firstIndex]);
        }
        
        /// <summary>
        /// Uses the Merge sort algorithm to sort an array of integers.
        /// </summary>
        /// <param name="array">An unsorted array of integers.</param>
        public static void MergeSort(int[] array)
        {
            int arrayLength = array.Length;
            
            // If array contains only 1 element then it is already sorted.
            if (arrayLength < 2)
            {
                return;
            }
            
            // Get the two left and right temporary subarrays.
            int middleIndex = arrayLength / 2;
            int[] leftArray = new int[middleIndex];
            int[] rightArray = new int[arrayLength - middleIndex];
            
            // Copy data to the left subarray from main array.
            for (int i = 0; i < middleIndex; i++)
            {
                leftArray[i] = array[i];
            }
            
            // Copy data to the right subarray from main array.
            for (int i = middleIndex; i < arrayLength; i++)
            {
                rightArray[i - middleIndex] = array[i];
            }
            
            // Merge the two arrays and recursively call until sorted.
            MergeSort(leftArray);
            MergeSort(rightArray);
            Merge(array, leftArray, rightArray);
        }
    
        /// <summary>
        /// Algorithm used to merge the sub-arrays during the Merge sort algorithm.
        /// </summary>
        /// <param name="array">An array of integers.</param>
        /// <param name="leftArray">Left subarray.</param>
        /// <param name="rightArray">Right subarray.</param>
        private static void Merge(int[] array, int[] leftArray, int[] rightArray)
        {
            int leftArrayLength = leftArray.Length;
            int rightArrayLength = rightArray.Length;
            
            // Create iterators for each array. i = leftArray, j = rightArray, k = merged array.
            int i = 0, j = 0, k = 0;
            
            // Loop until it runs out of elements in either array.
            while (i < leftArrayLength && j < rightArrayLength)
            {
                // Add element at leftArray[i] to the merged array as it's smallest number.
                if (leftArray[i] <= rightArray[j])
                {
                    array[k] = leftArray[i];
                    i++;
                    StepCounter++;
                }
                // Add element at the rightArray to the merged array if it's smaller than leftArray[i].
                else
                {
                    array[k] = rightArray[j];
                    j++;
                    StepCounter++;
                }
                k++;
            }
            
            // Capture possible elements that could be left in either leftArray or rightArray.
            // Check left array.
            while (i < leftArrayLength)
            {
                array[k] = leftArray[i];
                i++;
                k++;
                StepCounter++;
            }
            
            // Check right array.
            while (j < rightArrayLength)
            {
                array[k] = rightArray[j];
                j++;
                k++;
                StepCounter++;
            }
        }

       /// <summary>
       /// Uses the Heapsort algorithm to sort an array of integers.
       /// </summary>
       /// <param name="array">An unsorted array of integers.</param>
        public static void HeapSort(int[] array)
        {
            // Length variable, last parent node in heap and the last child in the heap.
            int length = array.Length;
            
            // Restructure the heap and decrement through, swapping elements where needed.
            for (int i = length / 2; i >= 0; i--)
            {
                BuildMaxHeap(array, length - 1, i);
            }
            for (int i = length - 1; i >= 0; i--)
            {
                // Swap elements.
                (array[i], array[0]) = (array[0], array[i]);
                StepCounter++;
                
                // Call BuildMaxHeap again but excluded already sorted last element.
                BuildMaxHeap(array, i - 1, 0);
            }
        }
        
       /// <summary>
       /// Builds a heap data structure from a binary tree. Used during the Heapsort algorithm.
       /// </summary>
       /// <param name="array"> An array of integers.</param>
       /// <param name="length">Length of the heap.</param>
       /// <param name="i">The node.</param>
        private static void BuildMaxHeap(int[] array, int length, int i)
        {
            // max = parent node, left and right are child nodes.
            int max = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;
            
            // Check if left and right child node exist, it's greater than the root and index is less than heap size.
            if (left <= length && array[left] > array[max])
            {
                max = left;
            }

            if (right <= length && array[right] > array[max])
            {
                max = right;
            }
            
            // Check to see if elements were swapped, if so then we swap elements in the array.
            if (max != i)
            {
                // Swap elements.
                (array[i], array[max]) = (array[max], array[i]);
                StepCounter++;

                // Recursively call BuildHeap and check the lower elements.
                BuildMaxHeap(array, length, max);
            }
        }

       /// <summary>
       /// Uses the Binary search algorithm to locate the user's chosen number.
       /// </summary>
       /// <param name="array">A sorted array of integers.</param>
       /// <param name="userNum">The number the user wants to find in the array.</param>
       /// <returns>The integer if found, -1 if not.</returns>
        private static int BinarySearch(int[] array, int userNum)
        {
            // Create a list for the number of occurrences that may appear from the user number.
            List<int> occurrencesOfUserNum = new List<int>();

            int leftIndex = 0;   // Left side of array.
            int rightIndex = array.Length - 1;   // Right side of array.

            while (leftIndex <= rightIndex)
            {
                StepCounter++;
                
                // Obtain the middle index.
                int middleIndex = (leftIndex + rightIndex) / 2;
                
                // If the user number is equal to the middle index then return that.
                if (array[middleIndex] == userNum)
                {
                    return middleIndex;
                }
                
                // If the user number is less than the middle index then we look at left hand side of the array.
                if (userNum < array[middleIndex])
                {
                    rightIndex = middleIndex - 1;
                }
                // If the user number is greater than the middle index then we look at the right hand side of the array.
                else
                {
                    leftIndex = middleIndex + 1;
                }
            }
            
            // If number is not found return.
            return -1;
        }
        
       
       /// <summary>
       /// Uses the Binary search algorithm, except it returns the closest number in the array to the user's chosen
       /// number if it was not found. Either to the right or left depending which is closest.
       /// </summary>
       /// <param name="array">A sorted array of integers.</param>
       /// <param name="userNum">The number the user wants to find in the array.</param>
       /// <returns>The closest integer to the user's number.</returns>
        private static int BinarySearchClosest(int[] array, int userNum)
        {
            int leftIndex = 0;
            int rightIndex = array.Length - 1;   
            int middleIndex = 0;

            while (leftIndex <= rightIndex)
            {
                StepCounter++;
                middleIndex = (leftIndex + rightIndex) / 2;
                
                // If the user number is equal to the middle index then return that.
                if (array[middleIndex] == userNum)
                {
                    return middleIndex;
                }
                
                // If the user number is less than the middle index then we look at left hand side of the array.
                if (userNum < array[middleIndex])
                {
                    rightIndex = middleIndex - 1;
                }
                // If the user number is greater than the middle index then we look at the right hand side of the array.
                else
                {
                    leftIndex = middleIndex + 1;
                }
            }
            
            // If the user enters a number bigger than the greatest number then it returns the last number in array.
            if (userNum > array.Last())
            {
                return array.GetUpperBound(0);
            }
            
            // Returns smallest number in the array if user enters a number smaller than that.
            if (userNum < array[0])
            {
                return array.GetLowerBound(0);
            }

            // Finds the closest number in the array to the user number by comparing the numbers above and below.
            if (array[middleIndex + 1] - userNum < userNum - array[middleIndex])
            {
               return middleIndex + 1;
            }
            
            return middleIndex; 
        }
        
       /// <summary>
       /// Uses the Interpolation search algorithm to locate the user's chosen number.
       /// </summary>
       /// <param name="array">A sorted array of integers.</param>
       /// <param name="userNum">The number the user wants to find in the array.</param>
       /// <returns>The integer if found, -1 if not.</returns>
        private static int InterpolationSearch(int[] array, int userNum)
        {
            int leftIndex = 0;
            int rightIndex = array.Length - 1;
            int middleIndex = 0;

            while ((array[rightIndex] != array[leftIndex]) && (userNum >= array[leftIndex]) && 
                   (userNum <= array[rightIndex]))
            {
                StepCounter++;
                
                // Formula for approximating where userNum is located.
                middleIndex = leftIndex + (((rightIndex - leftIndex) / (array[rightIndex] - array[leftIndex])) * 
                                           (userNum - array[leftIndex]));
                
                // If the user number is equal to the middle index then return that.
                if (array[middleIndex] == userNum)
                {
                    return middleIndex;
                }
                
                // If the user number is less than the middle index then we look at left hand side of the array.
                if (userNum < array[middleIndex])
                {
                    rightIndex = middleIndex - 1;
                }
                // If the user number is greater than the middle index then we look at the right hand side of the array.
                else
                {
                    leftIndex = middleIndex + 1;
                }
            }
            
            return -1;
        }
        
       /// <summary>
       /// Uses the Interpolation search algorithm, except it returns the closest number in the array to the user's
       /// chosen number if it was not found. Either to the right or left depending which is closest.
       /// </summary>
       /// <param name="array">A sorted array of integers.</param>
       /// <param name="userNum">The number the user wants to find in the array.</param>
       /// <returns>The closest integer to the user's number.</returns>
        private static int InterpolationSearchClosest(int[] array, int userNum)
        {
            int leftIndex = 0;
            int rightIndex = array.Length - 1;
            int middleIndex = 0;

            while ((array[rightIndex] != array[leftIndex]) && (userNum >= array[leftIndex]) && 
                   (userNum <= array[rightIndex]))
            {
                StepCounter++;
                
                // Formula for approximating where userNum is located.
                middleIndex = leftIndex + ((rightIndex - leftIndex) / (array[rightIndex] - array[leftIndex])) * 
                                           (userNum - array[leftIndex]);
                
                // If the user number is equal to the middle index then return that.
                if (array[middleIndex] == userNum)
                {
                    return middleIndex;
                }
                
                // If the user number is less than the middle index then we look at left hand side of the array.
                if (userNum < array[middleIndex])
                {
                    rightIndex = middleIndex - 1;
                }
                // If the user number is greater than the middle index then we look at the right hand side of the array.
                else
                {
                    leftIndex = middleIndex + 1;
                }
            }
            
            
            // If the user enters a number bigger than the greatest number then it returns the last number in array.
            if (userNum > array.Last())
            {
                return array.GetUpperBound(0);
            }
            
            // Returns smallest number in the array if user enters a number smaller than that.
            if (userNum < array[0])
            {
                return array.GetLowerBound(0);
            }

            // Finds the closest number in the array to the user number by comparing the numbers above and below.
            if (array[middleIndex + 1] - userNum < userNum - array[middleIndex])
            {
                return middleIndex + 1; // value above the user num
            }
            
            return middleIndex; // value below user num
        }

       /// <summary>
       /// Uses either the Binary or Interpolation search algorithms to find: the location(s) of the user's number,
       /// or the location(s) of the number that is closest to the user's number.
       /// </summary>
       /// <param name="array">A sorted array of integers.</param>
       /// <param name="userNum">The number the user wants to find in the array.</param>
       /// <param name="isBinary">A boolean used to check whether to use Binary or Interpolation search.</param>
       /// <returns>List of integers containing the location(s) of the user's number or the closest number.</returns>
        public static List<int> UserNumOccurrences(int[] array, int userNum, bool isBinary)
        {
            // Create list to hold the indices of the found locations of either user number or the closest value.
            List<int> numOfOccurrences = new List<int>();
            
            // Check to see if BinarySearch has been used.
            if (isBinary)
            {
                Console.WriteLine("\nBinary Search Results:");
                
                // Set current index to result of the BinarySearch.
                int index = BinarySearch(array, userNum);
                UserInputNum = userNum;
                
                // Check if the userNum was found.
                if (index != -1)
                {
                    // Add the userNum to the list.
                    NearestNum = UserInputNum;
                    numOfOccurrences.Add(index);
                    
                    // Check to left of number.
                    int leftNum = index - 1;
                    while (leftNum >= 0 && array[leftNum] == userNum)
                    {
                        numOfOccurrences.Add(leftNum);
                        leftNum--;
                        StepCounter++;
                    }
                    
                    // Check to right of number.
                    int rightNum = index + 1;
                    while (rightNum < array.Length - 1 && array[rightNum] == userNum)
                    {
                        numOfOccurrences.Add(rightNum);
                        rightNum++;
                        StepCounter++;
                    }
                }
                
                // Number hasn't been found.
                else
                {
                    // Set current index to result of the BinarySearchClosest.
                    index = BinarySearchClosest(array, userNum);
                    
                    // Set NearestNum to the result BinarySearchClosest
                    NearestNum = array[index];
                    
                    // Captures an edge where an extra index is duplicated.
                    if (NearestNum != array.Last())
                    {
                        numOfOccurrences.Add(index);
                    }
                    
                    // Check to left of number.
                    int leftNum = index - 1;
                    while (leftNum >= 0 && array[leftNum] == NearestNum)
                    {
                        numOfOccurrences.Add(leftNum);
                        leftNum--;
                        StepCounter++;
                    }
                    
                    // Check to right of number.
                    int rightNum = index + 1;
                    while (rightNum < array.Length - 1 && array[rightNum] == NearestNum)
                    {
                        numOfOccurrences.Add(rightNum);
                        rightNum++;
                        StepCounter++;
                    }
                }
            }
            
            // Check to see if InterpolationSearch was used.
            else
            {
                // Set index to the result of the InterpolationSearch.
                int index = InterpolationSearch(array, userNum);
                UserInputNum = userNum;
                
                Console.WriteLine("\nInterpolation Search Results:");
                
                // Check if the userNum was found.
                if (index != -1)
                {
                    
                    // Add the userNum to the list.
                    NearestNum = UserInputNum;
                    numOfOccurrences.Add(index);

                    // Check to left of number.
                    int leftNum = index - 1;
                    while (leftNum >= 0 && array[leftNum] == userNum)
                    {
                        numOfOccurrences.Add(leftNum);
                        leftNum--;
                        StepCounter++;
                    }
                    
                    // Check to the right of number.
                    int rightNum = index + 1;
                    while (rightNum < array.Length - 1 && array[rightNum] == userNum)
                    {
                        numOfOccurrences.Add(rightNum);
                        rightNum++;
                        StepCounter++;
                    }
                }

                // Number hasn't been found.
                else
                {
                    // Set current index to the result of InterpolationSearchClosest.
                    index = InterpolationSearchClosest(array, userNum);
                    
                    // Set NearestNum to result InterpolationSearchClosest.
                    NearestNum = array[index];
                    
                    // Captures the edge cases where an extra index is duplicated.
                    if (NearestNum != array.Last())
                    {
                        numOfOccurrences.Add(index);
                    }
                    
                    // Check left of number.
                    int leftNum = index - 1;
                    while (leftNum >= 0 && array[leftNum] == NearestNum)
                    {
                        numOfOccurrences.Add(leftNum);
                        leftNum--;
                        StepCounter++;
                    }
                    
                    // Check right of number.
                    int rightNum = index + 1;
                    while (rightNum < array.Length - 1 && array[rightNum] == NearestNum)
                    {
                        numOfOccurrences.Add(rightNum);
                        rightNum++;
                        StepCounter++;
                    }
                }
            }
            
            // Return the list.
            return numOfOccurrences;
        }

       /// <summary>
       /// Sorts each integer array in a list of arrays.
       /// </summary>
       /// <param name="listOfArrays">List of integer arrays containing unsorted arrays.</param>
        public static void SortArrays(List<int[]> listOfArrays)
        {
            // Loop through each array sorting it using Quicksort.
            foreach (int[] array in listOfArrays)
            {
                QuickSort(array); 
            }
        }
    }
}