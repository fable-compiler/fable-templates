module App

// a port of the last example: Yargs is here to help you...
// https://github.com/yargs/yargs/blob/master/docs/examples.md#yargs-is-here-to-help-you
// It could the number of lines in a file.

open Yargs
open Fable.Core
open Fable.Core.JsInterop
open System.Text.RegularExpressions

let argv =
    yargs
        .usage("Usage: $0 <command> [options]")
        .command(!^"count", "Count the lines in a file")
        .example("$0 count -f foo.js", "count the lines in the given file")
        .alias("f", !^"file")
        .nargs("f", 1.)
        .describe(!^"f", "Load a file")
        .demandOption(!^"f", "Please specify a file.")
        .help("h")
        .alias("h", !^"help")
        .epilog("copyright 2015")
        .argv

let s = Node.fs.createReadStream(Node.Fs.PathLike.ofString(argv.["file"].Value :?> string))

let mutable lines = 1

s.on_data(fun buf ->
    let s = 
        match buf with
        | U2.Case1 b -> b.toString()
        | U2.Case2 s -> s
    lines <- lines + Regex.Matches(s, "(\n)").Count
) |> ignore

s.on_end(fun _ ->
    printfn "%d" lines
) |> ignore