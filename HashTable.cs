//реализация хэш-таблицы

//АТД HashTable
/*
 abstract class HashTable<T>

   //конструкторы

   //постусловие: создана пустая хэш-таблица
   public HashTable<T> (int size);

   //команды

   //постусловие: в таблицу добавлен элемент
   public void add (T value);

   //постусловие: из таблицы удалён элемент
   public void remove (T value);

   //запросы

   public bool is_member (T value); //true, если элемент содержится в таблице; false, если нет

*/

using System;
using System.Collections.Generic;

namespace lab7
{
    class HashTable<T>
    {
        //хранимые значения
        private int length; //изначальная длина таблицы
        private int length_new; //текущая длина таблицы
        private T[] space; //пространство для хранения 

        //конструкторы
        public HashTable (int size)
        {
            length = size;
            length_new = size;
            space = new T[length];
        }

        //команды

        //добавляет элемент в таблицу
        public void add(T value)
        {
            bool flag = false;
            int index = hash(value);
            for (int i = index; i < length_new; i += length)
            {
                if (space[index].ToString() == default(T).ToString())
                {
                    space[index] = value;
                    flag = true;
                    break;
                } 
            }
            if (!flag)
            {
                reallocation();
                space[length_new - length + index] = value;
            }

        }

        //удаляет элемент из таблицы
        public void remove(T value)
        {
            int index = hash(value);
            for (int i = index; i < length_new; i += length)
            {
                if (space[i].ToString() == value.ToString())
                {
                    space[i] = default(T);
                    break;
                }
            }
        }

        //возвращает индекс элемента
        private int hash(T value)
        {
            string temp = value.ToString();
            int sum = 0;
            for (int i = 0; i < temp.Length; i++)
            {
                sum += temp[i];
            }
            return sum % length;
        }

        //увеличивает размер буфера
        private void reallocation()
        {
            length_new += length;
            T[] temp = new T[length];
            Array.Copy(space, temp, length);

            space = new T[length_new];
            Array.Copy(temp, space, length);
        }

        //запросы

        //true, если элемент содержится в таблице; false, если нет
        public bool is_member(T value)
        {
            int index = hash(value);
            for (int i = index; i < length_new; i += length)
            {
                if (space[i].ToString() == value.ToString()) return true;
            }
            return false;
        } 
    }
}
