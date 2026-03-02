using System;
using System.Collections;
using System.Collections.Generic;

namespace IPC2_Proyecto1_2020XXXX.Estructuras
{
    public class ListaEnlazada<T> : IEnumerable<T>
    {
        private Nodo<T> head;
        private Nodo<T> tail;
        private int count;

        public int Count => count;

        public ListaEnlazada()
        {
            head = tail = null;
            count = 0;
        }

        public void AddLast(T value)
        {
            var nodo = new Nodo<T>(value);
            if (tail == null)
            {
                head = tail = nodo;
            }
            else
            {
                tail.Siguiente = nodo;
                tail = nodo;
            }
            count++;
        }

        public void AddFirst(T value)
        {
            var nodo = new Nodo<T>(value);
            if (head == null)
            {
                head = tail = nodo;
            }
            else
            {
                nodo.Siguiente = head;
                head = nodo;
            }
            count++;
        }

        public bool Remove(T value)
        {
            Nodo<T> current = head;
            Nodo<T> prev = null;
            while (current != null)
            {
                if (EqualityComparer<T>.Default.Equals(current.Valor, value))
                {
                    if (prev == null)
                        head = current.Siguiente;
                    else
                        prev.Siguiente = current.Siguiente;

                    if (current == tail)
                        tail = prev;

                    count--;
                    return true;
                }
                prev = current;
                current = current.Siguiente;
            }
            return false;
        }

        public T Find(Func<T, bool> predicate)
        {
            foreach (var v in this)
            {
                if (predicate(v)) return v;
            }
            return default(T);
        }

        public int IndexOf(T value)
        {
            int idx = 0;
            foreach (var v in this)
            {
                if (EqualityComparer<T>.Default.Equals(v, value)) return idx;
                idx++;
            }
            return -1;
        }

        public bool Contains(T value) => IndexOf(value) != -1;

        public IEnumerator<T> GetEnumerator()
        {
            Nodo<T> current = head;
            while (current != null)
            {
                yield return current.Valor;
                current = current.Siguiente;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public List<T> ToList()
        {
            var list = new List<T>();
            foreach (var v in this)
                list.Add(v);
            return list;
        }
    }
}
