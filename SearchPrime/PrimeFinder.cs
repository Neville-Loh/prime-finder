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
        private int[] _primes;
        private int n;
        private int _lastIndex;

        public PrimeFinder(int n)
        {
            this.n = n;
        }

        public void findSolution()
        {
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            _primes = SieveOfEratosthenes(n);
            int i = CalculateStartingIndex();
            Console.WriteLine(String.Join(", ", _primes));
            //Console.WriteLine(_primes[i]);
            Console.WriteLine(i);
            //Console.WriteLine(_primes[lastIndex]);
            Console.WriteLine(_lastIndex);
            Console.WriteLine(_primes.Length);
            dfs_tree(i, new List<int>(), 1);
            watch.Stop();
            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds } ms");

        }
        
        private void dfs_tree(int index, List<int> path, long mul)
        {
            if (index >= _lastIndex) return;
            if (path.Count == 4)
            {
                if (IsValid(mul * path[3]))
                {
                    Console.WriteLine(string.Join(", ", path));
                    Console.WriteLine(mul * path[3]); 
                }
            }
            else
            {
                
                mul = path.Count > 0 ? mul * path[^1] : 1;
                if (ShouldTrim(mul, path)) return;
                for (var i = index; i < _primes.Length; i++)
                {
                   path.Add(_primes[i]);
                   dfs_tree(i+1, path, mul);
                   path.RemoveAt(path.Count-1);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int CalculateStartingIndex()
        {
            if (_primes.Length < 3) return 0;
            long mul = _primes[^1] * _primes[^2] * _primes[^3];
            var i = 0;
            while ((mul * _primes[i]).ToString().Length < 12)
            {
                i++;
            }
            var j = _primes.Length - 1;
            mul = _primes[i+1] * _primes[i+2] * _primes[i+3];
            while ((mul * _primes[j]).ToString().Length > 12)
            {
                j--;
            }

            this._lastIndex = j;
            return i;
        }

        private static bool IsValid(long n)
        {
            string s = n.ToString();
            if (s.Length != 12)
            {
                return false;
            }
            return s == string.Join("", s.OrderBy(i => i.ToString()));
        }
        

        private bool ShouldTrim(long mul, List<int> path)
        {
            return path.Count switch
            {
                2 => (mul * _primes[^1] * _primes[^2]).ToString().Length < 12,
                3 => (mul * _primes[^1]).ToString().Length < 12,
                _ => false
            };
        }

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