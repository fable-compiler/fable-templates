module App

open Node
open Node.http2
open Node.net
open Fable.Core
open Fable.Core.JsInterop

let noObj: obj option = None
let server = http2.createSecureServer(jsOptions<SecureServerOptions>(fun opt ->
    opt.key <- fs.readFileSync(PathLike.ofString "localhost-privkey.pem" |> U2.Case1, noObj) |> U4.Case3 |> Some
    opt.cert <- fs.readFileSync(PathLike.ofString "localhost-cert.pem" |> U2.Case1, noObj) |> U4.Case3 |> Some
))
server.on_error (fun err -> printfn "%A" err) |> ignore
server.on_socketError (fun err -> printfn "%A" err) |> ignore

server.on_stream (fun stream _inHeaders _ ->
    let outHeaders = createEmpty<OutgoingHttpHeaders>
    outHeaders.["content-type"] <- "text/html" |> U3.Case2 |> Some
    outHeaders.[":status"] <- 200. |> U3.Case1 |> Some
    stream.respond outHeaders
    let wstream = stream :> stream.Writable
    wstream.``end``("<h1>HTTP/2 from F# !!!</h1>", "utf8")
) |> ignore

server.listen(jsOptions<ListenOptions>(fun opt ->
    opt.port <- 8443. |> Some
)) |> ignore
