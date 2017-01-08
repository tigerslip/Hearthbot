module MashapeHSApi

open HearthbotCommands
open SlackFormatter
open FSharp.Data
open Data
open Newtonsoft.Json

let cardsEndpoint = 
    "https://omgvamp-hearthstone-v1.p.mashape.com/cards/"

let fromJson<'a> json =
  JsonConvert.DeserializeObject<'a>(json)

let httpRequest token url = 
    Http.RequestString ( url, headers = ["X-Mashape-Key", token; HttpRequestHeaders.Accept HttpContentTypes.Json])

let Get token card = 
    sprintf "%s/%s" cardsEndpoint card
    |> httpRequest token
    |> fromJson<Card list>

let Search token query = 
    sprintf "%s/%s/%s" cardsEndpoint "search" query
    |> httpRequest token
    |> fromJson<Card list>