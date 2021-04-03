using System.Reflection;
using System.Threading.Tasks;
using Volo.Abp.TextTemplating;

namespace Owl.RazorTemplate
{
    public interface ICompiledViewProvider
    {
        Task<Assembly> GetAssemblyAsync(TemplateDefinition templateDefinition, string templateContent);
    }
}
