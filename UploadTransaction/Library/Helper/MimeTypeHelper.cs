using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace UploadTransaction.Library.Helper
{
    public static class MimeTypeHelper
    {
        private static readonly Lazy<IDictionary<string, string>> _mappings = new Lazy<IDictionary<string, string>>(BuildMappings);

        private static IDictionary<string, string> BuildMappings()
        {
            var mappings = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase) {

                  {".xml", "text/xml"},
                  {".csv", "text/csv"},
             };

            var cache = mappings.ToList(); // need ToList() to avoid modifying while still enumerating

            foreach (var mapping in cache)
            {
                if (!mappings.ContainsKey(mapping.Value))
                {
                    mappings.Add(mapping.Value, mapping.Key);
                }
            }

            return mappings;
        }

        public static bool IsMatchMimeType(string extensionProtoType,IFormFile formFile)
        {
            if (extensionProtoType == null)
            {
                throw new ArgumentNullException("extensionProtoType");
            }
            
            extensionProtoType = extensionProtoType.ToLower();
            if (!extensionProtoType.StartsWith("."))
            {
                extensionProtoType = "." + extensionProtoType;
            }

            var extension = Path.GetExtension(formFile.FileName).ToLower();
            if(!extension.StartsWith("."))
            {
                extension = "." + extension;
            }
            string mime;
            if (_mappings.Value.TryGetValue(extensionProtoType, out mime))
            {
                if(extension == extensionProtoType) // mime == formFile.ContentType
                {
                    return true;
                }
            }
            return false;

        }

        public static string GetMimeType(string extension)
        {
            if (extension == null)
            {
                throw new ArgumentNullException("extension");
            }

            if (!extension.StartsWith("."))
            {
                extension = "." + extension;
            }

            string mime;

            return _mappings.Value.TryGetValue(extension, out mime) ? mime : "application/octet-stream";
        }

        public static string GetExtension(string mimeType)
        {
            if (mimeType == null)
            {
                throw new ArgumentNullException("mimeType");
            }

            if (mimeType.StartsWith("."))
            {
                throw new ArgumentException("Requested mime type is not valid: " + mimeType);
            }

            string extension;

            if (_mappings.Value.TryGetValue(mimeType, out extension))
            {
                return extension;
            }

            throw new ArgumentException("Requested mime type is not registered: " + mimeType);
        }
    }
}
