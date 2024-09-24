namespace advanced_CS
{
    internal class Program
    {
        class Stack<T>
        {
            private T[] elements;
            private int top;

            public Stack()
            {
                elements = new T[100];
                top = 0;
            }

            public void Push(T item)
            {
                elements[top++] = item;
            }

            public T Pop()
            {
                return elements[--top];
            }
        }


        static void Main(string[] args)
        {
            Stack<int> intStack = new Stack<int>();
            for (int i = 0; i < 100; i++)
            {
                intStack.Push(i);
            }
            Console.WriteLine(intStack.Pop());
        }
    }
}
