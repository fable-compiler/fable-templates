#r "paket: groupref netcorebuild //"
#load ".fake/build.fsx/intellisense.fsx"
#if !FAKE
#r "Facades/netstandard"
#r "netstandard"
#endif

#nowarn "52"

open System
open Fake.Core
open Fake.Core.TargetOperators
open Fake.DotNet
open Fake.IO
open Fake.IO.Globbing.Operators
open Fake.JavaScript

let runFable args =
    let result =
        DotNet.exec
            (DotNet.Options.withWorkingDirectory __SOURCE_DIRECTORY__)
            "fable" args
    if not result.OK then
        failwithf "dotnet fable failed with code %i" result.ExitCode

Target.create "Clean" (fun _ ->
    !! "src/bin"
    ++ "src/obj"
    ++ "output"
    |> Seq.iter Shell.cleanDir
)

Target.create "DotnetRestore" (fun _ ->
    DotNet.restore
        (DotNet.Options.withWorkingDirectory __SOURCE_DIRECTORY__)
        "Fable2TemplateFulma.sln"
)

Target.create "YarnInstall" (fun _ ->
    Yarn.install id
)

Target.create "Build" (fun _ ->
    runFable "webpack-cli"
)

Target.create "Watch" (fun _ ->
    runFable "webpack-dev-server"
)

// Build order
"Clean"
    ==> "DotnetRestore"
    ==> "YarnInstall"
    ==> "Build"

"YarnInstall"
    ==> "Watch"

Target.runOrDefault "Build"
