using FluentResults;
using Humanizer;
using SlimMessageBus.Host;

namespace BlueXTX.Abp.SlimMessageBus;

public static class MessageBusRegistrationExtensions
{
    public static MessageBusBuilder HandleCommand<TCommand, THandler>(
        this MessageBusBuilder builder,
        string commandTopicsPrefix)
        where THandler : CommandHandler<TCommand>
    {
        if (string.IsNullOrWhiteSpace(commandTopicsPrefix)) throw new ArgumentNullException(nameof(commandTopicsPrefix));

        var commandTopic = $"{commandTopicsPrefix}-{Kebaberize<TCommand>()}";
        return builder.Produce<TCommand>(producerBuilder => producerBuilder.DefaultTopic(commandTopic))
            .Handle<TCommand, IResultBase>(
                handlerBuilder => handlerBuilder
                    .Topic(commandTopic)
                    .WithHandler<THandler>());
    }

    public static MessageBusBuilder HandleQuery<TQuery, TAnswer, THandler>(
        this MessageBusBuilder builder,
        string queryTopicsPrefix)
        where THandler : QueryHandler<TQuery, TAnswer>
        where TAnswer : notnull
    {
        if (string.IsNullOrWhiteSpace(queryTopicsPrefix)) throw new ArgumentNullException(nameof(queryTopicsPrefix));

        var queryTopic = $"{queryTopicsPrefix}-{Kebaberize<TQuery>()}";

        return builder.Produce<TQuery>(producerBuilder => producerBuilder.DefaultTopic(queryTopic))
            .Handle<TQuery, IResult<TAnswer>>(
                handlerBuilder => handlerBuilder
                    .Topic(queryTopic)
                    .WithHandler<THandler>());
    }

    private static string Kebaberize<T>() => typeof(T).Name.Humanize(LetterCasing.LowerCase).Kebaberize();
}
