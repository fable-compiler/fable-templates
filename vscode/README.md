## Build and running the app

- Make sure nodejs, the .Net SDK, and yarn are installed and on your path
- install or upgrade the templates with `dotnet new -i ctaggart.templates`
- create a new project from the template with `dotnet new vscode -n myvscode`
- Switch to directory: `cd myvscode`
- Launch Visual Studio Code: `code .`
- In vscode press F5 to get dependencies, build, and launch.

Look at ./.vscode/tasks.json to understand what's going on.