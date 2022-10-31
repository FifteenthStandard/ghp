using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Physical;
using Microsoft.Extensions.Primitives;

namespace ghp
{

    public class GitHubFileProvider : IFileProvider
    {
        private readonly string _root;
        private readonly IFileProvider _base;

        public GitHubFileProvider(string root)
        {
            _root = root;
            _base = new PhysicalFileProvider(root);
        }

        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            var fullPath = Path.Join(_root, subpath);
            var htmlIndex = Path.Join(_root, subpath.TrimEnd('/') + ".html");

            return Directory.Exists(fullPath)
                ? _base.GetDirectoryContents(subpath)
                : File.Exists(htmlIndex)
                ? new DirectoryExistsContents()
                : new NotFoundDirectoryContents();
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            var fullPath = Path.Join(
                _root,
                subpath.EndsWith("/default.htm")
                    ? (subpath.Substring(0, subpath.Length - "/default.htm".Length) + ".html")
                    : subpath);

            return File.Exists(fullPath)
                ? new PhysicalFileInfo(new FileInfo(fullPath))
                : new NotFoundFileInfo(fullPath);
        }

        public IChangeToken Watch(string filter)
        {
            throw new System.NotImplementedException();
        }
    }
}
