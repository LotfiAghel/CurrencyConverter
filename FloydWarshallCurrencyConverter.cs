class FloydWarshallCurrencyConverter : ICurrencyConverter
{

    
    

    List<Tuple<string, string, double>> configurations{get;set;}=new List<Tuple<string, string, double>>();


    /// <summary>
    ///   at first, every Currency will be converted to int
    /// </summary>
    Dictionary<string,int> CurrencyNameToIndex{get;set;}=new Dictionary<string, int>();



    private int n;
    /// <summary>
    ///    in this array, we save the chosen shortest path length between i, j
    /// </summary>
    int[,] ShortPathMatrix ;

    /// <summary>
    ///    in this array, we save one of the vertexes the chosen shortest path between i, j. 
    ///    if  ShortPathFinder[i][j]==k  thats mean (i)--->(k)--->(j) is one of shortess math bitwen i and j 
    /// </summary>
    int[,] ShortPathFinder ;


    public void ClearConfiguration(){
        configurations.Clear();
    }

    private void indexNewCurrency(string a){
            if(!CurrencyNameToIndex.ContainsKey(a)){
                CurrencyNameToIndex[a]=CurrencyNameToIndex.Count;
            }
    }
    public void UpdateConfiguration(IEnumerable<Tuple<string, string, double>> conversionRates){
        // TODO set write lock
        configurations.Clear();
        
        foreach(var i in configurations){
            this.configurations.Add(i);
            indexNewCurrency(i.Item1);
            indexNewCurrency(i.Item2);
        }
        
        n=CurrencyNameToIndex.Count;
        ShortPathMatrix=new int[n,n];
        ShortPathFinder=new int[n,n];
        for(int i=0; i<n ; ++i)
            for(int j=0; j<n ; ++j){
              ShortPathMatrix[i,j]=1000000;
              ShortPathFinder[i,j]=-1;
            }
       
    }



    public double Convert(string fromCurrency, string toCurrency, double amount){
        // TODO set read lock
        return 0;
    }
}