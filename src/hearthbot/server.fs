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

    let routeParseResult command = 
        match command with
            | Some(cmd) -> Query "TVEvw4MKnumshTNOevm3Svrbmkqgp1ukSh5jsn5CDa3g5x6GLM" cmd
            | None -> "Could not parse hearthbot request"

    let run str : string = 
        printfn "body is %s" str
        str
        |> Parse
        |> routeParseResult

    let getBody req = 
        let getString rawForm = 
            System.Text.Encoding.UTF8.GetString(rawForm)

        req.rawForm |> getString

    let app : WebPart = POST >=> request (getBody >> run >> OK)

    startWebServer config app
    0