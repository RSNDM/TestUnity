namespace QFramework
{
    using System;
    public class CustomObjectFactory<T>:IObjectFactory<T>
    {
        protected Func<T> mFactoryMehtod;
        public CustomObjectFactory(Func<T> factoryMethod)
        {
            mFactoryMehtod = factoryMethod;
        }
        public T Create()
        {
            return mFactoryMehtod();
        }
    }
    public class SimpleObjectPool<T>:Pool<T>
    {
        //不用显式声明一个委托
        //当你使用Action委托，则不需要显式定义一个委托封装的无参数的过程。
        //public delegate void mResetMethod()
        readonly Action<T> mResetMethod;

        public SimpleObjectPool(Func<T> factoryMethod,Action<T> resetMethod=null,int initCount=0)
        {
            factory = new CustomObjectFactory<T>(factoryMethod);
            mResetMethod = resetMethod;
            for (int i = 0; i < initCount; i++)
            {
                mCacheStack.Push(factory.Create());
            }
        }
        public override bool Recycle(T obj)
        {
            //mResetMethod.
            return false;
        }
    }
    
}
