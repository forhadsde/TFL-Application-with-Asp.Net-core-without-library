public class CustomPriorityQueue<TElement, TPriority> where TPriority : IComparable<TPriority>
{
    private CustomList<(TElement element, TPriority priority)> elements = new CustomList<(TElement, TPriority)>();

    public int Count => elements.Count;

    public void Enqueue(TElement element, TPriority priority)
    {
        elements.Add((element, priority));
        int childIndex = elements.Count - 1;
        while (childIndex > 0)
        {
            int parentIndex = (childIndex - 1) / 2;
            if (elements[childIndex].priority.CompareTo(elements[parentIndex].priority) >= 0) 
                break;
            
            (elements[childIndex], elements[parentIndex]) = (elements[parentIndex], elements[childIndex]);
            childIndex = parentIndex;
        }
    }

    public TElement Dequeue()
    {
        if (Count == 0)
            throw new InvalidOperationException("The queue is empty.");

        TElement topElement = elements[0].element;
        elements[0] = elements[Count - 1];
        elements.RemoveAt(Count - 1);

        int parentIndex = 0;
        while (true)
        {
            int leftChildIndex = 2 * parentIndex + 1;
            int rightChildIndex = 2 * parentIndex + 2;
            int smallestIndex = parentIndex;

            if (leftChildIndex < Count && elements[leftChildIndex].priority.CompareTo(elements[smallestIndex].priority) < 0)
                smallestIndex = leftChildIndex;
            if (rightChildIndex < Count && elements[rightChildIndex].priority.CompareTo(elements[smallestIndex].priority) < 0)
                smallestIndex = rightChildIndex;

            if (smallestIndex == parentIndex)
                break;

            (elements[parentIndex], elements[smallestIndex]) = (elements[smallestIndex], elements[parentIndex]);
            parentIndex = smallestIndex;
        }

        return topElement;
    }
}
