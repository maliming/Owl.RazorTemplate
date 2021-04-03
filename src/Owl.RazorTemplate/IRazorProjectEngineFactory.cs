using System;
using Microsoft.AspNetCore.Razor.Language;

namespace Owl.RazorTemplate
{
    public interface IRazorProjectEngineFactory
    {
        RazorProjectEngine Create(Action<RazorProjectEngineBuilder> configure = null);
    }
}
