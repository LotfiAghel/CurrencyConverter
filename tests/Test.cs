using System.Runtime.CompilerServices;

class Test1:ITest
{
    public void run(){
        var convertor=new FloydWarshallCurrencyConverter();
        convertor.ClearConfiguration();
        convertor.UpdateConfiguration(new List<Tuple<string, string, double>>(){
            //new Tuple<string, string, double>("USD","CAD",1.34),
            new Tuple<string, string, double>("CAD","GBP",0.58),
            new Tuple<string, string, double>("USD","EUR",0.86),
        });

        Console.WriteLine(convertor.Convert("USD","CAD",1));
        Console.WriteLine(convertor.Convert("CAD","USD",1));
        Console.WriteLine(convertor.Convert("EUR","USD",1));

        Console.WriteLine($"{convertor.Convert("EUR","GBP",1)} == {1.0/0.86 * 1.34 * 0.58}");


    }
}