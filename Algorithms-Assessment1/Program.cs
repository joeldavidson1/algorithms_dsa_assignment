using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace Algorithms_Assessment1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // Start the sorting process.
            Input.StartSorting(false);
            
            // Start the searching process.
            Input.StartSearching(false);
        }
    }
}