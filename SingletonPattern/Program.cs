using System;

namespace SingletonPattern
{
    public class SingletonTester
    {
        public static bool IsSingleton(Func<object> func)
        {
            var obj1 = func.Invoke();
            var obj2 = func.Invoke();

            return obj1 == obj2;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
