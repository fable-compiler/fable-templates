module Renderer

open Fable.Core
open Fable.Core.JsInterop
open Fable.Import
open Electron
open Node.Exports

let body = Browser.document.getElementsByTagName_h1().[0]
body.textContent <- "Hello World!"
