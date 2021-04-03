using System;
using Microsoft.AspNetCore.Razor.Language;
using Volo.Abp.DependencyInjection;

namespace Owl.RazorTemplate
{
    public class DefaultRazorProjectEngineFactory : IRazorProjectEngineFactory, ITransientDependency
    {
        public virtual RazorProjectEngine Create(Action<RazorProjectEngineBuilder> configure = null)
        {
            return RazorProjectEngine.Create(CreateRazorConfiguration(), EmptyProjectFileSystem.Empty, configure);
        }

        protected virtual RazorConfiguration CreateRazorConfiguration()
        {
            return RazorConfiguration.Default;
        }
    }
}
