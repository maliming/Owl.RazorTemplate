using Volo.Abp.Modularity;
using Volo.Abp.TextTemplating;

namespace Owl.RazorTemplate
{
    [DependsOn(typeof(AbpTextTemplatingModule))]
    public class OwlRazorTemplateModule : AbpModule
    {

    }
}
