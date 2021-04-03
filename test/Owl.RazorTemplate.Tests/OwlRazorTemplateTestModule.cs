using Microsoft.CodeAnalysis;
using Owl.RazorTemplate.Tests.Localization;
using Volo.Abp.Autofac;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Owl.RazorTemplate.Tests
{
    [DependsOn(
        typeof(OwlRazorTemplateModule),
        typeof(AbpAutofacModule),
        typeof(AbpLocalizationModule)
    )]
    public class OwlRazorTemplateTestModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<CSharpCompilerOptions>(options =>
            {
                options.References.Add(MetadataReference.CreateFromFile(typeof(OwlRazorTemplateTestModule).Assembly.Location));
            });

            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<OwlRazorTemplateTestModule>("Owl.RazorTemplate.Tests");
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<TestLocalizationSource>("en")
                    .AddVirtualJson("/Localization");
            });
        }
    }
}
