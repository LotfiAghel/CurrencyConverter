class FloydWarshallCurrencyConverter : ICurrencyConverter
{

    public void ClearConfiguration()
    {
        configurations.Clear();
    }


    public void UpdateConfiguration(IEnumerable<Tuple<string, string, double>> conversionRates)
    {
        // TODO set write lock
        configurations.Clear();

        foreach (var i in conversionRates)
        {
            this.configurations.Add(i);
            indexNewCurrency(i.Item1);
            indexNewCurrency(i.Item2);
        }

        vertexSize = CurrencyNameToIndex.Count;
        ShortPathLength = new int[vertexSize, vertexSize];
        ShortPathFinder = new int[vertexSize, vertexSize];
        ShortPathValue = new double[vertexSize, vertexSize];
        for (int i = 0; i < vertexSize; ++i)
            for (int j = 0; j < vertexSize; ++j)
            {
                ShortPathLength[i, j] = 1000000;
                ShortPathFinder[i, j] = -1;
                ShortPathValue[i, j] = double.NaN;
            }
        foreach (var i in configurations)
        {
            ShortPathLength[CurrencyNameToIndex[i.Item1], CurrencyNameToIndex[i.Item2]] = 1;
            ShortPathLength[CurrencyNameToIndex[i.Item2], CurrencyNameToIndex[i.Item1]] = 1;

            ShortPathValue[CurrencyNameToIndex[i.Item1], CurrencyNameToIndex[i.Item2]] = i.Item3;
            ShortPathValue[CurrencyNameToIndex[i.Item2], CurrencyNameToIndex[i.Item1]] = 1.0 / i.Item3;
        }
        doFloyd();
    }



    public double Convert(string fromCurrency, string toCurrency, double amount)
    {
        // TODO set read lock
        return ShortPathValue[CurrencyNameToIndex[fromCurrency], CurrencyNameToIndex[toCurrency]] * amount;

    }



    private void indexNewCurrency(string a)
    {
        if (!CurrencyNameToIndex.ContainsKey(a))
        {
            CurrencyNameToIndex[a] = CurrencyNameToIndex.Count;
        }
    }

    private void doFloyd()
    {
        for (int i = 0; i < vertexSize; ++i)
            for (int j = 0; j < vertexSize; ++j)
                for (int k = 0; k < vertexSize; ++k)
                {
                    if (ShortPathLength[i, j] > ShortPathLength[i, k] + ShortPathLength[k, j])
                    {

                        ShortPathLength[i, j] = ShortPathLength[i, k] + ShortPathLength[k, j];
                        ShortPathValue[i, j] = ShortPathValue[i, k] * ShortPathValue[k, j];
                        ShortPathFinder[i, j] = k;

                    }
                }
    }

    List<Tuple<string, string, double>> configurations { get; set; } = new List<Tuple<string, string, double>>();


    /// <summary>
    ///   at first, every Currency will be converted to int
    /// </summary>
    Dictionary<string, int> CurrencyNameToIndex { get; set; } = new Dictionary<string, int>();



    private int vertexSize;
    /// <summary>
    ///    in this array, we save the chosen shortest path length between i, j
    /// </summary>
    int[,] ShortPathLength { get; set; }

    /// <summary>
    ///    in this array, we save one of the vertexes of the chosen shortest path between i, j. 
    ///    if  ShortPathFinder[i,j]==k  thats mean (i)--->(k)--->(j) is one of shortess math bitwen i and j 
    /// </summary>
    int[,] ShortPathFinder { get; set; }

    double[,] ShortPathValue { get; set; }

}