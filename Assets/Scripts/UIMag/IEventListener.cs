using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



    public interface IEventListener
    {
        /// <summary>
        /// 处理消息
        /// </summary>
        /// <param name="eventkey">消息的key</param>
        /// <param name="objArgs">参数集</param>
        /// <returns>是否终止消息派发</returns>
        bool HandleEvent(int eventkey, params object[] objArgs);

        /// <summary>
        /// 消息的优先级
        /// </summary>
        /// <returns>优先级</returns>
        int EventPriority();
    }

