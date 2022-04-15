using System;
using System.Collections.Generic;
using System.Linq;

namespace SearchPrime
{
    /// <summary>
    /// 
    /// </summary>
    public class PrimeFinder
    {
        // maximal limit of prime
        private int _n;
        // array that contain primes up to n
        private int[] _primes;
        
        // define the search range of them prime
        private int _startIndex;
        private int _endIndex;
        
        private static int REQUIRED_LENGTH = 12;
        
        
        public void FindSolution(int n)
        {
            _primes = SieveOfEratosthenes(n);
            SetStartAndEndIndex();
            dfs_tree(_startIndex, new List<int>(), 1);
        }
        
        /// <summary>
        /// Exhaustive search using DFS for every possible, no duplicated computation is run
        /// Time, space complexity of O(n^4)
        /// Subtree are killed early if it is deem not possible
        /// </summary>
        /// <param name="index">
        /// The starting prime index, only increase during recursive call
        /// </param>
        /// <param name="path">
        /// Array that contains the prime number
        /// </param>
        /// <param name="product">
        /// Current multiple of path, so multiplication is not repeated during recursive call 
        /// </param>
        private void dfs_tree(int index, List<int> path, long product)
        {
            if (index >= _endIndex) return;
            
            // calculate the product of all prime in current search
            product = path.Count > 0 ? product * path[^1] : 1;
            
            // return if path is 4
            if (path.Count == 4)
            {
                // print the number if it meet the requirement
                if (IsValid(product))
                {
                    Console.WriteLine(string.Join(", ", path));
                    Console.WriteLine(product); 
                }
            }
            else
            {
                if (ShouldTrim(product, path.Count)) return;
                
                // Recursively called every other prime that is not searched previously
                for (var i = index; i < _primes.Length; i++)
                {
                   path.Add(_primes[i]);
                   dfs_tree(i+1, path, product);
                   path.RemoveAt(path.Count-1);
                }
            }
        }

        /// <summary>
        /// Calculate the starting and ending index based on the existing prime, if the prime with prime at start index
        /// multiple the largest possible prime is less than require length, shift the starting index.
        /// vice versa for ending index, if reject
        /// </summary>
        /// <returns></returns>
        private void SetStartAndEndIndex()
        {
            if (_primes.Length < 3) return;
            
            var i = 0;
            // generate largest prime possible prime
            long largestPossiblePrime = _primes[^1] * _primes[^2] * _primes[^3];
            while ((largestPossiblePrime * _primes[i]).ToString().Length < 12)
            {
                i++;
            }

            _startIndex = i;
            
            // generate smallest possible prime
            var j = _primes.Length - 1;
            long smallestPossiblePrime = _primes[i+1] * _primes[i+2] * _primes[i+3];
            while ((smallestPossiblePrime * _primes[j]).ToString().Length > 12)
            {
                j--;
            }

            _endIndex = j;
        }

        /// <summary>
        /// Check if the string is ascending, and of the required length
        /// </summary>
        /// <param name="n">
        /// the input number
        /// </param>
        /// <returns>
        /// True if it is, else false
        /// </returns>
        private static bool IsValid(long n)
        {
            string s = n.ToString();
            if (s.Length != REQUIRED_LENGTH)
            {
                return false;
            }
            return s == string.Join("", s.OrderBy(i => i.ToString()));
        }
        
        /// <summary>
        /// Optimization, kill the search tree if the current path multiply by the largest prime is less than
        /// the required length of product
        /// 
        /// </summary>
        /// <param name="product">
        /// the current result product 
        /// </param>
        /// <param name="pathLength">
        /// the current length of the path
        /// </param>
        /// <returns></returns>
        private bool ShouldTrim(long product, int pathLength)
        {
            return pathLength switch
            {
                2 => (product * _primes[^1] * _primes[^2]).ToString().Length < REQUIRED_LENGTH,
                3 => (product * _primes[^1]).ToString().Length < REQUIRED_LENGTH,
                _ => false
            };
        }

        /// <summary>
        /// Helper function to generate all primes, O(n)
        /// </summary>
        /// <param name="n">
        /// Upper limit of prime
        /// </param>
        /// <returns>
        /// All prime number in array up to n, in ascending order
        /// </returns>
        private static int[] SieveOfEratosthenes(int n)
        {
 
            // Create a boolean array "prime[0..n]" and initialize all entries
            // it as true. A value in prime[i] will finally be false if i is Not a prime, else true.
 
            bool[] prime = new bool[n + 1];
 
            for ( int i = 0; i <= n; i++)
                prime[i] = true;
 
            for ( int p = 2; p * p <= n; p++)
            {
                // If prime[p] is not changed, then it is a prime
                if (prime[p] != true) continue;
                // Update all multiples of p
                for (int i = p * p; i <= n; i += p)
                    prime[i] = false;
            }
            // Print all prime numbers
            List<int> list = new List<int>();
            for (int i = 2; i <= n; i++)
            {
                if (prime[i])
                {
                    list.Add(i);
                }
                

            }
            return list.ToArray();
        }
    }
}