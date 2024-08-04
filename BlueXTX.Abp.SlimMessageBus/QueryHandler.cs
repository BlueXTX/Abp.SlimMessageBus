using FluentResults;
using SlimMessageBus;
using Volo.Abp.DependencyInjection;

namespace BlueXTX.Abp.SlimMessageBus;

public abstract class QueryHandler<TQuery, TAnswer> : IRequestHandler<TQuery, IResult<TAnswer>>, ITransientDependency
{
    Task<IResult<TAnswer>> IRequestHandler<TQuery, IResult<TAnswer>>.OnHandle(TQuery query) => Handle(query);
    protected abstract Task<IResult<TAnswer>> Handle(TQuery query);
}
