using System;

namespace BinMaxHeap
{
    internal class Program
    {
        public static void Main()
        {
            var myHeap = new MyBinMaxHeap(100001);
            var commandsAmount = int.Parse(Console.ReadLine());
            for (var i = 0; i < commandsAmount; i++)
            {
                var commandParams = Console.ReadLine().Split();
                switch (commandParams[0])
                {
                    case "Insert" :
                        myHeap.Add(int.Parse(commandParams[1]));
                        break;
                    case "ExtractMax" :
                        Console.WriteLine(myHeap.GetMax());
                        break;
                    default: return;
                }
            }
        }
    }

    internal struct MyBinMaxHeap
    {
        private readonly int[] _body;
        private int _indexOfLastElement;

        public MyBinMaxHeap(int sizeOfHeap)
        {
            _body = new int [sizeOfHeap];
            _indexOfLastElement = -1;
        }

        internal void Add(int elementToAdd)
        {
            _body[++_indexOfLastElement] = elementToAdd;
            RaiseOut(_indexOfLastElement);
        }

        private void RaiseOut(int elemToRaiseIndex)
        {
            var currentParenInd = GetParentIndex(elemToRaiseIndex);
            while (currentParenInd >= 0 && _body[currentParenInd] < _body[elemToRaiseIndex])
            {
                Swap(elemToRaiseIndex, currentParenInd);
                elemToRaiseIndex = currentParenInd;
                currentParenInd = GetParentIndex(elemToRaiseIndex);
            }
        }

        internal int GetMax()
        {
            var maxToReturn = _body[0];
            RemoveElement(0);
            return maxToReturn;
        }

        private void RemoveElement(int indexOfElement)
        {
            Swap(indexOfElement, _indexOfLastElement);
            _indexOfLastElement--;
            Drown(indexOfElement);
        }

        private void Drown(int elemToDrownIndex)
        {
            var maxChildIndex = GetMaxChildIndex(elemToDrownIndex);
            while (maxChildIndex >= 0 && _body[maxChildIndex] > _body[elemToDrownIndex])
            {
                Swap(maxChildIndex, elemToDrownIndex);
                elemToDrownIndex = maxChildIndex;
                maxChildIndex = GetMaxChildIndex(elemToDrownIndex);
            }
        }

        private int GetMaxChildIndex(int elemToDrownIndex)
        {
            var presumedLeftIndex = GetLeftChildIndex(elemToDrownIndex);
            if (presumedLeftIndex < 0 || presumedLeftIndex > _indexOfLastElement)
                return -1;
            var presumedRightIndex = presumedLeftIndex + 1;
            if (presumedRightIndex > _indexOfLastElement) return presumedLeftIndex;
            return presumedRightIndex > presumedLeftIndex ? presumedRightIndex : presumedLeftIndex;
        }

        private void Swap(int index1, int index2)
        {
            var buffer = _body[index1];
            _body[index1] = _body[index2];
            _body[index2] = buffer;
        }

        private static int GetParentIndex(int childIndex) => (childIndex - 1) / 2;
        private static int GetLeftChildIndex(int parentIndex) => parentIndex * 2 + 1;
    }
}