namespace QFramework
{
    using System.Collections.Generic;
    public interface IPool<T>
    {
        T Allocate();
        bool Recycle(T obj);
    }
    public interface ICountObserveAble
    {
        int CurCount { get; }
    }
    public interface IObjectFactory<T>
    {
        T Create();
    }
    public abstract class Pool<T>:IPool<T>,ICountObserveAble
    {
        #region ICountObserverable
        /// <summary>
        /// gets the current count
        /// </summary>
        /// <value>the current count.</value>
        public int CurCount
        {
            get { return mCacheStack.Count; }
        }
        #endregion
        protected readonly Stack<T> mCacheStack = new Stack<T>();
        protected IObjectFactory<T> factory;
        /// <summary>
        /// default is 5
        /// </summary>
        protected int mMaxCount = 12;
        public virtual T Allocate()
        {
            return mCacheStack.Count == 0 ? factory.Create() : mCacheStack.Pop();
        }
        public abstract bool Recycle(T obj);
    }
}
