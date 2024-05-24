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
            Count+=elements.Length;
                        
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
        /// Сортировка любым алгоритмом (кроме пузырькового)
        /// </summary>
        public void Sort()
        {
            for (int i = 1; i < Count; i++  )
            {
                int j = default;
                int tmpValue = _array[i];
                for (j = i - 1; j >= 0; j--)
                {
                    if (_array[j] < tmpValue)
                    {
                        break;
                    }
                    _array[j + 1] = _array[j];
                }
                _array[j + 1] = tmpValue;
            }
        }

        /// <summary>
        /// Изменение размера массива
        /// </summary>
        private void Resize(int newSize)
        {
            int[] tmpList = new int[newSize];

            for (int i = 0; i < _array.Length; i++)
            {
                tmpList[i] = _array[i];
            }

            _array = tmpList;
        }
    }    
}

