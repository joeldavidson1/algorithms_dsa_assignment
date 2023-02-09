using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;

namespace Algorithms_Assessment1
{
    /// <summary>
    /// Class containing various methods used to prompt the user and obtain input from the console.
    /// </summary>
    public static class Input
    { 
        /// <summary>
        /// Keeps track of the number of files the user has entered.
        /// </summary>
        private static int FileCounter { get; set; }
        
        /// <summary>
        /// Keeps a list of integer arrays from the user inputted arrays.
        /// </summary>
        private static List<int[]> UserArrays { get; set; }
        
        /// <summary>
        /// Holds the two merged arrays of 256 length into one merged array.
        /// </summary>
        private static int[] Merged256Array { get; set; }
        
        /// <summary>
        /// Holds the two merged arrays of 2048 length into one merged array.
        /// </summary>
        private static int[] Merged2048Array { get; set; }
        
        /// <summary>
        /// Resets the file counter back to 0 to keep track of amount of files entered each time.
        /// </summary>
        private static void ResetFileCounter()
        {
            FileCounter = 0;
        }
        
        /// <summary>
        /// Prompts user to enter a valid file path so they can add it to an array. Method also checks whether the
        /// file path exists and then converts the contents to an array of integers.
        /// </summary>
        /// <returns>An array of integers.</returns>
        private static int[] FileDataToArray()
        {
            Console.WriteLine($"Enter valid file path {FileCounter + 1}: ");
            string filePath = Console.ReadLine();
            
            // Check whether the file path exists or is accessible.
            if (!File.Exists(filePath))
            {
                // If not output to user and prompt user again.
                Console.WriteLine("Invalid file path.");
                return FileDataToArray();
            }
            
            // Convert the contents of the file into an array of integers.
            int[] array = File.ReadAllLines(filePath).Select(t => Convert.ToInt32(t)).ToArray(); 
            
            FileCounter++;
            
            // Return the final converted array.
            return array;
        }
        
        // Returns number of user options
        /// <summary>
        /// Outputs to user to enter 6 files containing only integers and calls the CreateArrays method to create the
        /// list of integer arrays.
        /// </summary>
        /// <returns>List of integer arrays.</returns>
        private static List<int[]> ListOfArrays()
        {
            Console.WriteLine("Enter all 6 files to be analysed. The files must contain integers only.");
            
            // Call the create arrays method.
            return CreateArrays(6);
        }
 
        /// <summary>
        /// Creates a list of integer arrays containing the arrays created from the user inputted file paths. Calls the
        /// FileDataToArray method to create the arrays from the file paths.
        /// </summary>
        /// <param name="files">The number of files the user entered.</param>
        /// <returns>List of integer arrays.</returns>
        private static List<int[]> CreateArrays(int files)
        {
            // List of arrays to save each one so the user can choose.
            List<int[]> userData = new List<int[]>();
            
            // Loops through the number of files and adds each created integer array to the list.
            for (int i = 1; i <= files; i++)
            {
                userData.Add(FileDataToArray());
            }

            // Returns the list of integer arrays.
            return userData;
        }

        /// <summary>
        /// Prompts user to pick which array in the list of integer arrays they would like to sort.
        /// </summary>
        /// <param name="arrayNumber">The number of arrays present in the list of arrays.</param>
        /// <returns>Number of arrays in the list of integer arrays.</returns>
        private static int SortArrayOption(int arrayNumber)
        {
            int option;
            
            // Error handling loop which outputs to the user to enter an integer between 1 and the arrayNumber.
            do
            {
                Console.WriteLine("\nWhich array would you like to sort? Please choose in the order you entered.");
                if (Int32.TryParse(Console.ReadLine(), out option) && option > 0 && option <= arrayNumber)
                {
                    break;
                }
                
                Console.WriteLine($"Please enter a valid option from 1-{arrayNumber}.");
                
            } while (true);
            
            // Return the array number they have chosen.
            return option;
        }

        /// <summary>
        /// Prompts the user to choose how many every (x) values in the array to display to the console.
        /// </summary>
        /// <param name="length">The length of the array.</param>
        /// <returns></returns>
        private static int DisplayXValues(int length)
        {
            int option;
            
            // Error handling loop which outputs to the user to enter an integer between 1 and the arrayNumber.
            do
            {
                Console.WriteLine("\nHow many every x value(s) would you like to be displayed?");
                if (Int32.TryParse(Console.ReadLine(), out option) && option > 0 && option <= length)
                {
                    break;
                }
                
                Console.WriteLine($"Please enter a valid option from 1-{length}.");
                
            } while (true);
            
            // Return the array number they have chosen.
            return option;
        }
        
