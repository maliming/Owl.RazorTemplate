using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;

namespace Owl.RazorTemplate
{
    public interface IRazorTemplatePage
    {
        IServiceProvider ServiceProvider { get; set; }

        IStringLocalizer Localizer { get; set; }

        Task ExecuteAsync();

        Task<string> GetOutputAsync();
    }
}
