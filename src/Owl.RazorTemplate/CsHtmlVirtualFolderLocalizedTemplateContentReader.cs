using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.FileProviders;
using Volo.Abp;
using Volo.Abp.TextTemplating.VirtualFiles;
using Volo.Abp.VirtualFileSystem;

namespace Owl.RazorTemplate
{
    public class CsHtmlVirtualFolderLocalizedTemplateContentReader : ILocalizedTemplateContentReader
    {
        private Dictionary<string, string> _dictionary;

        public async Task ReadContentsAsync(
            IVirtualFileProvider virtualFileProvider,
            string virtualPath)
        {
            _dictionary = new Dictionary<string, string>();

            var directoryContents = virtualFileProvider.GetDirectoryContents(virtualPath);
            if (!directoryContents.Exists)
            {
                throw new AbpException("Could not find a folder at the location: " + virtualPath);
            }

            foreach (var file in directoryContents)
            {
                if (file.IsDirectory)
                {
                    continue;
                }

                _dictionary.Add(file.Name.RemovePostFix(".cshtml"), await file.ReadAsStringAsync());
            }
        }

        public string GetContentOrNull(string cultureName)
        {
            if (cultureName == null)
            {
                return null;
            }

            return _dictionary.GetOrDefault(cultureName);
        }
    }
}
