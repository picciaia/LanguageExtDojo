using static System.Console;
using static Factories;

class Chapter01
{
    public void Run()
    {
        (Func<int> zero, Func<int, int, int> add) monoid = (zero: () => 0, add: (x, y) => x + y);
        monoid.zero();

        Func<(Order, double discount), (Order updatedOrder, double totalPrice)> updateOrder = state => { return (new Order(1), 0.1); };
        Func<(Order, double totalPrice), (double totalPrice, string errors)> validate = state => { return (0.1, "noerror"); };

        Func<(Order, double discount), (double totalPrice, string errors)> proccess =
            order => validate(updateOrder(order));

        var ret = (1, 2).Map(x => x * x);

        WriteLine(ret);
        var t = IO(() => 1);

        string F(int x, int y) => (x + y).ToString();

        Func<int, string> F1(int x)
        {
            string g(int y) { return (x + y).ToString(); }
            return g;
        }

        var s = F(3, 4);
        var s1 = F1(3)(4);

        Func<int, Func<int, string>> F1b = x => y => { return (x + y).ToString(); };
        var s1b = F1b(3)(4);

        Func<double, double, double> discountedPrice = (discount, price) =>
            price - discount * price;

        Func<double, Func<double, double>> discountedPriceCurried = discount => price =>
            price - discount * price;

        var discountedPrices = new List<double> { 10, 20, 30 }.Select(x => discountedPrice(0.1, x));
        var discountedPrices1 = new List<double> { 10, 20, 30 }.Select(discountedPriceCurried(0.1));

        Func<double, double> discountedPricePartial = x => discountedPrice(0.1, x);

        static Func<T, Func<T, T>> Curry<T> (Func<T, T, T> f) => x => y => f(x, y);

        var c = Curry(discountedPrice)(0.1);
    }
}


class IO<T>
{
    public Func<T> Fn { get; set; }
    public IO(Func<T> fn) => Fn = fn;
    public T Run() => Fn();
}

class Factories
{
    public static IO<T> IO<T>(Func<T> fn) => new IO<T>(fn);
}

static class FunctionalExt
{
    public static ValueTuple<T1, T1> Map<T, T1>(this ValueTuple<T, T> @this, Func<T, T1> f) =>
        new ValueTuple<T1, T1>(f(@this.Item1), f(@this.Item2));
}



