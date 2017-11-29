module Extension

open VSCode.Vscode

let activate(context : ExtensionContext) =
    printfn "Congratulations, your extension is now active!"

    commands.registerCommand("extension.sayHello", fun _ ->
        window.showInformationMessage("Hello, World from F#!", ResizeArray []) |> ignore
        printfn "Let's log something to the console here too."
        None
    )
    |> context.subscriptions.Add