module App

open Node
open Node.Fs
open Node.Http2
open Node.Net
open Fable.Core
open Fable.Core.JsInterop

let server = http2.createSecureServer(jsOptions<SecureServerOptions>(fun opt ->
    let privkey = PathLike.ofString "localhost-privkey.pem"
    let cert = PathLike.ofString "localhost-cert.pem"
    let noObj: obj option = None // https://github.com/fable-compiler/ts2fable/issues/109
    opt.key <- fs.readFileSync(!^privkey, noObj) |> U4.Case3 |> Some
    opt.cert <- fs.readFileSync(!^cert, noObj) |> U4.Case3 |> Some
))

server.on_error (fun err -> printfn "%A" err) |> ignore
server.on_socketError (fun err -> printfn "%A" err) |> ignore

server.on_stream (fun stream _inHeaders _ ->
    let outHeaders = createEmpty<OutgoingHttpHeaders>
    outHeaders.["content-type"] <- Some !^"text/html"
    outHeaders.[":status"] <- Some !^200.
    stream.respond outHeaders
    stream.``end``("<h1>HTTP/2 from F# !!!</h1>", "utf8")
) |> ignore

server.listen(jsOptions<ListenOptions>(fun opt ->
    opt.port <- Some 8443.
)) |> ignore