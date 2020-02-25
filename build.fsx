#r "paket:
nuget Fake.DotNet.Cli
nuget Fake.Core.Target
nuget Fake.Core.Process
nuget Fake.Core.String
nuget Fake.IO.FileSystem
nuget Fake.JavaScript.Npm
nuget BlackFox.Fake.BuildTask
nuget Fake.Api.GitHub
nuget Fake.Tools.Git 
//"

#load ".fake/build.fsx/intellisense.fsx"
#if !FAKE
#r "Facades/netstandard"
#r "netstandard"
#endif

#nowarn "52"

open System
open System.IO
open System.Text.RegularExpressions

open Fake.Core
open Fake.IO
open Fake.IO.Globbing.Operators
open Fake.IO.FileSystemOperators
open Fake.DotNet
open Fake.Tools
open Fake.Api
open Fake.JavaScript
open BlackFox.Fake

let CWD = __SOURCE_DIRECTORY__

module Util =

    let visitFile (visitor: string -> string) (fileName : string) =
        File.ReadAllLines(fileName)
        |> Array.map (visitor)
        |> fun lines -> File.WriteAllLines(fileName, lines)

    let replaceLines (replacer: string -> Match -> string option) (reg: Regex) (fileName: string) =
        fileName |> visitFile (fun line ->
            let m = reg.Match(line)
            if not m.Success
            then line
            else
                match replacer line m with
                | None -> line
                | Some newLine -> newLine)

module Changelog =

    let versionRegex = Regex("^## ?\\[?v?([\\w\\d.-]+\\.[\\w\\d.-]+[a-zA-Z0-9])\\]?", RegexOptions.IgnoreCase)

    let getLastVersion changelogPath =
        File.ReadLines(changelogPath)
            |> Seq.tryPick (fun line ->
                let m = versionRegex.Match(line)
                if m.Success then Some m else None)
            |> function
                | None -> failwith "Couldn't find version in changelog file"
                | Some m ->
                    m.Groups.[1].Value

    let isPreRelease (version : string) =
        let regex = Regex(".*(alpha|beta|rc).*", RegexOptions.IgnoreCase)
        regex.IsMatch(version)

    let getNotes(changelogPath : string) (version : string) =
        File.ReadLines(changelogPath)
        |> Seq.skipWhile(fun line ->
            let m = versionRegex.Match(line)

            if m.Success then
                (m.Groups.[1].Value <> version)
            else
                true
        )
        // Remove the version line
        |> Seq.skip 1
        // Take all until the next version line
        |> Seq.takeWhile (fun line ->
            let m = versionRegex.Match(line)
            not m.Success
        )

    let needsPublishing (versionRegex: Regex) (newVersion: string) projFile =
        printfn "Project: %s" projFile
        if newVersion.ToUpper().EndsWith("NEXT")
            || newVersion.ToUpper().EndsWith("UNRELEASED")
        then
            Trace.traceImportantfn "Version marked as unreleased version in Changelog, don't publish yet."
            false
        else
            File.ReadLines(projFile)
            |> Seq.tryPick (fun line ->
                let m = versionRegex.Match(line)
                if m.Success then Some m else None)
            |> function
                | None -> failwith "Couldn't find version in project file"
                | Some m ->
                    let sameVersion = m.Groups.[1].Value = newVersion
                    if sameVersion then
                        Trace.traceImportantfn "Already version %s, no need to publish." newVersion
                    not sameVersion

let pushNuget (newVersion: string) (projFile: string) =
    let versionRegex = Regex("<Version>(.*?)</Version>", RegexOptions.IgnoreCase)

    let nugetKey =
        match Environment.environVarOrNone "NUGET_KEY" with
        | Some nugetKey -> nugetKey
        | None -> failwith "The Nuget API key must be set in a NUGET_KEY environmental variable"

    let needsPublishing = Changelog.needsPublishing versionRegex newVersion projFile

    if needsPublishing then

        (versionRegex, projFile) ||> Util.replaceLines (fun line _ ->
            versionRegex.Replace(line, "<Version>" + newVersion + "</Version>") |> Some)

        DotNet.pack (fun p ->
                { p with
                    Configuration = DotNet.BuildConfiguration.Release
                }
            )
            projFile

        let projDir = Path.GetDirectoryName(projFile)

        let nupkg =
            Directory.GetFiles(projDir </> "bin" </> "Release", "*.nupkg")
            |> Array.find (fun nupkg -> nupkg.Contains(newVersion))

    // if needsPublishing then
    //     DotNet.nugetPush (fun p ->
    //             { p with
    //                 PushParams =
    //                     { p.PushParams with
    //                         ApiKey = Some nugetKey
    //                         Source = Some "nuget.org"
    //                     }
    //              }
    //         ) nupkg
        ()

let clean = BuildTask.create "Clean" [ ] {
    !! "minimal/**/bin"
    ++ "minimal/**/obj"
    |> Shell.cleanDirs
}

let _release = BuildTask.create "Release" [ clean ] {
    [
        "minimal"
    ]
    |> List.iter (fun template ->
        let changelogPath = template </> "CHANGELOG.md"
        let version = Changelog.getLastVersion changelogPath
        let projFile = !! (template </> "*.proj") |> Seq.head
        
        pushNuget version projFile
    )

    Git.Staging.stageAll CWD
    Git.Commit.exec CWD "Release new versions"
    Git.Branches.push CWD
}

BuildTask.runOrList ()