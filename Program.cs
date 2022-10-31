using ghp;
using System.CommandLine;

var builder = WebApplication.CreateBuilder(args);
var command = new GhpCommand(builder);

return await command.InvokeAsync(args);
