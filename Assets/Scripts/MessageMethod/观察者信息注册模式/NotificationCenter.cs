using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace UniEventDispatcher
{
    /// <summary>
    /// 定义事件分发委托
    /// </summary>
    /// <param name="notific"></param>
    public delegate void OnNotification(Notification notific);

    /// <summary>
    /// 通知中心
    /// </summary>
    public class NotificationCenter
    {
        /// <summary>
        /// 通知中心单例
        /// </summary>
        private static NotificationCenter instance = null;
        public static NotificationCenter Get()
        {
            if (instance==null)
            {
                instance = new NotificationCenter();
                return instance;
            }
            return instance;
        }
        /// <summary>
        /// 储存事件的字典
        /// </summary>
        private Dictionary<string, OnNotification> eventListners = new Dictionary<string, OnNotification>();

        /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="eventKey">事件key</param>
        /// <param name="eventListener">事件监听器</param>
        public void AddListener(string eventKey,OnNotification eventListener)
        {
            if (!eventListners.ContainsKey(eventKey))
            {
                eventListners.Add(eventKey, eventListener);
            }
        }

        /// <summary>
        /// 移除事件
        /// </summary>
        /// <param name="eventKey">事件key</param>
        public void RemoveEventListener(string eventKey)
        {
            if (!eventListners.ContainsKey(eventKey))
            {
                return;
            }
            eventListners[eventKey] = null;
        }
        /// <summary>
        /// 分发事件
        /// </summary>
        /// <param name="eventKey">事件key</param>
        /// <param name="sender">发送者</param>
        /// <param name="param">通知内容</param>
        public void DispatchEvent(string eventKey,GameObject sender,object param)
        {
            if (!eventListners.ContainsKey(eventKey))
                return;
            eventListners[eventKey](new Notification(sender, param));
        }

        /// <summary>
        /// 分发事件
        /// </summary>
        /// <param name="eventKey">事件key</param>
        /// <param name="param">通知内容</param>
        public void DispatchEvent(string eventKey,  object param)
        {
            if (!eventListners.ContainsKey(eventKey))
                return;
            eventListners[eventKey](new Notification( param));
        }

        public bool HasEventListener(string eventKey)
        {
            return eventListners.ContainsKey(eventKey);
        }
    }
}
