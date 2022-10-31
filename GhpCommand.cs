using System.CommandLine;
using System.Net;
using System.Net.Sockets;

namespace ghp
{
    public class GhpCommand : RootCommand
    {
        public GhpCommand(WebApplicationBuilder builder)
            : base("GitHub Pages-style static site host")
        {
            this.Name = "ghp";

            var root = new Option<string>("--root", "Directory root");
            var port = new Option<int>("--port", () => 8080, "Port");

            this.Add(root);
            this.Add(port);

            this.SetHandler((root, port) =>
            {
                if (string.IsNullOrWhiteSpace(root)) root = Environment.CurrentDirectory;
                root = Path.IsPathFullyQualified(root)
                    ? root
                    : Path.Join(Environment.CurrentDirectory, root);

                builder.WebHost.UseKestrel(options =>
                {
                    options.ListenAnyIP(port);
                });

                var app = builder.Build();

                app.UseFileServer(new FileServerOptions
                {
                    FileProvider = new GitHubFileProvider(root)
                });

                Console.WriteLine($"Serving content from {root}");
                Console.WriteLine($"Local IP Addresses are {string.Join(", ", LocalIps(port))}");

                app.Run();
            }, root, port);
        }

    private IEnumerable<string> LocalIps(int port)
        => Dns.GetHostEntry(Dns.GetHostName())
            .AddressList
            .Where(ip => ip.AddressFamily == AddressFamily.InterNetwork)
            .Select(ip => $"http://{ip}:{port}");
    }
}