        /// <summary>
        /// Prompts user to pick a sorting algorithm to use.
        /// </summary>
        /// <returns>The option they have chosen as an integer.</returns>
        private static int ChooseSortingAlgorithm()
        {
            int option;
            
            // Error handling loop which outputs to the user to enter an integer between 1-4.
            do 
            {
                Console.WriteLine("\nWhich algorithm would you like to use to sort the array?\n1) Quicksort" +
                                  "\n2) Insertion Sort\n3) Heapsort\n4) Merge Sort");
                if (Int32.TryParse(Console.ReadLine(), out option) && option > 0 && option < 5)
                {
                    break;
                }
                
                Console.WriteLine("Please enter a valid option from 1-4.");
                
            } while (true);
            
            // Return the option they have chosen.
            return option;
        }
        
        /// <summary>
        /// Prompts user to pick a searching algorithm to use.
        /// </summary>
        /// <returns>The option they have chosen as an integer.</returns>
        private static int ChooseSearchingAlgorithm()
        {
            int option;
            
            // Error handling loop which outputs to the user to enter either 1 or 2.
            do 
            {
                Console.WriteLine("\nWhich algorithm would you like to use to search a number for?\n1) Binary Search" +
                                  "\n2) Interpolation Search");
                if (Int32.TryParse(Console.ReadLine(), out option) && option > 0 && option < 3)
                {
                    break;
                }
                
                Console.WriteLine("Please enter either 1 or 2.");
                
            } while (true);
            
            // Return the option they have chosen.
            return option;
        }

        /// <summary>
        /// Prompts user to pick whether to display the array in ascending or descending order.
        /// </summary>
        /// <returns>The option they have chosen as an integer.</returns>
        private static int AscendingOrDescending()
        {
            int option;
            
            // Error handling loop which outputs to the user to enter either 1 or 2.
            do 
            {
                Console.WriteLine("\nWould you like to display the ordered array in: (1) Ascending" +
                                  " or (2) Descending order?");
                
                if (Int32.TryParse(Console.ReadLine(), out option) && option > 0 && option < 3)
                {
                    break;
                }
                
                Console.WriteLine("Please enter either 1 or 2.");
                
            } while (true);

            // Return the option they have chosen.
            return option;
        }

        /// <summary>
        /// Prompts user to display the original array for reference or not.
        /// </summary>
        /// <returns>A string of either "y" (yes), or "n" (no).</returns>
        private static string DisplayOriginalArray()
        {
            string yesOrNo;
            // Error handling loop which outputs to the user enter either "y" or "n".
            do 
            {
                Console.WriteLine("\nWould you like to see the original unordered array for reference? y/n.");
                string option = Console.ReadLine().ToLower();
                if (option == "y" || option == "n")
                {
                    yesOrNo = option;
                    break;
                }
                Console.WriteLine("Please enter either y/n.");

            } while (true);

            // Return either "y" or "n".
            return yesOrNo;
        }

        /// <summary>
        /// Prompts user whether they would like to continue sorting the arrays they have inputted.
        /// </summary>
        /// <returns>The option they have chosen as an integer.</returns>
        private static int ContinueSorting()
        {
            int option;
            
            // Error handling loop which outputs to the user to enter either 1 or 2.
            do
            {
                Console.WriteLine("\nWould you like to: \n1) Order another array of your choice of algorithm" +
                                  "\n2) Search for a number in the array of your choice");
                if (Int32.TryParse(Console.ReadLine(), out option) && option > 0 && option < 3)
                {
                    break;
                }

                Console.WriteLine("Please enter either 1 or 2.");

            } while (true);

            // Return the option they have chosen.
            return option;
        }
        
        /// <summary>
        /// Prompts user whether they would like to continue searching for values in the arrays.
        /// </summary>
        /// <returns>The option they have chosen as an integer.</returns>
        private static int ContinueSearching()
        {
            int option;
            
            // Error handling loop which outputs to the user to enter an integer between 1-4.
            do
            {
                Console.WriteLine("\nWould you like to: \n1) Search for another number with your chosen " +
                                  "algorithm and array \n2) Restart the program \n3) Merge arrays together \n4) Quit");
                if (Int32.TryParse(Console.ReadLine(), out option) && option > 0 && option < 5)
                {
                    break;
                }

                Console.WriteLine("Please enter an option from 1-4");

            } while (true);
            
            // Return the option they have chosen.
            return option;
        }
        
