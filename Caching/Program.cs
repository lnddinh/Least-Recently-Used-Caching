using System;

namespace Caching
{
	class Program
    {
        static void Main(string[] args)
        {
            int cap = 10;
            LRUCache cache = new LRUCache(cap);
            for (int i = 1; i <= cap + 5; i++)
            {
                cache.Add(i, i);
            }

            for (int i = 1; i <= cap + 5; i++)
            {
                Console.Write(cache.Get(i) + " ");
            }
            Console.ReadLine();
        }
    }
}
