﻿using System.Collections.Generic;

namespace Sorting.CSharpStringSort
{
  public static class StringSort
  {

    public static string[] Sort(string[] input)
    {
      string[] copy = new string[input.Length];
      input.CopyTo(copy, 0);
      InPlaceSort(copy, 0, 0, copy.Length);
      return copy;
    }

    public static void Sort(ref string[] inout)
    {
      InPlaceSort(inout, 0, 0, inout.Length);
    }

    public static void Sort(List<string> sList)
    {
      InPlaceSort(sList, 0, 0, sList.Count);
    }

    static void InPlaceSort(List<string> input, int depth, int st, int ed)
    {
      int len = ed - st;
      if (len > 1)
      {
        if (len < 10)
        {
          //this in general depends on the length of the strings to be sorted
          //if the strings are long make the bound less (e.g. 10)
          insertionSort(input, st, len, depth);
        }
        else
        {
          int st1 = st;
          int ed1 = ed - 1;
          int eqStart;
          int eqEnd;

          int pl = st;
          //int pm = st + (len / 2);
          int pm = st + (len >> 1);
          int pn = ed1;
          if (len > 30)
          { // On big arrays, pseudomedian of 9
            //int d = (len / 8);
            int d = (len >> 3);
            pl = med3func(input, pl, pl + d, pl + 2 * d, depth);
            pm = med3func(input, pm - d, pm, pm + d, depth);
            pn = med3func(input, pn - 2 * d, pn - d, pn, depth);
          }
          pm = med3func(input, pl, pm, pn, depth);
          string pivot = input[pm];

          if (depth < pivot.Length)
          {
            char pivotChar = pivot[depth];
            while (st1 <= ed1 && 0 == (cmpByPivotChar(depth, input[st1], pivotChar)))
            {
              st1 = st1 + 1;
            }

            eqStart = st1 - 1;

            while (st1 <= ed1 && 0 == (cmpByPivotChar(depth, input[ed1], pivotChar)))
            {
              ed1 = ed1 - 1;
            }

            eqEnd = ed1 + 1;

            int cmpFlag = 0;
            bool flag = (st1 <= ed1);

            while (flag)
            {
              while (flag)
              {
                cmpFlag = cmpByPivotChar(depth, input[st1], pivotChar);
                if (cmpFlag < 0)
                {
                  st1 = st1 + 1;
                  flag = (st1 <= ed1);
                }
                else if (cmpFlag == 0)
                {
                  eqStart = eqStart + 1;
                  swap(input, st1, eqStart);
                  st1 = st1 + 1;
                }
                else
                  flag = false;
              }


              flag = (st1 <= ed1);
              while (flag)
              {
                cmpFlag = cmpByPivotChar(depth, input[ed1], pivotChar);
                if (cmpFlag > 0)
                {
                  ed1 = ed1 - 1;
                  flag = (st1 <= ed1);
                }
                else if (cmpFlag == 0)
                {
                  eqEnd = eqEnd - 1;
                  swap(input, ed1, eqEnd);
                  ed1 = ed1 - 1;
                }
                else
                  flag = false;
              }
              flag = (st1 <= ed1);
              if (flag)
              {
                swap(input, st1, ed1);
                st1 = st1 + 1;
                ed1 = ed1 - 1;
              }
            }
          }
          else
          {
            while (st1 <= ed1 && 0 == (cmpByPivotLen(depth, input[st1])))
            {
              st1 = st1 + 1;
            }
            eqStart = st1 - 1;

            while (st1 <= ed1 && 0 == (cmpByPivotLen(depth, input[ed1])))
            {
              ed1 = ed1 - 1;
            }
            eqEnd = ed1 + 1;

            int cmpFlag = 0;
            bool flag = (st1 <= ed1);

            while (flag)
            {
              while (flag)
              {
                cmpFlag = cmpByPivotLen(depth, input[st1]);
                if (cmpFlag < 0)
                {
                  st1 = st1 + 1;
                  flag = (st1 <= ed1);
                }
                else if (cmpFlag == 0)
                {
                  eqStart = eqStart + 1;
                  swap(input, st1, eqStart);
                  st1 = st1 + 1;
                }
                else
                  flag = false;
              }
              flag = (st1 <= ed1);
              while (flag)
              {
                cmpFlag = cmpByPivotLen(depth, input[ed1]);
                if (cmpFlag > 0)
                {
                  ed1 = ed1 - 1;
                  flag = (st1 <= ed1);
                }
                else if (cmpFlag == 0)
                {
                  eqEnd = eqEnd - 1;
                  swap(input, ed1, eqEnd);
                  ed1 = ed1 - 1;
                }
                else
                  flag = false;
              }
              flag = (st1 <= ed1);
              if (flag)
              {
                swap(input, st1, ed1);
                st1 = st1 + 1;
                ed1 = ed1 - 1;
              }
            }
          }
          int i = st;
          int j = st1 - 1;
          while (i <= eqStart)
          {
            swap(input, i, j);
            i = i + 1;
            j = j - 1;
          }

          i = ed - 1;
          j = st1;
          while (i >= eqEnd)
          {
            swap(input, i, j);
            i = i - 1;
            j = j + 1;
          }

          eqEnd = st1 + (ed - eqEnd);

          if (ed - eqEnd > 1)
          {
            InPlaceSort(input, depth, eqEnd, ed);
          }
          eqStart = st1 - (eqStart + 1 - st);
          //st..eqStart..eqEnd..ed
          if (eqEnd - eqStart > 1)
          {
            if (input[eqStart].Length > depth)
            {
              InPlaceSort(input, (depth + 1), eqStart, eqEnd);
            }
          }
          if (eqStart - st > 1)
          {
            InPlaceSort(input, depth, st, eqStart);
          }
        }
      }
    }

