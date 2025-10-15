using Architecture;

Console.WriteLine(BreadV1().Value); // 1
Console.WriteLine(BreadV2().Value); // 2
Console.WriteLine(BearingV1().Value); // 10
return;

static BreadPrediction BreadV1()
{
    // Входные данные для обучения
    BreadDataV1[] trainingData =
    [
        new(10M, 5),
        new(12M, 7)
    ];
        
    // Новые данные для прогноза
    BreadDataV1 newData = new(9M, 4);

    var trainer = new BreadTrainerV1();
    var model = trainer.Train(trainingData);

    var forecast = new BreadPredictorV1();
    return forecast.Predict(model, newData);
}

static BreadPrediction BreadV2()
{
    // Входные данные для обучения
    BreadDataV2[] trainingData =
    [
        new(110M, 15),
        new(112M, 17)
    ];
        
    // Новые данные для прогноза
    BreadDataV2 newData = new(19M, 14);

    var trainer = new BreadTrainerV2();
    var model = trainer.Train(trainingData);

    var forecast = new BreadPredictorV2();
    return forecast.Predict(model, newData);
}

static BearingPrediction BearingV1()
{
    // Входные данные для обучения
    BearingDataV1[] trainingData =
    [
        new(211),
        new(212)
    ];
        
    // Новые данные для прогноза
    BearingDataV1 newData = new(219);

    var trainer = new BearingTrainerV1();
    var model = trainer.Train(trainingData);

    var forecast = new BearingPredictorV1();
    return forecast.Predict(model, newData);
}