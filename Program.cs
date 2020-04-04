using System;
using System.Collections;
using System.IO;

namespace HW1
{

    public abstract class Deque : IEnumerable
    {
        public abstract int Size { get; set; }
        public abstract void Clear();
        public abstract void Unshift(int item);
        public abstract int Shift();
        public abstract void Push(int item);
        public abstract int Pop();

        public abstract DequeEnumerator GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public abstract class DequeEnumerator : IEnumerator
    {
        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public abstract int Current { get; }
        public abstract bool MoveNext();
        public abstract void Reset();
    }


    public class DLink
    {
        public DLink Next { get; set; }
        public DLink Previous { get; set; }
        public int Data { get; set; }
        public DLink(int data, DLink next, DLink previous)
        {
            this.Data = data;
            this.Next = next;
            this.Previous = previous;
        }
    }

    class DLinkDeque : Deque
    {
        private DLink tail;
        private DLink head;

        public override int Size { get; set;  }
        public DLinkDeque()
        {
            this.head = this.tail = null;
            this.Size = 0;
        }


        public override void Push(int item)
        {
            if (head == null)
            {
                // Becase head is null, the list is empty, so create the first new link
                // and set that as the head and tail. At this point, head and tail 
                // should be null and Length should be 0
                this.head = new DLink(item, null, null);
                this.tail = head;
                this.Size++;
            }
            else
            {
                DLink newLink = new DLink(item, null, this.tail);
                this.tail.Next = newLink;

                this.tail = newLink;
                this.Size++;
            }
        }

        public override void Unshift(int item)
        {
            if (head == null)
            {
                // Becase head is null, the list is empty, so create the first new link
                // and set that as the head and tail. At this point, head and tail 
                // should be null and Length should be 0
                this.head = new DLink(item, null, null);
                this.tail = head;
                this.Size++;
            }
            else
            {
                DLink newLink = new DLink(item, this.head, null);
                this.head.Previous = newLink;
                this.head = newLink;
                this.Size++;
            }
        }

        public override int Pop()
        {
            if (head != null)
            {
                if (this.Size == 1)
                {
                    this.head = null;
                    this.tail = null;
                    this.Size--;
                }
                else
                {
                    this.tail.Previous.Next = null;
                    this.tail = this.tail.Previous;
                    this.Size--;
                }
            }
            return this.Size;
        }

        public override int Shift()
        {
            if (head != null)
            {
                if (this.Size == 1)
                {
                    this.head = null;
                    this.tail = null;
                    this.Size--;
                }
                else
                {
                    this.head.Next.Previous = null;
                    this.head = this.head.Next;
                    this.Size--;
                }
            }
            return this.Size;
        }


        

        public override void Clear()
        {
            while (this.Size >= 1)
            {
                if (head != null)
                {
                    if (this.Size == 1)
                    {
                        this.head = null;
                        this.tail = null;
                        this.Size--;
                    }
                    else
                    {
                        this.head.Next.Previous = null;
                        this.head = this.head.Next;
                        this.Size--;
                    }
                }
            }
        }

        //BURAYA KADAR

        public override DequeEnumerator GetEnumerator()
        {
            return new DLinkEnumerator(this.head, this.tail, this.Size);
        }



   




        }

    public class DLinkEnumerator : DequeEnumerator
    {
        private DLink head;
        private DLink tail;
        private DLink currentLink;
        private int length;
        private bool startedFlag;

        public DLinkEnumerator(DLink head, DLink tail, int length)
        {
            this.head = head;
            this.tail = tail;
            this.currentLink = null;
            this.length = length;
            this.startedFlag = false;
        }

        public override int Current
        {
            get { return this.currentLink.Data; }
        }

        public void Dispose()
        {
            this.head = null;
            this.tail = null;
            this.currentLink = null;
        }

        public override bool MoveNext()
        {
            if (this.startedFlag == false)
            {
                this.currentLink = this.head;
                this.startedFlag = true;
            }
            else
            {
                this.currentLink = this.currentLink.Next;
            }

            return this.currentLink != null;
        }

        public override void Reset()
        {
            this.currentLink = this.head;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            // You must replace this with the class you create
            // that uses a doubly linked list:
            HW1.Deque deque = new HW1.DLinkDeque();
            // Deque deque = new DLinkDeque();


            string[] lines = File.ReadAllLines("integers.txt");
            int[] integers = new int[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                int temp = int.Parse(lines[i]);
                integers[i] = temp;
            }


            foreach (int integer in integers)
            {
                deque.Push(integer);
            }


            foreach (var item in deque)
            {
                Console.WriteLine(item);
            }
        }
    }
}




