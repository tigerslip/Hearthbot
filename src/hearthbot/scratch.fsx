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

let getBody req = 
    let getString rawForm = 
        System.Text.Encoding.UTF8.GetString(rawForm)

    req.rawForm |> getString

let app : WebPart = POST >=> request (getBody >> printthis >> OK)

startWebServer defaultConfig app