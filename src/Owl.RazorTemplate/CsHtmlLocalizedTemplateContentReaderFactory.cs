using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.TextTemplating;
using Volo.Abp.TextTemplating.VirtualFiles;
using Volo.Abp.VirtualFileSystem;

namespace Owl.RazorTemplate
{
    [Dependency(ReplaceServices = true)]
    public class CsHtmlLocalizedTemplateContentReaderFactory : LocalizedTemplateContentReaderFactory
    {
        public CsHtmlLocalizedTemplateContentReaderFactory(IVirtualFileProvider virtualFileProvider)
            : base(virtualFileProvider)
        {
        }

        protected override async Task<ILocalizedTemplateContentReader> CreateInternalAsync(
            TemplateDefinition templateDefinition)
        {
            var virtualPath = templateDefinition.GetVirtualFilePathOrNull();
            if (virtualPath == null)
            {
                return NullLocalizedTemplateContentReader.Instance;
            }

            var fileInfo = VirtualFileProvider.GetFileInfo(virtualPath);
            if (!fileInfo.Exists)
            {
                var directoryContents = VirtualFileProvider.GetDirectoryContents(virtualPath);
                if (!directoryContents.Exists)
                {
                    throw new AbpException("Could not find a file/folder at the location: " + virtualPath);
                }

                fileInfo = new VirtualDirectoryFileInfo(virtualPath, virtualPath, DateTimeOffset.UtcNow);
            }

            if (fileInfo.IsDirectory)
            {
                var folderReader = new CsHtmlVirtualFolderLocalizedTemplateContentReader();
                await folderReader.ReadContentsAsync(VirtualFileProvider, virtualPath);
                return folderReader;
            }
            else //File
            {
                var singleFileReader = new FileInfoLocalizedTemplateContentReader();
                await singleFileReader.ReadContentsAsync(fileInfo);
                return singleFileReader;
            }
        }
    }
}