        /// <summary>
        /// Outputs to user that arrays have been sorted. Method also prompts user to choose an array to search for
        /// a number in.
        /// </summary>
        /// <param name="arrayNumber">Number of arrays in the list of integer arrays.</param>
        /// <returns>The option they have chosen as integer.</returns>
        private static int SearchArrayOption(int arrayNumber)
        {
            int option;
            
            // Error handling loop which outputs to the user to enter an integer between 1-4.
            do
            {
                Console.WriteLine("\nThe arrays have been ordered in order to use the following 2 searching" +
                                  " algorithms.");
                Console.WriteLine("Which array would you like to search for a number in? Please choose in the " +
                                  "order you entered.");
                if (Int32.TryParse(Console.ReadLine(), out option) && option > 0 && option <= arrayNumber)
                {
                    break;
                }
                
                Console.WriteLine($"Please enter a valid option from 1-{arrayNumber}.");
                
            } while (true);
            
            // Return the option they have chosen.
            return option;
        }
        
        /// <summary>
        /// Prompt user to enter a number they would like to locate in the array.
        /// </summary>
        /// <returns>The number the user entered.</returns>
        private static int SearchNumber()
        {
            int userNum;
            
            // Error handling loop which outputs to the user to enter an integer only.
            do
            {
                Console.WriteLine("\nWhich number would you like to find? (integer only).");
                if (Int32.TryParse(Console.ReadLine(), out userNum))
                {
                    break;
                }
                
                Console.WriteLine("Please enter an integer.");
                
            } while (true);
            
            // Returns the number they inputted.
            return userNum;
        }

        /// <summary>
        /// The first main method that starts the inputting and sorting process of the arrays. Calls various other
        /// methods to prompt the user, sort the arrays and outputs to the console.
        /// </summary>
        /// <param name="isMerged">Boolean to check whether the user has entered the merged files yet.</param>
        public static void StartSorting(bool isMerged)
        {
            ResetFileCounter();
            
            // Prompt user for file paths and add the contents to a list of arrays.
            if (!isMerged)
            {
                UserArrays = ListOfArrays();
            }
            
            // Merged arrays are then used as the 2 arrays instead of the 6 original arrays.
            else
            {
                UserArrays.Clear();
                UserArrays.Add(Merged256Array);
                UserArrays.Add(Merged2048Array);
                Console.WriteLine("\nThe 256 merged array is Array 1, and the 2048 merged array is Array 2.");
            }

            // Loop to check whether to continue sorting the arrays or not.
            int sortAction = 1;
            while (sortAction == 1)
            {
                // Makes a copy of the original list of arrays so they can be used again unordered.
                List<int[]> originalArrays = Analyse.OriginalArrays(UserArrays);

                Algorithms.ResetStepCounter();

                
                // Get from user the array they want to analyse and whether the merged files have been used yet.
                // -1 used due to indexing.
                int arrayNum = 0;
                if (!isMerged)
                {
                    arrayNum = SortArrayOption(6) - 1;
                }
                
                // The two merged arrays are then used instead.
                else
                {
                    arrayNum = SortArrayOption(2) - 1;
                }
                
                // Obtain the chosen user array.
                int[] userArray = UserArrays[arrayNum];
                
                // Obtain length of the array
                int length = userArray.Length;

                // Check if array is either of 256 or 2048 length. 512 is used to display every 10th value of the
                // merged array and 256 array lengths.
                bool is2048 = userArray.Length > 512;
                
                // Check whether to output the original array or not.
                if (DisplayOriginalArray() == "y")
                {
                    Analyse.OutputArray(userArray);
                }

                // Choose algorithm to sort the array. Using a switch statement to choose a case depending on the
                // user option.
                int algorithmSortOption = ChooseSortingAlgorithm();
                switch (algorithmSortOption)
                {
                    case 1:
                        Algorithms.QuickSort(userArray);
                        break;
                    case 2:
                        Algorithms.InsertionSort(userArray);
                        break;
                    case 3:
                        Algorithms.HeapSort(userArray);
                        break;
                    case 4:
                        Algorithms.MergeSort(userArray);
                        break;
                }

                // Prompt user for how many every (x) values they'd like to view.
                int numValues = DisplayXValues(length);
                
                // Get either descending or ascending order. Using a switch to choose a case depending on the user
                // option.
                int displayOption = AscendingOrDescending();
                switch (displayOption)
                {
                    case 1:
                        Analyse.DisplayAscendingValues(userArray, numValues);
                        break;
                    case 2:
                        Analyse.DisplayDescendingValues(userArray, numValues);
                        break;
                }
                
                // Prompt user to continue sorting their chosen array(s) or not. Using a switch statement to choose
                // case depending on the user option.
                int continueSorting = ContinueSorting();
                switch (continueSorting)
                {
                    case 1:
                        sortAction = 1;
                        // Reset the user arrays back to the original unsorted inputted arrays.
                        UserArrays = originalArrays;
                        break;
                    case 2:
                        // Exit out of the loop and into the next method.
                        sortAction = 2;
                        break;
                }
            }
            
            // Sort every array in the list of arrays ready for searching
            Algorithms.SortArrays(UserArrays);
        }

