using Caching.Cache;
using System;
using System.Collections.Generic;
using System.Text;

namespace Caching
{
	namespace Cache
	{
		public interface ICache
		{
			// return the value if key exists in cache, or -1 if not. 
			int Get(int key);

			// add a new data entry to cache
			void Add(int key, int value);
		}
	}

	public class LRUCache : ICache
	{
		private int capacity;
		private int count;
		Dictionary<int, LRUNode> map;
		LRUDoubleLinkedList doubleLinkedList;
		public LRUCache(int capacity)
		{
			this.capacity = capacity;
			this.count = 0;
			map = new Dictionary<int, LRUNode>();
			doubleLinkedList = new LRUDoubleLinkedList();
		}

		// each time when access the node, we move it to the top
		public int Get(int key)
		{
			if (!map.ContainsKey(key)) return -1;
			LRUNode node = map[key];
			doubleLinkedList.RemoveNode(node);
			doubleLinkedList.AddToTop(node);
			return node.Value;
		}

		public void Add(int key, int value)
		{
			// just need to update value and move it to the top
			if (map.ContainsKey(key))
			{
				LRUNode node = map[key];
				doubleLinkedList.RemoveNode(node);
				node.Value = value;
				doubleLinkedList.AddToTop(node);
			}
			else
			{
				// if cache is full, then remove the least recently used node
				if (count == capacity)
				{
					LRUNode lru = doubleLinkedList.RemoveLRUNode();
					map.Remove(lru.Key);
					count--;
				}

				// add a new node
				LRUNode node = new LRUNode(key, value);
				doubleLinkedList.AddToTop(node);
				map[key] = node;
				count++;
			}
		}
	}
}
