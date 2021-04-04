using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.Language;

namespace Owl.RazorTemplate
{
    public interface IRazorProjectEngineFactory
    {
        Task<RazorProjectEngine> CreateAsync(Action<RazorProjectEngineBuilder> configure = null);
    }
}
