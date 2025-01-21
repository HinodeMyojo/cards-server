namespace CardsServer.BLL.Test.Strategy
{
    public interface IStrategy
    {
        object DoAlgorithm(object data);
    }

    // Конкретные Стратегии реализуют алгоритм, следуя базовому интерфейсу
    // Стратегии. Этот интерфейс делает их взаимозаменяемыми в Контексте.
    class ConcreteStrategyA : IStrategy
    {
        public object DoAlgorithm(object data)
        {
            var list = data as List<string>;
            list.Sort();

            return list;
        }
    }

    class ConcreteStrategyB : IStrategy
    {
        public object DoAlgorithm(object data)
        {
            var list = data as List<string>;
            list.Sort();
            list.Reverse();

            return list;
        }
    }

    public class Context
    {
        private IStrategy _strategy;

        public Context()
        {
        }

        public Context(IStrategy strategy)
        {
            _strategy = strategy;
        }

        public void SetStrategy(IStrategy strategy)
        {
            _strategy = strategy;
        }

        // Вместо того, чтобы самостоятельно реализовывать множественные версии
        // алгоритма, Контекст делегирует некоторую работу объекту Стратегии.
        public void DoSomeBusinessLogic()
        {
            Console.WriteLine("Context: Sorting data using the strategy (not sure how it'll do it)");
            var result = _strategy.DoAlgorithm(new List<string> { "a", "b", "c", "d", "e" });

            string resultStr = string.Empty;
            foreach (var element in result as List<string>)
            {
                resultStr += element + ",";
            }

            Console.WriteLine(resultStr);
        }
    }

    //static void Main(string[] args)
    //{
    //    // Клиентский код выбирает конкретную стратегию и передаёт её в
    //    // контекст. Клиент должен знать о различиях между стратегиями,
    //    // чтобы сделать правильный выбор.
    //    var context = new Context();

    //    Console.WriteLine("Client: Strategy is set to normal sorting.");
    //    context.SetStrategy(new ConcreteStrategyA());
    //    context.DoSomeBusinessLogic();

    //    Console.WriteLine();

    //    Console.WriteLine("Client: Strategy is set to reverse sorting.");
    //    context.SetStrategy(new ConcreteStrategyB());
    //    context.DoSomeBusinessLogic();
    //}
}
