#r "../../packages/suave/lib/net40/suave.dll"

open System
open Suave                 // always open suave
open Suave.Successful      // for OK-result
open Suave.Web             // for config
open Suave.Operators
open Suave.Filters

let printthis str  = 
    printfn "%s" str
    str
let huh what = 
    what

let app : WebPart = 
    choose
        [ 
            POST >=> choose
                //[ path "/hearthbot" >=> request (fun req -> add (printthis <| req.ToString()) ; OK "") ]
                [ path "/hearthbot" >=> request (fun req -> add ; OK "") ]
        ]

startWebServer defaultConfig app