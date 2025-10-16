using System.Numerics;

namespace Architecture;

// Входные данные для обучения (приходит много таких тренеру) и прогноза (приходит один прогнозисту)
public abstract record Data;

// Входные данные для любой версии хлеба
public abstract record BreadData : Data;

// Входные данные для первой версии хлеба с ценой на пшеницу и электричество
public record BreadDataV1(decimal WheatPrice, decimal ElectricityPrice) : BreadData;

// Входные данные для первой версии хлеба с ценой на пшеницу и газ
public record BreadDataV2(decimal WheatPrice, decimal GasPrice) : BreadData;

// Входные данные для любой версии подшипника
public abstract record BearingData : Data;

// Входные данные для первой версии подшипника с силой трения
public record BearingDataV1(double Friction) : BearingData;

// Результат прогноза, может быть любым числом
public abstract record Prediction<T>(T Value) where T : INumberBase<T>;

// Результат прогноза хлеба - цена
public record BreadPrediction(decimal Value) : Prediction<decimal>(Value);

// Результат прогноза подшипника - процент износа
public record BearingPrediction(double Value) : Prediction<double>(Value);

// Обученная модель, возвращается тренером
public abstract record Model(string Name);

// Обученная модель для хлеба
public record BreadModel(string Name) : Model(Name);

// Обученная модель для подшипника
public record BearingModel(string Name) : Model(Name);

// Тренер, обучающий модель
public interface ITrainer<in TData, out TModel>
    where TModel : Model
    where TData : Data
{
    TModel Train(TData[] data);
}

// Тренер, обучающий модель на данных хлеба
public interface IBreadTrainer<in T> : ITrainer<T, BreadModel>
    where T : BreadData;

// Тренер, обучающий модель на данных хлеба версии 1
public class BreadTrainerV1 : IBreadTrainer<BreadDataV1>
{
    public BreadModel Train(BreadDataV1[] data) => new("Модель хлеба 1.");
}

// Тренер, обучающий модель на данных хлеба версии 2
public class BreadTrainerV2 : IBreadTrainer<BreadDataV2>
{
    public BreadModel Train(BreadDataV2[] data) => new("Модель хлеба 2.");
}

// Тренер, обучающий модель на данных подшипника
public interface IBearingTrainer<in T> : ITrainer<T, BearingModel>
    where T : BearingData;

// Тренер, обучающий модель на данных подшипника версии 1
public class BearingTrainerV1 : IBearingTrainer<BearingDataV1>
{
    public BearingModel Train(BearingDataV1[] data) => new("Модель подшипника 1.");
}

// Прогнозист 
public interface IPredictor<in TModel, in TData, out TPrediction, TPredictionType>
    where TModel : Model
    where TData : Data
    where TPrediction : Prediction<TPredictionType>
    where TPredictionType : INumberBase<TPredictionType>
{
    TPrediction Predict(TModel model, TData data);
}

// Прогнозист данных для хлеба
public interface IBreadPredictor<in TModel, in TData, out TPrediction> : IPredictor<TModel, TData, TPrediction, decimal>
    where TModel : BreadModel
    where TData : BreadData
    where TPrediction : BreadPrediction;

// Прогнозист данных для хлеба версии 1
public class BreadPredictorV1 : IBreadPredictor<BreadModel, BreadDataV1, BreadPrediction>
{
    public BreadPrediction Predict(BreadModel model, BreadDataV1 data) => new(1M);
}

// Прогнозист данных для хлеба версии 2
public class BreadPredictorV2 : IBreadPredictor<BreadModel, BreadDataV2, BreadPrediction>
{
    public BreadPrediction Predict(BreadModel model, BreadDataV2 data) => new(2M);
}

// Прогнозист данных для подшипника
public interface IBearingPredictor<in TModel, in TData, out TPrediction> : IPredictor<TModel, TData, TPrediction, double>
    where TModel : BearingModel
    where TData : BearingData
    where TPrediction : BearingPrediction;

// Прогнозист данных для подшипника версии 1
public class BearingPredictorV1 : IBearingPredictor<BearingModel, BearingDataV1, BearingPrediction>
{
    public BearingPrediction Predict(BearingModel model, BearingDataV1 data) => new(10);
}
