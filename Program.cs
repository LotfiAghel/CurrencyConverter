

{
    var test=new Test0<ThreadSafeWithoutLockConvertor<FloydWarshallCurrencyConverter>>();
    test.run();
}


{
    var test=new Test0<ThreadSafeWithReaderWriterLockConvertor<FloydWarshallCurrencyConverter>>();
    test.run();
}



{
    var test=new Test1();
    test.run();
}


{
    var test=new Test2();
    test.run();
}
