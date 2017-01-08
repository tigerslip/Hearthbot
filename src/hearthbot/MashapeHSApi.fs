module MashapeHSApi

open HearthbotCommands
open SlackFormatter
open FSharp.Data
open Data
open Newtonsoft.Json

let fromJson<'a> json =
  JsonConvert.DeserializeObject<'a>(json)

let Query key command = 

    let request key command term = 
        let url = sprintf "https://omgvamp-hearthstone-v1.p.mashape.com/cards/%s/%s" command term
        let json = Http.RequestString  ( url, headers = [ "X-Mashape-Key", key])
        fromJson<Card list> json

    match command with 
    | Search(search) -> request key "search" search.searchTerm
    | Get(get) -> request key "cards" get.card