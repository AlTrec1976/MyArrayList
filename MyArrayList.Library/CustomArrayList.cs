using System.Collections;

namespace MyArrayList.Library
{
    public class CustomArrayList
    {
        private int[] _array;
        private int _length;

        public int Count { get; private set; }

        public CustomArrayList()
        {
            _array = new int[4];
            _length = _array.Length;
        }

        public CustomArrayList(int element)
        {
            _array = new int[4];
            _array[0] = element;

            _length = _array.Length;
            Count++;
        }

        public CustomArrayList(int[] elements)
        {
            Count = elements.Length;
            _array = new int[elements.Length];
            _array = elements;
            _length = (int)(_array.Length * 1.5);

            Resize(_length);
        }

        /// <summary>
        /// Добавление одного элемента
        /// </summary>
        public void Add(int element)
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
        public void Add(int[] elements)
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
        public void Add(int element, int index)
        {
            Count++;

            int[] tmpArray = new int[_array.Length];
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
        public void Add(int[] elements, int index)
        {
            Count += elements.Length;

            if (_array.Length < Count)
            {
                _length = (int)((_array.Length + elements.Length) * 1.5);
                Resize(_length);
            }

            int[] tmpArray = new int[_array.Length];
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

        /// <summary>
        /// Сортировка выбором. Сложность: худшая - О((n^2)/2), средняя - O((n^2)/4)
        /// </summary>
        public void Sort()
        {
            for (int i = 1; i < Count; i++)
            {
                int j = default;
                int tmpValue = _array[i];

                for (j = i - 1; j >= 0; j--)
                {
                    if (_array[j] <= tmpValue)
                    {
                        break;
                    }

                    _array[j + 1] = _array[j];
                }

                _array[j + 1] = tmpValue;
            }
        }
        
        private int MaxRadixCount()
        {
            var result = 0;

            foreach (var item in _array)
            {
                var radix = 0;
                var number = item;
                while (number > 0)
                {
                    radix++;
                    number /= 10;
                }

                result = result > radix ? result : radix;
            }
            
            return result;
        }
        
        /// <summary>
        /// Сортировка поразрядная LSD. Сложность всегда O(n*logn)
        /// </summary>
        public void RadixSort() 
        {
            ArrayList[] lists = new ArrayList[10];
            
            for (int i = 0; i < 10; i++)
            {
                lists[i] = new ArrayList();
            }

            var radix = MaxRadixCount();
            
            for(int i = 0; i < radix; i++) 
            {
                //Распределяем значения массива по спискам
                for(int j = 0; j < Count; j++)
                {
                    int tempIndex = (_array[j] % (int)Math.Pow(10, i + 1)) / (int)Math.Pow(10, i);
                    int tempVar = default;
                    if (tempIndex < 0)
                    {
                        var tmpInd = lists[0].Count-1;
                        tmpInd = tmpInd < 0 ? 0 : tmpInd;
                        lists[0].Add(_array[j]);
                        if (tmpInd > 0 && _array[j] < lists[0][tmpInd-1].GetHashCode())
                        {
                            tempVar = lists[0][tmpInd].GetHashCode();
                            lists[0][tmpInd + 1] = tempVar;
                            lists[0][tmpInd] = _array[j];
                        }
                        
                    }
                    else
                    {
                        lists[tempIndex].Add(_array[j]);
                        
                    }
                }
                
                //Собираем отсортированные значения в массив
                int index = 0;
                
                for(int j = 0; j < 10; j++) 
                {
                    for(int k = 0; k < lists[j].Count; k++) 
                    {
                        _array[index++] = (int)lists[j][k];
                    }
                }

                //Очищаем списки для последующего заполнения
                for (int j = 0; j < 10; j++)
                {
                    lists[j].Clear();
                }
            }
        }
        
        /// <summary>
        /// Изменение размера массива
        /// </summary>
        private void Resize(int newSize)
        {
            int[] tmpList = new int[newSize];

            for (int i = 0; i < Count; i++)
            {
                tmpList[i] = _array[i];
            }

            _array = tmpList;
        }
    }
}
