using MyNet6.Interfaces;

namespace MyNet6.Services
{
    public class TestServiceA : ITestServiceA
    {
        public string ShowA()
        {
            return $"this is form {GetType().FullName} showA";
        }

        public  TestServiceA()
        {
            Console.WriteLine($"{GetType().Name} 被构造");
        }
    }
}