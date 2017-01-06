#r "../../packages/suave/lib/net40/suave.dll"
#r "../../build/hearthbot.exe"

open System
open Suave                 // always open suave
open Suave.Successful      // for OK-result
open Suave.Web             // for config
open Suave.Operators
open Suave.Filters
open Hearthbot.Core.HearthbotCommandParser
open Hearthbot.Core.HearthstoneApi

let routeParseResult command = 
    match command with
        | Some(cmd) -> Query "TVEvw4MKnumshTNOevm3Svrbmkqgp1ukSh5jsn5CDa3g5x6GLM" cmd
        | None -> "Could not parse hearthbot request"

let run str : string = 
    str
    |> Parse
    |> routeParseResult

let getBody req = 
    let getString rawForm = 
        System.Text.Encoding.UTF8.GetString(rawForm)

    req.rawForm |> getString

let app : WebPart = POST >=> path "/hearthbot" >=> request (getBody >> run >> OK)

startWebServer defaultConfig app