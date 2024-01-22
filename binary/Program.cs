var sortedNumbers = new List<int> {7,11,23,56,90,340};
var indexOf11 = sortedNumbers.FindItem(11);
var indexOf340 = sortedNumbers.FindItem(340);
var indexOf12 = sortedNumbers.FindItem(12);
var indexOf7 = sortedNumbers.FindItem(7);
var indexOf72 = sortedNumbers.BinarySearch(7);


Console.ReadKey();

public static class ListExtension
{
   public static int? FindItem<T>(this IList<T> list,T target) where T:IComparable<T>
    {
        int left = 0;
        int right = list.Count - 1;


        while (left <= right)
        {
            var middleIndex = (left + right)/2;
            if (target.Equals( list[middleIndex]))
            {
                return middleIndex;
            }
            else if (target.CompareTo(list[middleIndex])<0)
            {
                right = middleIndex - 1;
            }
            else
            {
                left = middleIndex + 1;
            }
        }

        return null;
    }

}

