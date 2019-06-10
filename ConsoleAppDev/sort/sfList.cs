using System;
using System.Collections.Generic;
using Sorting.StringSort;

namespace SortTests.Sorting
{
  public class sfList : List<string>
  {
    public sfList() : base() { }

    public sfList(int size) : base(size) { }

    public sfList(IEnumerable<String> aArray) : base(aArray) { }

    public new void Sort()
    {
      string[] a = this.ToArray();
      this.Clear();
      //sort array and refill contents of this (faster than directly sorting List)
      StringSort.Sort(a);
      this.AddRange(a);
    }
  }
}
