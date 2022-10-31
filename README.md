# GHP

A GitHub Pages-style static site host.

This tool is useful for serving your static GitHub Pages repositories locally
for testing purposes. GHP runs on all available IP addresses (`0.0.0.0`) so you
can use it locally (via `localhost`) or from other devices on your local
network (your network IP addresses are printed when you run the tool).


## Build

Building requires [.NET 6.0](https://dotnet.microsoft.com/en-us/download).

```cmd
dotnet build
```


## Publish

Publishing will create a standalone executable.

```cmd
dotnet publish
```


## Install

Install by adding the published executable anywhere on your `PATH`.


## Run

After adding the published executable to your `PATH`, you can use the following
to run:

```cmd
ghp [options]
```

### Options

* `--root <root>`: Specify the directory root. Defaults to current directory
* `--port <port>`: Specify the port. Defaults to 8080