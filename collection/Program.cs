using System.Collections;
using System.Collections.ObjectModel;

//IEnumerable<int> collection = new List<int>();
//collection.Add(5);



var names=new CustomCollection( new[] { "blue", "lulu", "jaydn" });

foreach (var name in names)
{
    Console.WriteLine(name);
}


//var first=names[0];
//names[2] = "xuan";


//var collection = new CustomCollection()
//{
//    "hello",
//    "apple",
//    "pear"
//};


//var names = ReadNames();
//var readOnlyNames = (ReadOnlyCollection<string>)names;
////readOnlyNames.Clear();


//var dictionary = new Dictionary<string, int>
//{
//    ["jaydn"] = 01101997
//};

//var readOnlyDic = new ReadOnlyDictionary<string,int>(dictionary);
//readOnlyDic.Clear();
//readOnlyDic["jaydn"] = 111;





IEnumerable<string> ReadNames()
{
    var result=new List<string>
    {
       " blue", "lulu", "jaydn" ,
    };
    return new ReadOnlyCollection<string>(result);

}

var letters = new List<string> { "a", "b", null, "d", "e", "f", null, "h", "i" };
var result = YieldExercise.GetAllAfterLastNullReversed(letters);

Console.ReadKey();

public class YieldExercise
{
    public static IEnumerable<T> GetAllAfterLastNullReversed<T>(IList<T> input)
    {
        //your code goes here
        for (int i = input.Count - 1; i >= 0; --i)
        {
            if (input[i] is not null)
            {
                yield return input[i];
            }
            else
            {
                yield break;
            }

        }
    }
}

public class CustomCollection : IEnumerable
{
    public string[] Words;


    public CustomCollection(string[] strings)
    {
        Words = strings;
    }

    public CustomCollection()
    {
        Words = new string[10];
    }

    private int _index=0;

    public void Add(string item)
    {
        Words[_index] = item;
        ++_index;
    }

    public string this[int index]
    {
        get => Words[index];
        
        set=> Words[index]=value;

    }

    IEnumerator IEnumerable.GetEnumerator() //non-generic
    {
        return GetEnumerator();
    }
    public IEnumerator<string> GetEnumerator()//generic
    {
        //return new WordsEnumerator(Words);
        IEnumerable<string> words = Words;
        return words.GetEnumerator();

    }

}

public class WordsEnumerator : IEnumerator<string>
{
    private const int Initial = -1;
    private int _current = Initial;
    private string[] _words { get; }

    public WordsEnumerator(string[] words)
    {
        _words = words;
    }

    object IEnumerator.Current => Current;

    public string Current
    {
        get
        {
            try
            {
                return _words[_current];
            }
            catch (Exception ex)
            {
                throw new IndexOutOfRangeException($"{nameof(CustomCollection)} is out of range" + ex);
            }
        }
    }



    public bool MoveNext()
    {
        ++_current;
        return _current < _words.Length;
    }

    public void Reset()
    {
        _current = Initial;
    }

    public void Dispose()
    {
    }
}
