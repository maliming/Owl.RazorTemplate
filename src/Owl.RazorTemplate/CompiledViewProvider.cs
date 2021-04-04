using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
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
        private readonly ITemplateContentProvider _templateContentProvider;

        public CompiledViewProvider(
            IRazorProjectEngineFactory razorProjectEngineFactory,
            CSharpCompiler cSharpCompiler,
            ITemplateContentProvider templateContentProvider)
        {
            _razorProjectEngineFactory = razorProjectEngineFactory;
            _cSharpCompiler = cSharpCompiler;
            _templateContentProvider = templateContentProvider;
        }

        public virtual async Task<Assembly> GetAssemblyAsync(TemplateDefinition templateDefinition)
        {
            async Task<Assembly> CreateAssembly(string content)
            {
                using (var assemblyStream = await GetAssemblyStreamAsync(templateDefinition, content))
                {
                    return Assembly.Load(await assemblyStream.GetAllBytesAsync());
                }
            }

            var templateContent = await _templateContentProvider.GetContentOrNullAsync(templateDefinition);
            return CachedAssembles.GetOrAdd((templateDefinition.Name + templateContent).ToMd5(), await CreateAssembly(templateContent));
        }

        public virtual async Task<Stream> GetAssemblyStreamAsync(TemplateDefinition templateDefinition)
        {
            var templateContent = await _templateContentProvider.GetContentOrNullAsync(templateDefinition);
            return await GetAssemblyStreamAsync(templateDefinition, templateContent);
        }

        protected virtual Task<Stream> GetAssemblyStreamAsync(TemplateDefinition templateDefinition, string templateContent)
        {
            var razorProjectEngine = _razorProjectEngineFactory.Create();
            var codeDocument = razorProjectEngine.Process(
                RazorSourceDocument.Create(templateContent, templateDefinition.Name), null,
                new List<RazorSourceDocument>(), new List<TagHelperDescriptor>());

            var cSharpDocument = codeDocument.GetCSharpDocument();

            return Task.FromResult(_cSharpCompiler.CreateAssembly(cSharpDocument.GeneratedCode, templateDefinition.Name));
        }
    }
}
