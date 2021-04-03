using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace Owl.RazorTemplate
{
    public class CSharpCompilerOptions
    {
        public List<PortableExecutableReference> References { get; }

        public CSharpCompilerOptions()
        {
            References = new List<PortableExecutableReference>();
        }
    }
}
