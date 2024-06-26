﻿using System.Collections;
using System.Diagnostics.Metrics;
using System.Numerics;
using MyArrayList.Library;

namespace MyArrayList.Library
{
    public class CustomArrayList<T> where T : IComparable
    {
        #region Поля
        private T[] _array;
        private int _length;
        #endregion

        #region Свойства
        /// <summary>
        /// Длина списка
        /// </summary>
        public int Count { get; private set; }
        #endregion

        #region Конструкторы
        public CustomArrayList()
        {
            _array = new T[4];
            _length = _array.Length;
        }

        public CustomArrayList(T element)
        {
            _array = new T[4];
            _array[0] = element;

            _length = _array.Length;
            Count++;
        }

        public CustomArrayList(T[] elements)
        {
            Count = elements.Length;
            _array = new T[elements.Length];
            _array = elements;
            _length = (int)(_array.Length * 1.5);

            Resize(_length);
        }
        #endregion

        #region Индексатор
        public T this[int index]
        {
            get
            {
                if (index < 0 || index > Count - 1)
                {
                    throw new IndexOutOfRangeException();
                }

                return _array[index];
            }
            set
            {
                if (index < 0 || index > Count - 1)
                {
                    throw new IndexOutOfRangeException();
                }

                _array[index] = value;
            }
        }
        #endregion

        #region Добавление элементов
        /// <summary>
        /// Добавление одного элемента
        /// </summary>
        public void Add(T element)
        {
            _array[Count] = element;
            Count++;

            if (_array.Length == Count)
            {
                _length = (int)(_array.Length * 1.5);
                Resize(_length);
            }
        }

        /// <summary>
        /// Добавление массива элементов
        /// </summary>
        public void Add(T[] elements)
        {
            var elementsLength = Count + elements.Length;
            var oldArrayCount = Count;
            _length = (int)((_array.Length + elements.Length) * 1.5);

            Resize(_length);
            for (int i = Count; i < elementsLength; i++)
            {
                _array[i] = elements[i - oldArrayCount];
                Count++;
            }
        }

        /// <summary>
        /// Добавление элемента по индексу
        /// </summary>
        public void Add(T element, int index)
        {
            Count++;

            T[] tmpArray = new T[_array.Length];
            tmpArray[index] = element;

            for (int i = 0; i < index; i++)
            {
                tmpArray[i] = _array[i];
            }

            for (int i = index + 1; i < Count; i++)
            {
                tmpArray[i] = _array[i - 1];
            }

            _array = tmpArray;

            if (_array.Length == Count)
            {
                _length = (int)(_array.Length * 1.5);
                Resize(_length);
            }
        }

        /// <summary>
        /// Добавление массива элементов по индексу
        /// </summary>
        public void Add(T[] elements, int index)
        {
            Count += elements.Length;

            if (_array.Length < Count)
            {
                _length = (int)((_array.Length + elements.Length) * 1.5);
                Resize(_length);
            }

            T[] tmpArray = new T[_array.Length];
            int indexElement = default;

            for (int i = index; i < elements.Length + index; i++)
            {
                tmpArray[i] = elements[indexElement];
                indexElement++;
            }

            for (int i = 0; i < index; i++)
            {
                tmpArray[i] = _array[i];
            }

            for (int i = index + elements.Length; i < Count; i++)
            {
                tmpArray[i] = _array[i - elements.Length];
            }

            _array = tmpArray;
        }
        #endregion

        /// <summary>
        /// Сортировка выбором. Сложность: худшая - О((n^2)/2), средняя - O((n^2)/4)
        /// </summary>
        public void Sort()
        {
            for (int i = 1; i < Count; i++)
            {
                int j = default;
                T tmpValue = _array[i];

                for (j = i - 1; j >= 0; j--)
                {
                    if (_array[j].CompareTo(tmpValue) < 0)
                    {
                        break;
                    }

                    _array[j + 1] = _array[j];
                }

                _array[j + 1] = tmpValue;
            }
        }

        public void RadixSort<T>() where T : struct
        {
            //CustomArrayList<int> array = new CustomArrayList<int>((int[])(object)_array);
            //var array = (int[])(object)_array;

            Sorting.SortRadix((int[])(object)_array, Count);
        }

        /// <summary>
        /// Очистка списка
        /// </summary>
        public void Clear()
        {
            _length = 4;
            Count = 0;
            _array = new T[_length];
        }

        /// <summary>
        /// Изменение размера массива
        /// </summary>
        private void Resize(int newSize)
        {
            T[] tmpList = new T[newSize];

            for (int i = 0; i < Count; i++)
            {
                tmpList[i] = _array[i];
            }

            _array = tmpList;
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            return new CustomArrayListEnumerator<T>(_array, Count);
        }
    }

    public class CustomArrayListEnumerator<T> : IEnumerator<T>
    {
        private T[] _array;
        private int _count;
        private int _position = -1;

        public CustomArrayListEnumerator(T[] array, int count)
        {
            _array = array;
            _count = count;
        }

        public T Current
        {
            get
            {
                if (_position == -1 || _position >= _count)
                {
                    throw new ArgumentException();
                }

                return _array[_position];
            }
        }

        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            if (_position < _count - 1)
            {
                _position++;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Reset()
        {
            _position = -1;
        }

        public void Dispose()
        {
            // TODO release managed resources here
        }
    }
}

interface IRadixSort
{
    public void RadixSort<T>();
}

static class Sorting
{
    /// <summary>
    /// Поиск максимального разряда
    /// </summary>
    /// <typeparam name="CustomArrayList<int>">CustomArrayList</typeparam>
    /// <returns>Число разрядов</returns>
    private static int MaxRadixCount(int[] array, int count)
    {
        var result = 0;
        for (int i = 0; i < count; i++)
        {
            var radix = 0;
            var tmpVal = array[i];
            
            while (tmpVal > 0)
            {
                radix++;
                tmpVal /= 10;
            }

            result = result > radix ? result : radix;
        }

        return result;
    }

    /// <summary>
    /// Сортировка чисел поразрядная LSD. Сложность всегда O(n*logn)
    /// </summary>
    public static void SortRadix(int[] array, int count)
    {
        CustomArrayList<int>[] lists = new CustomArrayList<int>[10];

        for (int i = 0; i < 10; i++)
        {
            lists[i] = new CustomArrayList<int>();
        }

        var radix = MaxRadixCount(array, count);
        
        if (radix == 0)
        {
            return;
        }

        for(int i = 0; i < radix; i++)
        {
            //Распределяем значения массива по спискам
            for(int j = 0; j < count; j++)
            {
                int tempIndex = (array[j] % (int)Math.Pow(10, i + 1)) / (int)Math.Pow(10, i);
                if (tempIndex < 0)
                {
                    lists[0].Add(array[j]);
                    var tmpInd = lists[0].Count-1;
                    tmpInd = tmpInd < 0 ? 0 : tmpInd;

                    while (tmpInd > 0 && array[j] <= lists[0][tmpInd-1])
                    {
                        lists[0][tmpInd] = lists[0][tmpInd-1];
                        lists[0][tmpInd-1] = array[j];
                        tmpInd--;
                    }
                }
                else
                {
                    lists[tempIndex].Add(array[j]);

                }
            }

            //Собираем отсортированные значения в массив
            int index = 0;

            for(int j = 0; j < 10; j++)
            {
                for(int k = 0; k < lists[j].Count; k++)
                {
                    array[index++] = lists[j][k];
                }
            }

            //Очищаем списки для последующего заполнения
            for (int j = 0; j < 10; j++)
            {
                lists[j].Clear();
            }
        }
    }
}