    static void insertionSort(string[] x, int a, int len, int depth)
    {
      int pi = a + 1;
      int n = len - 1;
      //propages maximum to last position
      while (n > 0)
      {
        int pj = pi;
        while (pj > a)
        {
          int d = depth;
          int pj1 = pj - 1;
          string s = x[pj1];
          string t = x[pj];
          int s_len = s.Length;
          int t_len = t.Length;
          int min_len = s_len;
          if (t_len < s_len) min_len = t_len;
          while (d < min_len && s[d] == t[d])
          {
            d = d + 1;
          }
          if (d == s_len || (d < t_len && s[d] <= t[d]))
          {
            break;
          }
          else
          {
            string tmp = x[pj];
            x[pj] = x[pj1];
            x[pj1] = tmp;
            pj = pj1;
          }
        }
        n = n - 1;
        pi = pi + 1;
      }
    }

    static void insertionSort(List<string> x, int a, int len, int depth)
    {
      int pi = a + 1;
      int n = len - 1;
      //propages maximum to last position
      while (n > 0)
      {
        int pj = pi;
        while (pj > a)
        {
          int d = depth;
          int pj1 = pj - 1;
          string s = x[pj1];
          string t = x[pj];
          int s_len = s.Length;
          int t_len = t.Length;
          int min_len = s_len;
          if (t_len < s_len) min_len = t_len;
          while (d < min_len && s[d] == t[d])
          {
            d = d + 1;
          }
          if (d == s_len || (d < t_len && s[d] <= t[d]))
          {
            break;
          }
          else
          {
            string tmp = x[pj];
            x[pj] = x[pj1];
            x[pj1] = tmp;
            pj = pj1;
          }
        }
        n = n - 1;
        pi = pi + 1;
      }
    }

    static void swap(string[] arr, int i, int j)
    {
      string tmp = arr[i];
      arr[i] = arr[j];
      arr[j] = tmp;
    }

    static void swap(List<string> arr, int i, int j)
    {
      string tmp = arr[i];
      arr[i] = arr[j];
      arr[j] = tmp;
    }

