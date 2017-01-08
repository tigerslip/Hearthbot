module Pipeline

open Data
open HearthbotCommands
open MashapeHSApi
open SlackFormatter

let pipeRequest raw = 

    let queryApi cmd = 
        let cards = Query "TVEvw4MKnumshTNOevm3Svrbmkqgp1ukSh5jsn5CDa3g5x6GLM" cmd
        FormatCards cards

    let routeParseResult str command = 
        match command with
            | Some(cmd) -> queryApi cmd
            | None -> sprintf "Could not parse hearthbot request: %s" str

    raw
    |> Parse
    |> routeParseResult raw