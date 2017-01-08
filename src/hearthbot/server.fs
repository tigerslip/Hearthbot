open Suave
open Suave.Successful
open Suave.Web
open Suave.Operators
open Suave.Filters
open HearthbotCommands
open MashapeHSApi
open System.Net
open System
open Pipeline

[<EntryPoint>]
let main [| port |] =
    let config =
        { defaultConfig with
              bindings = [ HttpBinding.mk HTTP IPAddress.Loopback (uint16 port) ]
              listenTimeout = TimeSpan.FromMilliseconds 3000. }

    let getBody (req:HttpRequest) = 

        match req.formData "text" with
         | Choice1Of2(other) -> other
         | Choice2Of2(text) -> text

    let app : WebPart = POST >=> request (getBody >> pipeRequest >> OK)

    startWebServer config app
    0