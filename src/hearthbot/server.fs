open Suave
open Suave.Successful
open Suave.Web
open Suave.Operators
open Suave.Filters
open HearthbotCommands
open MashapeHSApi
open System.Net
open System
open CommandRouter

[<EntryPoint>]
let main [| port; token |] =
    let config =
        { defaultConfig with
              bindings = [ HttpBinding.mk HTTP IPAddress.Loopback (uint16 port) ]
              listenTimeout = TimeSpan.FromMilliseconds 3000. }

    let routeRequest = RouteRequest token

    let getBody (req:HttpRequest) = 

        match req.formData "text" with
         | Choice1Of2(other) -> other
         | Choice2Of2(text) -> text

    let app : WebPart = 
        POST 
        >=> request (getBody >> routeRequest >> OK) 
        >=> Writers.setMimeType "application/json; charset=utf-8"

    startWebServer config app
    0