        /// <summary>
        /// The second main method that starts the searching process of the arrays. Calls various other methods to
        /// prompt the user, search for a number and outputs to the console.
        /// </summary>
        /// <param name="isMerged">Boolean to check whether the user has entered the merged files yet.</param>
        public static void StartSearching(bool isMerged)
        {
            int searchAction = 1;
            while (searchAction == 1)
            {
                Algorithms.ResetStepCounter();
            
                // Get from user the array they want to analyse and whether the merged files have been used yet.
                // -1 used due to indexing.
                int arrayNum = 0;
                if (!isMerged)
                {
                    arrayNum = SearchArrayOption(6) - 1;
                }

                // The two merged arrays are then used instead.
                else
                {
                    arrayNum = SearchArrayOption(2) - 1;
                }
                
                // Obtain the user chosen array.
                int[] userArray = UserArrays[arrayNum];
            
                // Prompt user for a number to search for.
                int userNumber = SearchNumber();
                
                // Prompt user for their chosen search algorithm to use.
                int algorithmSearchOption = ChooseSearchingAlgorithm();
                
                // Output ordered array so the user c
                Console.WriteLine("\nThe ordered array is now being displayed for reference.");
                Analyse.OutputArray(userArray);
                
                // Get back the list of occurrences that may appear from the user number in the array.
                List<int> occurrences = new List<int>();
            
                // Using a switch statement to choose a case depending on the user's chosen search algorithm.
                switch (algorithmSearchOption)
                {
                    case 1 :
                        occurrences = Algorithms.UserNumOccurrences(userArray, userNumber, true);
                        break;
                    case 2:
                        occurrences = Algorithms.UserNumOccurrences(userArray, userNumber, false);
                        break;
                }
                
                // Display the results of the searching algorithm to the user.
                Analyse.OccurenceOfUserNumber(occurrences);
            
                
                // Prompt user to see if they want to continue searching or not. Using a switch statement to choose a
                // case depending on the user's chosen option.
                int continueSearching = ContinueSearching();
                switch (continueSearching)
                {
                    case 1:
                        // Continue searching for a number in their chosen array.
                        searchAction = 1;
                        break;
                    case 2:
                        // Call start sorting again to restart the program.
                        StartSorting(false);
                        break;
                    case 3:
                        // Merge the chosen arrays, start sorting but changed isMerged to true.
                        MergeArrays();
                        isMerged = true;
                        StartSorting(true);
                        break;
                    case 4:
                        // Terminate program.
                        return;
                }
            }
        }

        private static void MergeArrays()
        {
            Console.WriteLine("\nPlease enter the file path of the two 256 length text files to be merged: ");
            
            ResetFileCounter();
            List<int[]> arrays256 = CreateArrays(2);

            // Merge the two arrays.
            Merged256Array = arrays256[0].Concat(arrays256[1]).ToArray();
            
            Console.WriteLine("\nPlease enter the file path of the two 2048 length text files to be merged: ");
            
            ResetFileCounter();
            List<int[]> arrays2048 = CreateArrays(2);

            // Merge the two arrays.
            Merged2048Array = arrays2048[0].Concat(arrays2048[1]).ToArray();
        }
    }
}