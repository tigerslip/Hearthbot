module MashapeHSApi

open FSharp.Data
open HearthbotCommands

let Query key command = 

    let request key command term = 
        let url = sprintf "https://omgvamp-hearthstone-v1.p.mashape.com/cards/%s/%s" command term
        Http.RequestString  ( url, headers = [ "X-Mashape-Key", key])

    match command with 
    | Search(search) -> request key "search" search.searchTerm
    | Get(get) -> request key "cards" get.card