# Read me

Dotnet Api created for my app Curie (Open-source app created to be my template and feature playground, made with Flutter.)

- [Dotnet Setup](#dotnet-setup)
- [Docker](#docker)

## Dotnet Setup

- For .NET development, install [.NET Core SDK](https://dotnet.microsoft.com/download).
- Microsoft extension [C# for Visual Studio Code](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp).

## Docker

### Install Docker

- installation on [Windows](https://docs.docker.com/docker-for-windows/install/) (i recomend to use [Windows Subsystem for Linux 2](https://docs.microsoft.com/en-us/windows/wsl/wsl2-kernel))
- installation on [Linux](https://docs.docker.com/engine/install/ubuntu/)

### Install PostgreSQL

- `docker pull postgres`
- `docker run --name postgresql -e POSTGRES_PASSWORD=<password> -p 5432:5432 -d postgres`
