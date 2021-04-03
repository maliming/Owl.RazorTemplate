using Volo.Abp;
using Volo.Abp.Testing;

namespace Owl.RazorTemplate.Tests
{
    public abstract class OwlRazorTemplateTestBase : AbpIntegratedTest<OwlRazorTemplateTestModule>
    {
        protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
        {
            options.UseAutofac();
        }
    }
}
