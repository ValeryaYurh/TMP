using System.Collections;
using LR2.Exceptions;
using LR2.Interfaces;

namespace LR2.Collections
{
    // Реализация ICustomCollection<T> на связном списке.
    // Стандартные коллекции и массивы не используются (задание 1.1, п. d).
    public class MyCustomCollection<T> : ICustomCollection<T>
    {
        private class Node
        {
            public T Value;
            public Node? Next;

            public Node(T value)
            {
                Value = value;
                Next = null;
            }
        }

        private Node? _head;
        private Node? _tail;
        private Node? _cursor;
        private int _count;

        public int Count => _count;

        public T this[int index]
        {
            get => GetNodeAt(index).Value;
            set => GetNodeAt(index).Value = value;
        }

        private Node GetNodeAt(int index)
        {
            if (index < 0 || index >= _count)
                throw new IndexOutOfRangeException("Индекс выходит за границы коллекции");

            Node current = _head!;
            for (int i = 0; i < index; i++)
                current = current.Next!;

            return current;
        }

        public void Add(T item)
        {
            Node node = new Node(item);

            if (_head == null)
            {
                _head = node;
                _tail = node;
            }
            else
            {
                _tail!.Next = node;
                _tail = node;
            }

            _count++;
        }

        public void Remove(T item)
        {
            if (_head == null)
                throw new ItemNotFoundException();

            if (EqualityComparer<T>.Default.Equals(_head.Value, item))
            {
                if (ReferenceEquals(_cursor, _head)) _cursor = _head.Next;
                _head = _head.Next;
                if (_head == null) _tail = null;
                _count--;
                return;
            }

            Node prev = _head;
            Node? current = _head.Next;

            while (current != null)
            {
                if (EqualityComparer<T>.Default.Equals(current.Value, item))
                {
                    prev.Next = current.Next;
                    if (ReferenceEquals(current, _tail)) _tail = prev;
                    if (ReferenceEquals(_cursor, current)) _cursor = current.Next;
                    _count--;
                    return;
                }

                prev = current;
                current = current.Next;
            }

            // Элемент не найден ни в одном узле связного списка
            throw new ItemNotFoundException();
        }

        public void Reset()
        {
            _cursor = _head;
        }

        public void Next()
        {
            if (_cursor == null)
                throw new InvalidOperationException("Курсор не установлен. Сначала вызовите Reset()");

            _cursor = _cursor.Next;
        }

        public T Current()
        {
            if (_cursor == null)
                throw new InvalidOperationException("Текущий элемент не определён");

            return _cursor.Value;
        }

        public T RemoveCurrent()
        {
            if (_cursor == null)
                throw new InvalidOperationException("Нет текущего элемента для удаления");

            Node toRemove = _cursor;
            T value = toRemove.Value;

            if (ReferenceEquals(toRemove, _head))
            {
                _head = _head!.Next;
                _cursor = _head;
                if (_head == null) _tail = null;
            }
            else
            {
                Node prev = _head!;
                while (!ReferenceEquals(prev.Next, toRemove))
                    prev = prev.Next!;

                prev.Next = toRemove.Next;
                if (ReferenceEquals(toRemove, _tail)) _tail = prev;
                _cursor = toRemove.Next;
            }

            _count--;
            return value;
        }

        // Реализация обхода коллекции оператором foreach (задание 2.1, п. a).
        // Идём по узлам связного списка независимо от _cursor, чтобы foreach
        // не конфликтовал с Reset/Next/Current.
        public IEnumerator<T> GetEnumerator()
        {
            Node? node = _head;
            while (node != null)
            {
                yield return node.Value;
                node = node.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
