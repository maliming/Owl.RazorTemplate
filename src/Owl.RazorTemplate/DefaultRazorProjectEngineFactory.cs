﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.Language;
using Volo.Abp.DependencyInjection;

namespace Owl.RazorTemplate
{
    public class DefaultRazorProjectEngineFactory : IRazorProjectEngineFactory, ITransientDependency
    {
        public virtual async Task<RazorProjectEngine> CreateAsync(Action<RazorProjectEngineBuilder> configure = null)
        {
            return RazorProjectEngine.Create(await CreateRazorConfigurationAsync(), EmptyProjectFileSystem.Empty, configure);
        }

        protected virtual Task<RazorConfiguration> CreateRazorConfigurationAsync()
        {
            return Task.FromResult(RazorConfiguration.Default);
        }
    }
}