    static int fullCmp(int depth, string a, string b)
    {
      int lenA = a.Length;
      int lenB = b.Length;
      if (lenA == depth && lenB == depth)
        return 0;
      else
        if (lenA == depth)
          return -1; //a is empty, b is not empty
        else if (lenB == depth)
          return 1;
        else
        {
          char ch1 = a[depth];
          char ch2 = b[depth];
          if (ch1 < ch2)
          {
            return -1;
          }
          else if (ch1 == ch2)
            return 0;
          else
            return 1;
        }
    }

    static int cmpByPivotChar(int depth, string a, char ch2)
    {
      int lenA = a.Length;
      if (lenA == depth)
        return -1;
      else
      {
        char ch1 = a[depth];
        if (ch1 < ch2)
          return (-1);
        else if (ch1 == ch2)
          return 0;
        else
          return 1;
      }
    }

    static int cmpByPivotLen(int depth, string a)
    {
      int lenA = a.Length;
      if (lenA == depth)
        return 0;
      else
        return 1;
    }

    static int midOf(int a, int b)
    {
      return (a + ((b - a + 1) >> 1));
    }

    static void swapIfGreater(string[] input, int depth, int i, int j)
    {
      if (i != j && (fullCmp(depth, input[i], input[j]) > 0))
      {
        swap(input, i, j);
      }
    }


    static void swapIfGreater(List<string> input, int depth, int i, int j)
    {
      if (i != j && (fullCmp(depth, input[i], input[j]) > 0))
      {
        swap(input, i, j);
      }
    }

    static void sortPivots(string[] input, int depth, int s, int m, int e)
    {
      swapIfGreater(input, depth, s, m); //st1 <= mid
      swapIfGreater(input, depth, s, e); //st1 <= ed1
      swapIfGreater(input, depth, m, e); //mid <= ed1
    }

    static void sortPivots(List<string> input, int depth, int s, int m, int e)
    {
      swapIfGreater(input, depth, s, m); //st1 <= mid
      swapIfGreater(input, depth, s, e); //st1 <= ed1
      swapIfGreater(input, depth, m, e); //mid <= ed1
    }

    static char at(string s, int pos)
    {
      if (pos >= s.Length)
        return (char)0;
      return s[pos];
    }

    static int med3func(string[] x, int a, int b, int c, int depth)
    {
      char va, vb, vc;
      if ((va = at(x[a], depth)) == (vb = at(x[b], depth)))
        return a;
      if ((vc = at(x[c], depth)) == va || vc == vb)
        return c;
      return va < vb ?
            (vb < vc ? b : (va < vc ? c : a))
          : (vb > vc ? b : (va < vc ? a : c));
    }

    static int med3func(List<string> x, int a, int b, int c, int depth)
    {
      char va, vb, vc;
      if ((va = at(x[a], depth)) == (vb = at(x[b], depth)))
        return a;
      if ((vc = at(x[c], depth)) == va || vc == vb)
        return c;
      return va < vb ?
            (vb < vc ? b : (va < vc ? c : a))
          : (vb > vc ? b : (va < vc ? a : c));
    }

