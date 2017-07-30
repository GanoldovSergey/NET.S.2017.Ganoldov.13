using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task1
{
    public class Queue<T> : IEnumerable<T>
    {
        private T[] items;
        private int capacity = 8;
        /// <summary>
        /// Quantity of elements in the queue.
        /// </summary>
        public int Count { get; private set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public Queue()
        {
            items = new T[capacity];
        }
        /// <summary>
        /// Constructor that takes parametrized IEnumerable as a parameter.
        /// </summary>
        public Queue(IEnumerable<T> collection) : this()
        {
            foreach (var t in collection)
                Enqueue(t);
        }

        /// <summary>
        /// Add a new element to the tail of the queue.
        /// </summary>
        /// <param name="item">Element to add.</param>
        public void Enqueue(T item)
        {
            if (IsFull())
            {
                capacity *= 2;
                Array.Resize(ref items, capacity);
            }
            items[Count] = item;
            Count++;
        }

        /// <summary>
        /// Remove an element from the head of the queue.
        /// </summary>
        /// <returns>An element.</returns>
        public T Dequeue()
        {
            T item = items[0];
            for (int i = 1; i < Count; i++)
            {
                items[i - 1] = items[i];
            }
            items[--Count] = default(T);
            return item;
        }

        /// <summary>
        /// Show an element in the head of the queue.
        /// </summary>
        /// <returns>An element.</returns>
        public T Peek()
        {
            if (Count == 0)
                throw new InvalidOperationException("queue is empty");

            return items[0];
        }

        /// <summary>
        /// Clear the queue.
        /// </summary>
        public void Clear()
        {
            items = new T[capacity];
        }

        private bool IsFull() => Count == capacity;
        public void CopyTo(T[] array, int arrayIndex = 0)
        {
            for (int i = 0; i < Count; i++)
            {
                array[arrayIndex + i] = items[i];
            }
        }
        public T[] ToArray()
        {
            T[] array = new T[Count];
            CopyTo(array);
            return array;
        }
        public IEnumerator<T> GetEnumerator() => new IteratorSet(this);
        IEnumerator IEnumerable.GetEnumerator() => new IteratorSet(this);


        private class IteratorSet : IEnumerator<T>
        {
            private Queue<T> queue;
            public IteratorSet(Queue<T> queue)
            {
                this.queue = queue;
            }
            private int iterator = -1;
            public bool MoveNext() => ++iterator < queue.Count;
            public T Current => queue.items[iterator];
            object IEnumerator.Current => queue.items[iterator];

            public void Reset()
            {
                iterator = -1;
            }
            public void Dispose() { }
        }
    }
}