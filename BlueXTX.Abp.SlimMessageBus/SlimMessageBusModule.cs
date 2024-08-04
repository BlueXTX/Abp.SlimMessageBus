using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace BlueXTX.Abp.SlimMessageBus;

[DependsOn(typeof(AbpDddApplicationModule))]
public class SlimMessageBusModule : AbpModule;
