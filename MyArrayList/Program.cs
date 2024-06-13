using System.Collections;
using MyArrayList.Library;

/**/int[] arr = new int[] { 32, 120, 45 };
CustomArrayList<int> arrayList = new CustomArrayList<int>(arr);
int[] arr1 = new int[] { 102, 212, 952 };
int[] arr2 = new int[] { -702, -422, -862 };

double[] arr3 = new double[] { 0.1, 2.3, 15.7, 1.2 };

CustomArrayList<double> dblList = new CustomArrayList<double>(arr3);
//dblList.RadixSort();
dblList.Sort();

var x = arrayList[arrayList.Count - 1];
arrayList[arrayList.Count-2] = 455;
arrayList.Add(arr2);
arrayList.Add(arr1, 5);
//arrayList.RadixSort();

arrayList.RadixSort<int>();

var sum = arrayList[arrayList.Count - 2] - arrayList[3];
//arrayList.Sort();

CustomArrayList<string> strList = new CustomArrayList<string>(new string[] { "папа", "Дочь", "сын", "брат", "мама", "баба" });

var fam = strList[1] + strList[3];
strList.Sort();
//strList.RadixSort();
//strList.RadixSort<string>();

foreach (var item in arrayList)
{
    Console.WriteLine(item);
}

Console.WriteLine();

foreach (var item in strList)
{
    Console.WriteLine(item);
}


strList.Clear();
arrayList.Clear();

/*
List<string> list = new List<string>{ "папаjhk", "Дочь", "сын", "братyt", "мамаf", "бабаuyi" };

list.Sort();


CustomArrayList<string> strList = new CustomArrayList<string>(new string[] { "папа", "Дочь", "сын", "брат", "мама", "баба" });
strList.Sort();
*/

foreach (var item in strList)
{
    Console.WriteLine(item);
}
Console.ReadLine();



