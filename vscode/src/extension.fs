module Extension

open Fable.Core
open Fable.Core.JsInterop
open Fable.Import.vscode
open System

let activate(context : ExtensionContext) =

  let sayHello () = window.showInformationMessage("Hello, World!") |> ignore

  let disposable = commands.registerCommand("extension.sayHello", sayHello |> unbox<Func<obj,obj>>)
  
  context.subscriptions.Add(disposable)