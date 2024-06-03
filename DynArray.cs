//реализация динамического массива

//АТД DynArray
/*
abstract class DynArray<T>

     //конструкторы

   //постусловие: создан пустой массив с минимальной длиной (16)
   public DynArray<T> ();

   //команды
   
   //постусловие: добавлен элемент в конец массива
   public void append (T item);

   //предусловие: корректный индекс вставки (попадает в рабочую область массива)
   //постусловие: в заданную позицию добавлен элемент, последующие смещены вправо
   public void insert (int i, T item);

   //предусловие: корректный индекс удаления (попадает в рабочую область массива)
   //постусловие: из заданной позиции удалён элемент
   public void remove (int i);

   //постусловие: из массива удалены все значения; задана минимальная длина (16)
   public void clear();

   //запросы

   //предусловие: корректный индекс запроса
   public T getitem (int i); //возвращает значение элемента по заданному индексу

   public int size(); //возвращает текущую длину массива

   //запросы статусов

   public int get_status_insert(); //не вызывался; успех; неудача - неккоректный индекс
   public int get_status_remove(); //не вызывался; успех; неудача - неккоректный индекс
   public int get_status_get();    //не вызывался; успех; неудача - неккоректный индекс

 *  */

using System;
using System.Collections.Generic;

namespace lab4
{
    class DynArray<T>
    {
        //хранимые значения 
        private int capacity; //ёмкость массива
        private int length;   //текущая длина массива
        private T[] space;    //массив для хранения элементов

        //статусы
        private int status_insert;
        private int status_remove;
        private int status_get;

        //интерфейс
        public const int INSERT_NIL = 0;
        public const int INSERT_OK = 1;
        public const int INSERT_ERR = 2;
        public const int REMOVE_NIL = 0;
        public const int REMOVE_OK = 1;
        public const int REMOVE_ERR = 2;
        public const int GET_NIL = 0;
        public const int GET_OK = 1;
        public const int GET_ERR = 2;

        //конструкторы
        public DynArray() { clear(); }

        //команды
        public void append (T item) //добавляет элемент в конец массива
        {
            if (isfull()) reallocation(true); 
            space[length] = item;
            length++;
        }

        public void insert (int i, T item) //добавляет в заданную позицию элемент
        {
            status_insert = INSERT_ERR;
            if (i >= 0 && i < length)
            {
                if (isfull()) reallocation(true);
                for (int j = length; j > i; j--) space[j] = space[j - 1];
                space[i] = item;
                length++;
                status_insert = INSERT_OK;
            }
        }

        //предусловие: корректный индекс удаления (попадает в рабочую область массива)
        //постусловие: из заданной позиции удалён элемент
        public void remove (int i)
        {
            status_remove = REMOVE_ERR;
            if (i >= 0 && i < length)
            {
                for (int j = i; j < length - 1; j++) space[j] = space[j + 1];
                if (isempty()) reallocation(false);
                status_remove = REMOVE_OK;
            }
        }

        public void clear () //очищает массив
        {
            capacity = 16;
            length = 0;
            space = new T[capacity];
            status_insert = INSERT_NIL;
            status_remove = REMOVE_NIL;
            status_get = GET_NIL;
        }

        private void reallocation (bool more) //изменяет размер буфера массива
        {
            int new_capacity;
            if (more) { new_capacity = capacity * 2; }
            else { new_capacity = (int)((float)capacity / 1.5); }

            T[] temp = new T[length];
            Array.Copy(space, temp, length);
            space = new T[new_capacity];
            Array.Copy(temp, space, length);
        }

        //запросы
        public T getitem (int i) //возвращает значение элемента по индексу
        {
            status_get = GET_ERR;
            if (i >= 0 && i < length)
            {
                status_get = GET_OK;
                return space[i];
            }
            return default(T);
        }

        public int size () //возвращает длину массива
        {
            return length;
        }

        private bool isfull() //возвращает true, если массив заполнен
        {
            if (length == capacity) return true;
            return false;
        }

        private bool isempty() //возвращает true, если больше половины массива пусто 
        {
            if (length < capacity / 2 && capacity != 16) return true;
            return false;
        }

        //запросы статусов

        public int get_status_insert() { return status_insert; } //не вызывался; успех; неудача - неккоректный индекс
        public int get_status_remove() { return status_remove; }//не вызывался; успех; неудача - неккоректный индекс
        public int get_status_get() { return status_get; }    //не вызывался; успех; неудача - неккоректный индекс
    }
}
