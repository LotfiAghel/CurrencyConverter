
/// <summary>
/// this is struct and so . operator must have not run time overhead and Helpfull for AOT Optimizer 
/// UpdateConfiguration
/// </summary>
struct ThreadSafeWithoutLockConvertor<T>:ICurrencyConverter where T: ICurrencyConverter ,new()
{

    private T convertor;

    public void ClearConfiguration(){
        if(convertor!=null)
            convertor.ClearConfiguration();
    }
    
    public void UpdateConfiguration(IEnumerable<Tuple<string, string, double>> conversionRates){
        T newConvertor = new T();
        newConvertor.UpdateConfiguration(conversionRates);


        convertor=newConvertor;

    }

    public double Convert(string fromCurrency, string toCurrency, double amount){
        return convertor.Convert(fromCurrency,toCurrency,amount);
    }

}


struct ThreadSafeReadOnlyConvertor<T>:ICurrencyConverter where T: ICurrencyConverter ,new()
{

    private readonly T convertor;



    ThreadSafeReadOnlyConvertor(IEnumerable<Tuple<string, string, double>> conversionRates){
        T newConvertor = new T();
        newConvertor.UpdateConfiguration(conversionRates);
        convertor=newConvertor;
    }
    public void ClearConfiguration(){
        throw new Exception("dont allow");// But I know this is not true thats not checkable on compile time 
    }


    
    public void UpdateConfiguration(IEnumerable<Tuple<string, string, double>> conversionRates){
        throw new Exception("dont allow");// But I know this is not true thats not checkable on compile time 
    }

    public double Convert(string fromCurrency, string toCurrency, double amount){
        return convertor.Convert(fromCurrency,toCurrency,amount);
    }

}




