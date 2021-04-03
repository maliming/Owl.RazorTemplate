using System.IO;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.Extensions.FileProviders;

namespace Owl.RazorTemplate
{
    public class FileProviderRazorProjectItem : RazorProjectItem
    {
        public FileProviderRazorProjectItem(IFileInfo fileInfo)
        {
            FileInfo = fileInfo;
        }

        public IFileInfo FileInfo { get; }

        public override string BasePath => null;

        public override string FilePath => null;

        public override string FileKind => null;

        public override bool Exists => FileInfo.Exists;

        public override string PhysicalPath => null;

        public override string RelativePhysicalPath => null;

        public override Stream Read()
        {
            return FileInfo.CreateReadStream();
        }
    }
}
