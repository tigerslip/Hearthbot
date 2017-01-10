module CommandRouter

open Data
open HearthbotCommands
open MashapeHSApi
open SlackFormatter

let RouteRequest token raw  = 

    let routeCmd cmd : string = 
        match cmd with 
         | Search(search) -> 
            Search token search.searchTerm 
            |> FormatCards
         | Get(get) -> 
            Get token get.card 
            |> FormatCards

    let handleCmd command = 
        match command with
            | Some(cmd) -> routeCmd cmd
            | None -> sprintf "Could not parse hearthbot request: %s" raw

    try
        raw 
        |> Parse 
        |> handleCmd
    with
     | :? System.Exception as ex -> "Unable to process your request. try get %card% or search %card%"