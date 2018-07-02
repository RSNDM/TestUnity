namespace QFramework.Example
{
    using UnityEngine;
    internal class ClassQSingletonExample:Singleton<ClassQSingletonExample>
    {
        private static int mIndex = 0;
        private ClassQSingletonExample() { }
        public override void OnSingletonInit()
        {
            mIndex++;
        }
        public void Log(string content)
        {
            Debug.Log("ClassQSingleton" + mIndex + ":" + content);
        }
    }

    public class QSingletonExample:MonoBehaviour
    {
        private void Start()
        {
            ClassQSingletonExample.Instance.Log("Hello World");

            ClassQSingletonExample.Instance.Dispose();

            ClassQSingletonExample.Instance.Log("Hello World");

        }
    }
}