    static void InPlaceSort(string[] input, int depth, int st, int ed)
    {
      int len = ed - st;
      if (len > 1)
      {
        if (len < 10)
        {
          //this in general depends on the length of the strings to be sorted
          //if the strings are long make the bound less (e.g. 10)
          insertionSort(input, st, len, depth);
        }
        else
        {
          int st1 = st;
          int ed1 = ed - 1;
          int eqStart;
          int eqEnd;

          int pl = st;
          //int pm = st + (len / 2);
          int pm = st + (len >> 1);
          int pn = ed1;
          if (len > 30)
          { // On big arrays, pseudomedian of 9
            //int d = (len / 8);
            int d = (len >> 3);
            pl = med3func(input, pl, pl + d, pl + 2 * d, depth);
            pm = med3func(input, pm - d, pm, pm + d, depth);
            pn = med3func(input, pn - 2 * d, pn - d, pn, depth);
          }
          pm = med3func(input, pl, pm, pn, depth);
          string pivot = input[pm];

          if (depth < pivot.Length)
          {
            char pivotChar = pivot[depth];
            while (st1 <= ed1 && 0 == (cmpByPivotChar(depth, input[st1], pivotChar)))
            {
              st1 = st1 + 1;
            }

            eqStart = st1 - 1;

            while (st1 <= ed1 && 0 == (cmpByPivotChar(depth, input[ed1], pivotChar)))
            {
              ed1 = ed1 - 1;
            }

            eqEnd = ed1 + 1;

            int cmpFlag = 0;
            bool flag = (st1 <= ed1);

            while (flag)
            {
              while (flag)
              {
                cmpFlag = cmpByPivotChar(depth, input[st1], pivotChar);
                if (cmpFlag < 0)
                {
                  st1 = st1 + 1;
                  flag = (st1 <= ed1);
                }
                else if (cmpFlag == 0)
                {
                  eqStart = eqStart + 1;
                  swap(input, st1, eqStart);
                  st1 = st1 + 1;
                }
                else
                  flag = false;
              }


              flag = (st1 <= ed1);
              while (flag)
              {
                cmpFlag = cmpByPivotChar(depth, input[ed1], pivotChar);
                if (cmpFlag > 0)
                {
                  ed1 = ed1 - 1;
                  flag = (st1 <= ed1);
                }
                else if (cmpFlag == 0)
                {
                  eqEnd = eqEnd - 1;
                  swap(input, ed1, eqEnd);
                  ed1 = ed1 - 1;
                }
                else
                  flag = false;
              }
              flag = (st1 <= ed1);
              if (flag)
              {
                swap(input, st1, ed1);
                st1 = st1 + 1;
                ed1 = ed1 - 1;
              }
            }
          }
          else
          {
            while (st1 <= ed1 && 0 == (cmpByPivotLen(depth, input[st1])))
            {
              st1 = st1 + 1;
            }
            eqStart = st1 - 1;

            while (st1 <= ed1 && 0 == (cmpByPivotLen(depth, input[ed1])))
            {
              ed1 = ed1 - 1;
            }
            eqEnd = ed1 + 1;

            int cmpFlag = 0;
            bool flag = (st1 <= ed1);

            while (flag)
            {
              while (flag)
              {
                cmpFlag = cmpByPivotLen(depth, input[st1]);
                if (cmpFlag < 0)
                {
                  st1 = st1 + 1;
                  flag = (st1 <= ed1);
                }
                else if (cmpFlag == 0)
                {
                  eqStart = eqStart + 1;
                  swap(input, st1, eqStart);
                  st1 = st1 + 1;
                }
                else
                  flag = false;
              }
              flag = (st1 <= ed1);
              while (flag)
              {
                cmpFlag = cmpByPivotLen(depth, input[ed1]);
                if (cmpFlag > 0)
                {
                  ed1 = ed1 - 1;
                  flag = (st1 <= ed1);
                }
                else if (cmpFlag == 0)
                {
                  eqEnd = eqEnd - 1;
                  swap(input, ed1, eqEnd);
                  ed1 = ed1 - 1;
                }
                else
                  flag = false;
              }
              flag = (st1 <= ed1);
              if (flag)
              {
                swap(input, st1, ed1);
                st1 = st1 + 1;
                ed1 = ed1 - 1;
              }
            }
          }
          int i = st;
          int j = st1 - 1;
          while (i <= eqStart)
          {
            swap(input, i, j);
            i = i + 1;
            j = j - 1;
          }

          i = ed - 1;
          j = st1;
          while (i >= eqEnd)
          {
            swap(input, i, j);
            i = i - 1;
            j = j + 1;
          }

          eqEnd = st1 + (ed - eqEnd);

          if (ed - eqEnd > 1)
          {
            InPlaceSort(input, depth, eqEnd, ed);
          }
          eqStart = st1 - (eqStart + 1 - st);
          //st..eqStart..eqEnd..ed
          if (eqEnd - eqStart > 1)
          {
            if (input[eqStart].Length > depth)
            {
              InPlaceSort(input, (depth + 1), eqStart, eqEnd);
            }
          }
          if (eqStart - st > 1)
          {
            InPlaceSort(input, depth, st, eqStart);
          }
        }
      }
    }

  }
}
