using Akka.Actor;

namespace Akka.Report
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var system = ActorSystem.Create("Test"))
            {
                var manager = system.ActorOf<Manager>();
            }
        }
    }
}
