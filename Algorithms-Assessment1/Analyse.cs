using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Algorithms_Assessment1
{
    /// <summary>
    /// Class containing methods used to analyse the results of the algorithms and outputs to the console.
    /// </summary>
    public static class Analyse
    {
        /// <summary>
        /// Loops through the chosen array and output each integer to the console.
        /// </summary>
        /// <param name="array">The array they would like to output.</param>
        public static void OutputArray(int[] array)
        {
            // Loops through the unordered array and outputs to console.
            for (int i = 0; i < array.Length - 1; i ++)
            {
                Console.Write(array[i] + " ");
            }
        }

        /// <summary>
        /// Outputs to the console every number of values the user has chosen.
        /// </summary>
        /// <param name="numValues">The amount of values the user has chosen to display.</param>
        private static void DisplayingValues(int numValues)
        {
            Console.WriteLine($"\nDisplaying every {numValues} value(s) in the array:");
        }
        
        /// <summary>
        /// Outputs to console every x number of values the user chooses in ascending order.
        /// </summary>
        /// <param name="sortedArray">A sorted array of integers.</param>
        /// <param name="numValues">Number of values the user chooses to display.</param>
        public static void DisplayAscendingValues(int[] sortedArray, int numValues)
        {
            // Output to console.
            DisplayingValues(numValues);
            
            // Loop through array and display every 10th value.
          
            for (int i = 0; i < sortedArray.Length - 1; i+= numValues)
            {
                Console.Write(sortedArray[i] + " ");
            }

            // Output number of steps taken to sort the array.
            DisplaySortingSteps();
        }

        /// <summary>
        /// Outputs to console every x number of values the user chooses in descending order.
        /// </summary>
        /// <param name="sortedArray">A sorted array of integers.</param>
        /// <param name="numValues">Number of values the user chooses to display.</param>
        public static void DisplayDescendingValues(int[] sortedArray, int numValues)
        {
            // Output to console.
            DisplayingValues(numValues);
            
            // Loop through array and display every 10th value.

            for (int i = sortedArray.Length - 1; i >= 0; i -= numValues)
            {
                Console.Write(sortedArray[i] + " ");
            }

            // Output number of steps taken to sort the array.
            DisplaySortingSteps();
        }
    
        /// <summary>
        /// Creates a list of integer arrays containing the original inputted arrays the user chose. Used so the
        /// unsorted arrays can be accessed again.
        /// </summary>
        /// <param name="arrays">The original inputted user arrays.</param>
        /// <returns>List of integer arrays containing the original arrays.</returns>
        public static List<int[]> OriginalArrays(List<int[]> arrays)
        {
            List<int[]> originalArrays = new List<int[]>();
            
            // Loop through list of arrays and add it as a new array to a originalArrays.
            foreach (int[] array in arrays)
            {
                originalArrays.Add(new List<int>(array).ToArray());
            }
        
            // Return the list of integer arrays.
            return originalArrays;
        }

        /// <summary>
        /// Outputs to the console the indices of either the user's chosen number or the closest number to that.
        /// </summary>
        /// <param name="indices">List of integers containing the indices of the found or closest number.</param>
        public static void OccurenceOfUserNumber(List<int> indices)
        {
            // Sorting the list into ascending order.
            int[] indicesArray = indices.ToArray();
            Algorithms.QuickSort(indicesArray);
            
            // Checks whether the user's number has been found or not.
            if (Algorithms.UserInputNum != Algorithms.NearestNum)
            {
                // Output to console the number of occurrences of the closest number and the indices.
                Console.WriteLine("Number not found.");
                Console.WriteLine($"Number {Algorithms.UserInputNum} does not exist in the array." +
                                  $" {Algorithms.NearestNum} is the closest value with {indices.Count} occurrence(s)." + 
                                  " Displaying the location(s) of the closest value instead:");
                
                foreach (int index in indicesArray)
                {
                    Console.Write($"{index}" + " ");
                }
            }
            else
            {
                // Output to console the number of occurrences of the user's number and the indices.
                Console.WriteLine("Number has been found.");
                Console.WriteLine($"There are {indices.Count} occurrences of {Algorithms.UserInputNum}. Located at" +
                                  $" indices: ");
                
                foreach (int index in indicesArray)
                {
                    Console.Write($"{index}" + " ");
                }
            }
            
            // Output the number of steps taken to found the numbers location(s).
            DisplaySearchingSteps();
        }
        
        /// <summary>
        /// Output to the console the amount of steps it takes to sort the chosen array.
        /// </summary>
        private static void DisplaySortingSteps()
        {
            Console.WriteLine($"\nNumber of swaps taken to sort the array: {Algorithms.StepCounter}.");
        }

        /// <summary>
        /// Output to the console the amount of steps it take to find the user's number location(s) or the closest
        /// number location(s)
        /// </summary>
        private static void DisplaySearchingSteps()
        {
            // Checks whether it's the user's number which has been found or the closest number instead.
            if (Algorithms.UserInputNum != Algorithms.NearestNum)
            {
                Console.WriteLine($"\nNumber of steps taken to find the location(s) of number" +
                                  $" {Algorithms.NearestNum}: {Algorithms.StepCounter}.");
            }

            else
            {
                Console.WriteLine($"\nNumber of steps taken to find the location(s) of number" +
                                  $" {Algorithms.UserInputNum}: {Algorithms.StepCounter}.");
            }
        }
    }
}