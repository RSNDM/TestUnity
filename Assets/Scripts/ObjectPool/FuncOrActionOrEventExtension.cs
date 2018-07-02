namespace QFramework
{
    using System;
    using UnityEngine;

    public static class FuncOrActionOrEventExtension
    {
        private delegate void TestDelegate();

        public static T InvokeGracefully<T> (this Func<T> selefFunc)
        {
            return null != selefFunc ? selefFunc() : default (T);
        }
        public static bool InvokeGracefully(this System.Action selfAction)
        {
            if (null != selfAction)
            {
                selfAction();
                return true;
            }
            return false;
        }

    }
}
