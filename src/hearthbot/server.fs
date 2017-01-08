open Suave
open Suave.Successful
open Suave.Web
open Suave.Operators
open Suave.Filters
open HearthbotCommands
open MashapeHSApi
open System.Net
open System

[<EntryPoint>]
let main [| port |] =
    let config =
        { defaultConfig with
              bindings = [ HttpBinding.mk HTTP IPAddress.Loopback (uint16 port) ]
              listenTimeout = TimeSpan.FromMilliseconds 3000. }

    let routeParseResult str command = 
        match command with
            | Some(cmd) -> Query "TVEvw4MKnumshTNOevm3Svrbmkqgp1ukSh5jsn5CDa3g5x6GLM" cmd
            | None -> sprintf "Could not parse hearthbot request: %s" str

    let run str : string = 
        str
        |> Parse
        |> routeParseResult str

    let getBody (req:HttpRequest) = 

        match req.formData "text" with
         | Choice1Of2(other) -> other
         | Choice2Of2(text) -> text

    let app : WebPart = POST >=> request (getBody >> run >> OK)

    startWebServer config app
    0