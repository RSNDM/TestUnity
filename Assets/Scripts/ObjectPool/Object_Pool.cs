using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Pool<T> where T:class ,new()  {
    private Stack<T> m_objectStack = new Stack<T>();
    public T New()
    {
        return (m_objectStack.Count == 0) ? new T() : m_objectStack.Pop();
    }
    public void Store(T t)
    {
        m_objectStack.Push(t);
    }
}
