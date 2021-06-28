
using Reinforced.Typings.Attributes;
using Reinforced.Typings.Fluent;

[assembly: TsGlobal(GenerateDocumentation = true, UseModules = true, DiscardNamespacesWhenUsingModules = true)]
namespace AvService.Shared
{
    public static class ReinforcedTypingsConfiguration
    {
        public static void Configure(ConfigurationBuilder builder)
        {
            builder.ExportAsClass<Notification>().WithPublicProperties();
        }
    }
}
