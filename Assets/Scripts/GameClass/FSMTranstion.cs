
namespace QFramework
{
    using System.Collections.Generic;

    /// <summary>
    /// 基础状态类
    /// 包含了状态名 状态切换的逻辑
    /// 跳转字典
    /// </summary>
	public class QFSMState
    {
        public QFSMState(ushort stateName)
        {
            Name = stateName;
        }

        public ushort Name; // 字符串

        public virtual void OnEnter()
        {
        } // 进入状态(逻辑)

        public virtual void OnExit()
        {
        } // 离开状态(逻辑)

        /// <summary>
        /// translation for name
        /// </summary>
        public Dictionary<ushort, QFSMTranslation> TranslationDict = new Dictionary<ushort, QFSMTranslation>();
    }

    /// <summary>
    /// 跳转类
    /// 跳转类包含了跳转信息（从哪里来 ，要去哪，状态名）
    /// </summary>
    public class QFSMTranslation
    {
        public QFSMState FromState;
        public ushort EventName;
        public QFSMState ToState;

        public QFSMTranslation(QFSMState fromState, ushort eventName, QFSMState toState)
        {
            FromState = fromState;
            ToState = toState;
            EventName = eventName;
        }
    }

    /// <summary>
    /// 状态机
    /// 进入状态
    /// 增加状态
    /// 增加跳转（两种方法）
    /// 清楚状态机字典
    /// </summary>
	public class QFSM
    {
        private QFSMState mCurState;

        public QFSMState State
        {
            get { return mCurState; }
        }

        /// <summary>
        /// The m state dict.
        /// </summary>
        private readonly Dictionary<ushort, QFSMState> mStateDict = new Dictionary<ushort, QFSMState>();

        /// <summary>
        /// Adds the state.
        /// </summary>
        /// <param name="state">State.</param>
        public void AddState(QFSMState state)
        {
            mStateDict.Add(state.Name, state);
        }


        /// <summary>
        /// Adds the translation.
        /// </summary>
        /// <param name="translation">Translation.</param>
        public void AddTranslation(QFSMTranslation translation)
        {
            mStateDict[translation.FromState.Name].TranslationDict.Add(translation.EventName, translation);
        }


        /// <summary>
        /// Adds the translation.
        /// </summary>
        /// <param name="fromState">From state.</param>
        /// <param name="eventName">Event name.</param>
        /// <param name="toState">To state.</param>
        public void AddTranslation(QFSMState fromState, ushort eventName, QFSMState toState)
        {
            mStateDict[fromState.Name].TranslationDict.Add(eventName, new QFSMTranslation(fromState, eventName, toState));
        }

        /// <summary>
        /// Start the specified startState.
        /// </summary>
        /// <param name="startState">Start state.</param>
        public void Start(QFSMState startState)
        {
            mCurState = startState;
            mCurState.OnEnter();
        }

        /// <summary>
        /// Handles the event.
        /// </summary>
        /// <param name="eventName">Event name.</param>
        public void HandleEvent(ushort eventName)
        {
            if (mCurState != null && mStateDict[mCurState.Name].TranslationDict.ContainsKey(eventName))
            {
                //状态转移，从状态字典中当前状态的转移状态从来时状态中退出
                //状态转移的State换做当前状态，执行进入的OnEnter函数
                var tempTranslation = mStateDict[mCurState.Name].TranslationDict[eventName];
                tempTranslation.FromState.OnExit();
                mCurState = tempTranslation.ToState;
                tempTranslation.ToState.OnEnter();
            }
        }

        /// <summary>
        /// Clear this instance.
        /// </summary>
        public void Clear()
        {
            mStateDict.Clear();
        }
    }
}
