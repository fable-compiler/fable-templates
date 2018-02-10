
#r "paket:
nuget Fake.DotNet.Cli
nuget Fake.IO.FileSystem
nuget Fake.Core.Target //"
#load "./.fake/workflow.fsx/intellisense.fsx"
open Fake.DotNet.Cli
open Fake.IO
open Fake.Core
let dir = "./tools/" |> Path.getFullName

Target.Create "Build" (fun _ ->
    Dotnet 
        {DotnetOptions.Default with DotnetCliPath = "dotnet"; WorkingDirectory = dir} 
        "fable webpack -- -p --config webpack.config.prod.js"
    |> ignore   
)

Target.Create "Start" (fun _ ->
    Dotnet 
        {DotnetOptions.Default with DotnetCliPath = "dotnet"; WorkingDirectory = dir} 
        "fable webpack-dev-server -- --config webpack.config.dev.js"
    |> ignore   
)

Target.RunOrDefault "Start"
