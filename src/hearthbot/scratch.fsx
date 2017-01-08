#r "../../packages/suave/lib/net40/suave.dll"
#r "../../build/hearthbot.exe"
#r "../../packages/FSharp.Data/lib/net40/FSharp.Data.dll"
#r "../../packages/newtonsoft.json/lib/net40/newtonsoft.json.dll"

open FSharp.Data
open Newtonsoft.Json
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

let config =
    { defaultConfig with
          bindings = [ HttpBinding.mk HTTP IPAddress.Loopback (uint16 8083) ]
          listenTimeout = TimeSpan.FromMilliseconds 3000. }

let getBody (req:HttpRequest) = 

    match req.formData "text" with
      | Choice1Of2(other) -> other
      | Choice2Of2(text) -> text

let app : WebPart = POST >=> request (getBody >> RouteRequest >> OK)

startWebServer config app