
using System.Collections;

//var nums = new[] { 2, 4 };

var linked=new SingleLinkedList<string>();


linked.AddToFront("a");
linked.AddToFront("b");
linked.Add("c");
linked.AddToEnd("d");
linked.Remove("a");
var containsA = linked.Contains("a");
var containsD = linked.Contains("d");
//linked.Clear();
var array = new string[7];
linked.CopyTo(array, 2);

foreach(var item in linked)
{
    Console.WriteLine(item);
}

Console.ReadKey();




public interface ILinkedList<T> : ICollection<T>
     
{
    void AddToFront(T? item);
    void AddToEnd(T? item);
}











public class SingleLinkedList<T> : ILinkedList<T?>, IEnumerable
{


    private class Node
    {
        public T? Value { get;  }
        public Node? Next { get; set; }

        public Node(T? value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return $"Value: {Value}, Next: {(Next is null? "null":Next.Value)}.";
        }

    }
  
    private Node? _head;
    private int _count=0;

    public int Count
    {
        get => _count;
    }
    
    

    public void AddToFront(T? item)
    {
        var newHead = new Node(item);

        newHead.Next = _head;
        _head = newHead;
        ++_count;
       
    }

    public void AddToEnd(T? item)
    {
        var newNode = new Node(item);

        if (_head is null)
        {
            _head = newNode;
            
        }
        else
        {
            // Node<T?> lastNode = GetLastNode(_nodes);
           var lastNode = GetNodes().Last();
            lastNode.Next = newNode;
            newNode.Next = null;
        }
        ++_count;
       
    }

    //private Node<T>? GetLastNode(List<Node<T>>? nodes)
    //{
    //    var temp = _head;
    //    while (temp.Next != null)
    //    {
    //        temp = temp.Next;
    //    }
    //    return temp;
    //}




    public bool IsReadOnly
    {
       get=> false;
    }

    public void Add(T? item)
    {
       AddToEnd(item);
    }

    public void Clear()
    {
        Node? current = _head;
        while(current is not null)
        {
            Node? temp = current;
            current = current.Next;
            temp.Next = null;
        }
        _head = null;
        _count = 0;
    }

    public bool Contains(T? item)
    {
        if(item is null)
        {
            return GetNodes().Any(node => node.Value is null);

        }
        return GetNodes().Any(node => item.Equals(node.Value));
    }

    public IEnumerator<T?> GetEnumerator()
    {
        foreach(var node in GetNodes())
        {
            yield return node.Value;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private IEnumerable<Node> GetNodes()
    {
        Node? temp = _head;
        while (temp!.Next != null)
        {
            yield return temp;
            temp = temp.Next;
        }
        yield return temp;
    }

    public bool Remove(T? item)
    {
        Node? prev = null;

        foreach(var node in GetNodes())
        {

            if((node.Value is null && item is null)||(node.Value is not null && node.Value.Equals(item)) )
            {
                if(prev is null)//when the node to be removed is the head---first iteration
                {
                    _head = node.Next;
                }
                else
                {
                    prev.Next = node.Next;
                    
                }
                --_count;
                return true;
            }
            prev = node;   //give each node value to prev so that it becomes the predecessor of next node in iteration
        }
        return false;
    }

    public void CopyTo(T?[] arr, int index)
    {
        if(arr is null)
        {
            throw new ArgumentNullException(nameof(arr));
        }
        if(index < 0 || index > arr.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }
        if(arr.Length < _count + index)
        {
            throw new ArgumentException("the array is too short!");
        }
          
        foreach(var node in GetNodes())
        {
            arr[index] = node.Value;
            ++index;
        }
    }
}
