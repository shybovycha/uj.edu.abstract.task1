using NUnit.Framework;
using System;
using Exercise1Tree;
using System.Linq;

namespace tests
{
	[TestFixture ()]
	public class Test
	{
		[Test ()]
		public void SearchOrder()
		{
			var subtree = new Tree<int>(5, EnumeratorOrder.BreadthFirstSearch) { 1, 2 };
			var tree = new Tree<int>(7, EnumeratorOrder.BreadthFirstSearch) { subtree, 10, 15 };
			Assert.AreEqual(10, tree.First(i => i % 2 == 0));
			tree.Order = EnumeratorOrder.DepthFirstSearch;
			Assert.AreEqual(2, tree.First(i => i % 2 == 0));
		}

		[Test ()]
		public void OrderPropertyValidation()
		{
			var tree = new Tree<int>(7, EnumeratorOrder.BreadthFirstSearch);
			var subtree = new Tree<int>(5, EnumeratorOrder.DepthFirstSearch);
			tree.Add(subtree);
			Assert.AreEqual(EnumeratorOrder.BreadthFirstSearch, subtree.Order);
		}
	}
}

