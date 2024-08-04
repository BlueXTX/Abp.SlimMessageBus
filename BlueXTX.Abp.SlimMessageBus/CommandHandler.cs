using FluentResults;
using SlimMessageBus;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace BlueXTX.Abp.SlimMessageBus;

public abstract class CommandHandler<TCommand> : IRequestHandler<TCommand, IResultBase>, IApplicationService, ITransientDependency
{
    Task<IResultBase> IRequestHandler<TCommand, IResultBase>.OnHandle(TCommand command) => Handle(command);
    protected abstract Task<IResultBase> Handle(TCommand command);
}
