﻿using System.Collections;
using MyArrayList.Library;

int[] arr = new int[] { 32, 20, 45 };
CustomArrayList arrayList = new CustomArrayList(arr);
int[] arr1 = new int[] { 102, 212, 952 };

arrayList.Add(24);
arrayList.Add(20);
arrayList.Add(14);
arrayList.Add(11);
arrayList.Add(arr1);
arrayList.Add(223);
arrayList.Add(6);
arrayList.Add(7);
arrayList.Add(5);
arrayList.Add(10);
arrayList.Add(3);
arrayList.Add(15);
arrayList.Add(19);
arrayList.Add(13);
arrayList.Add(445);
arrayList.Add(23);
arrayList.Add(4);
arrayList.Add(12);
arrayList.Add(8);
arrayList.Add(25);
arrayList.Add(2);
arrayList.Add(9);
arrayList.Add(arr1, 20);
arrayList.Add(1);
arrayList.Add(777, 10);
arrayList.Add(16);
arrayList.Add(17);
arrayList.Add(18);
arrayList.Add(21);
arrayList.Add(22);
arrayList.Add(26);
arrayList.Sort();
Console.ReadLine();


