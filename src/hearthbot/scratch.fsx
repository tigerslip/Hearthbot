#r "../../packages/fsharp.data/lib/net40/FSharp.Data.dll"

open System
open FSharp.Data

Http.RequestString
  ( "https://omgvamp-hearthstone-v1.p.mashape.com/cards/Arcane Giantz",
    headers = [ "X-Mashape-Key", "TVEvw4MKnumshTNOevm3Svrbmkqgp1ukSh5jsn5CDa3g5x6GLM"; 
                "Accept", "application/json" ])

Http.RequestString
  ( "https://omgvamp-hearthstone-v1.p.mashape.com/cards/search/Doom!",
    headers = [ "X-Mashape-Key", "TVEvw4MKnumshTNOevm3Svrbmkqgp1ukSh5jsn5CDa3g5x6GLM" ])

Http.RequestString
  ( "https://omgvamp-hearthstone-v1.p.mashape.com/cards/search/Arcane Boober",
    headers = [ "X-Mashape-Key", "TVEvw4MKnumshTNOevm3Svrbmkqgp1ukSh5jsn5CDa3g5x6GLM" ])

let request key command term = 
    let url = sprintf "https://omgvamp-hearthstone-v1.p.mashape.com/cards/%s/%s" command term
    Http.RequestString  ( url, headers = [ "X-Mashape-Key", key])

type GetCommand = {
    card:string
}

type SearchCommand = {
    searchTerm:string
}

type HearthBotCommand = 
    | Get of GetCommand
    | Search of SearchCommand

let hitApi command key = 
    match command with 
     | Search(search) -> request "search" search.searchTerm key
     | Get(get) -> request "cards" get.card key

request "search" "arcane giant" "TVEvw4MKnumshTNOevm3Svrbmkqgp1ukSh5jsn5CDa3g5x6GLM"