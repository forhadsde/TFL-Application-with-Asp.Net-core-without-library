using System.Collections;

public class CustomList<T> : IEnumerable<T>
{
    private T[] _items;
    private int _count;

    public CustomList()
    {
        _items = new T[4];
        _count = 0;
    }

    public void Add(T item)
    {
        if (_count == _items.Length)
        {
            IncreaseCapacity();
        }
        _items[_count] = item;
        _count++;
    }

    public bool Remove(T item)
    {
        int index = Array.IndexOf(_items, item, 0, _count);
        if (index < 0)
        {
            return false;
        }
        RemoveAt(index);
        return true;
    }

    public void RemoveAt(int index)
    {
        if (index < 0 || index >= _count)
        {
            throw new ArgumentOutOfRangeException("Index out of range");
        }
        _count--;
        if (index < _count)
        {
            Array.Copy(_items, index + 1, _items, index, _count - index);
        }
        _items[_count] = default(T);
    }


    public void Clear()
    {
        if (_count > 0)
        {
            Array.Clear(_items, 0, _count);
            _count = 0;
        }
    }

    public bool Contains(T item)
    {
        return Array.IndexOf(_items, item, 0, _count) >= 0;
    }

    public int Count
    {
        get { return _count; }
    }

    private void IncreaseCapacity()
    {
        int newCapacity = _items.Length * 2;
        T[] newItems = new T[newCapacity];
        Array.Copy(_items, 0, newItems, 0, _count);
        _items = newItems;
    }

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= _count)
                throw new ArgumentOutOfRangeException("Index out of range");
            return _items[index];
        }
        set
        {
            if (index < 0 || index >= _count)
                throw new ArgumentOutOfRangeException("Index out of range");
            _items[index] = value;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < _count; i++)
        {
            yield return _items[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Reverse()
    {
        int left = 0;
        int right = _count - 1;
        while (left < right)
        {
            T temp = _items[left];
            _items[left] = _items[right];
            _items[right] = temp;
            left++;
            right--;
        }
    }

    public T FirstOrDefault(Func<T, bool> predicate)
    {
        for (int i = 0; i < _count; i++)
        {
            if (predicate(_items[i]))
            {
                return _items[i];
            }
        }
        return default(T);
    }

    public bool Any(Func<T, bool> predicate)
    {
        for (int i = 0; i < _count; i++)
        {
            if (predicate(_items[i]))
            {
                return true;  
            }
        }
        return false;  
    }
}
