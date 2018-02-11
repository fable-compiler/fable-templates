open System

#r "paket:
nuget Fake.DotNet.Cli
nuget Fake.IO.FileSystem
nuget Fake.Core.Target //"
#load "./.fake/workflow.fsx/intellisense.fsx"

open Fake.IO
open Fake.Core
let mutable dotnetExePath = "dotnet"
let toolDir = "./tools/" |> Path.getFullName
let runDotnet workingDir args =
    let result =
        Process.ExecProcess (fun info ->
        { info with 
            FileName = dotnetExePath
            WorkingDirectory = workingDir 
            Arguments =  args }) TimeSpan.MaxValue
    if result <> 0 then failwithf "dotnet %s failed" args 
    
Target.Create "Build" (fun _ ->
    runDotnet toolDir "fable webpack -- -p --config webpack.config.prod.js"
)

Target.Create "Start" (fun _ ->
    runDotnet toolDir "fable webpack-dev-server -- --config webpack.config.dev.js"
)

Target.RunOrDefault "Start"
