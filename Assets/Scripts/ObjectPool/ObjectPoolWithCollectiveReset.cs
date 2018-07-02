﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectPoolWithCollectiveReset<T> where T:class,new()  {
    private List<T> m_objectList;
    private int m_nextAvailableIndex = 0;

    private Action<T> m_resetAction;
    private Action<T> m_onetimeInitAction;

    public ObjectPoolWithCollectiveReset(int initialBufferSize,Action<T> ResetAction=null,Action<T> OnetimeInitAction=null)
    {
        m_objectList = new List<T>(initialBufferSize);
        m_resetAction = ResetAction;
        m_onetimeInitAction = OnetimeInitAction;
    }

    public T New()
    {
        if (m_nextAvailableIndex<m_objectList.Count)
        {
            T t = m_objectList[m_nextAvailableIndex];
            m_nextAvailableIndex++;

            if (m_resetAction!=null)
            {
                m_resetAction(t);
            }
            return t;
        }
        else
        {
            T t = new T();
            m_objectList.Add(t);
            m_nextAvailableIndex++;

            if (m_onetimeInitAction!=null)
            {
                m_onetimeInitAction(t);
            }

            return t;
        }
    }

    public void ResetAll()
    {
        //重置索引
        m_nextAvailableIndex = 0;
    }
}
