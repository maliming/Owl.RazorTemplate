using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.Language;
using Volo.Abp.DependencyInjection;
using Volo.Abp.TextTemplating;

namespace Owl.RazorTemplate
{
    public class CompiledViewProvider : ICompiledViewProvider, ITransientDependency
    {
        private static readonly ConcurrentDictionary<string, Assembly> CachedAssembles = new ConcurrentDictionary<string, Assembly>();

        private readonly CSharpCompiler _cSharpCompiler;
        private readonly IRazorProjectEngineFactory _razorProjectEngineFactory;

        public CompiledViewProvider(
            IRazorProjectEngineFactory razorProjectEngineFactory,
            CSharpCompiler cSharpCompiler)
        {
            _razorProjectEngineFactory = razorProjectEngineFactory;
            _cSharpCompiler = cSharpCompiler;
        }

        public Task<Assembly> GetAssemblyAsync(TemplateDefinition templateDefinition, string templateContent)
        {
            Assembly CreateAssembly()
            {
                var razorProjectEngine = _razorProjectEngineFactory.Create();
                var codeDocument = razorProjectEngine.Process(
                    RazorSourceDocument.Create(templateContent, templateDefinition.Name), null,
                    new List<RazorSourceDocument>(), new List<TagHelperDescriptor>());

                var cSharpDocument = codeDocument.GetCSharpDocument();
                return _cSharpCompiler.CreateAssembly(cSharpDocument.GeneratedCode, templateDefinition.Name);
            }

            return Task.FromResult(CachedAssembles.GetOrAdd((templateDefinition.Name + templateContent).ToMd5(), CreateAssembly()));
        }
    }
}
