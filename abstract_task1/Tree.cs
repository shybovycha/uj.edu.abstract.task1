using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise1Tree
{
	public class Tree<T> : IEnumerable<T>
		where T : IComparable
	{
        protected EnumeratorOrder order;

        public EnumeratorOrder Order 
        { 
            get {
                return order;
            }

            set {
                this.order = value;

                if (Children == null) {
                    return;
                }

                foreach (Tree<T> child in Children) {
                    child.Order = value;
                }
            } 
        }

        protected List<Tree<T>> Children { get; set; }
		public T Value { get; set; }

		public Tree(T value, EnumeratorOrder ordering)
		{
            Children = new List<Tree<T>>();

			Value = value;
			Order = ordering;
		}

		public Tree(Tree<T> parent, T value, EnumeratorOrder ordering)
		{
			Value = value;
			Order = ordering;

			Children = new List<Tree<T>>();
		}

		public void Add(Tree<T> elt)
		{
			elt.Order = Order;
			Children.Add(elt);
		}

		public void Add(T elt)
		{
			Tree<T> subtree = new Tree<T>(this, elt, Order);
			Children.Add(subtree);
		}

		protected List<Tree<T>> BFS()
		{
			Queue<Tree<T>> queue = new Queue<Tree<T>>();
			List<Tree<T>> res = new List<Tree<T>>();

			foreach (Tree<T> child in Children) 
			{
				queue.Enqueue(child);
			}

			while (queue.Count > 0)
			{
				Tree<T> element = queue.Dequeue();

				res.Add(element);

				foreach (Tree<T> child in element.Children)
				{
					queue.Enqueue(child);
				}
			}

			return res;
		}

		protected List<Tree<T>> DFS(Tree<T> startAt = null, List<Tree<T>> list = null)
		{
			if (startAt == null) {
				startAt = this;
			}

			if (list == null) {
				list = new List<Tree<T>>();
			}

			list.Add(startAt);

			foreach (Tree<T> element in startAt.Children)
			{
				list = DFS(element, list);
			}

			return list;
		}

		public IEnumerator<T> GetEnumerator()
		{
			List<Tree<T>> data;
			List<T> values = new List<T>();

			if (Order == EnumeratorOrder.BreadthFirstSearch) {
				data = BFS();
			} else {
				data = DFS();
			}

			foreach (Tree<T> tree in data) {
				values.Add(tree.Value);
			}

			return values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}
}