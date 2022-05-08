
struct ThreadSafeWithReaderWriterLockConvertor<T>:ICurrencyConverter where T: ICurrencyConverter ,new()
{

    private T convertor;
    ReaderWriterLock rwl ;


    public ThreadSafeWithReaderWriterLockConvertor(){
        convertor =new T();
        rwl = new ReaderWriterLock();
    }
    public void ClearConfiguration(){
        convertor.ClearConfiguration();
    }
    
    public void UpdateConfiguration(IEnumerable<Tuple<string, string, double>> conversionRates){
         try {
            rwl.AcquireWriterLock(1000000);
            convertor.UpdateConfiguration(conversionRates);
         }
         finally {
            // Ensure that the lock is released.
            rwl.ReleaseReaderLock();
         }
        

    }

    public double Convert(string fromCurrency, string toCurrency, double amount){
        try {
            rwl.AcquireReaderLock(1000000);
            return convertor.Convert(fromCurrency,toCurrency,amount);
         }
         finally {
            // Ensure that the lock is released.
            rwl.ReleaseReaderLock();
         }
        
    }

}


