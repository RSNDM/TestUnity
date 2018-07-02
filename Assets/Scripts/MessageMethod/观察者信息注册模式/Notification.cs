using System;
using UnityEngine;

namespace UniEventDispatcher
{
    public class Notification
    {
        ///<summary>
        ///通知发生者
        ///</summary>
        public GameObject sender;

        /// <summary>
        /// 通知内容
        /// 备注：在发送消息时需要装箱 ，解析消息时需要拆箱
        /// 注意
        /// </summary>
        public object param;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sender">通知发送者</param>
        /// <param name="param">通知内容</param>
        public Notification(GameObject sender,object param)
        {
            this.sender = sender;
            this.param = param;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="param"></param>
        public Notification(object param)
        {
            this.sender = null;
            this.param = param;
        }

        public override string ToString()
        {
            return string.Format("sender={0},param={1}", this.sender, this.param);
        }
    }
}
