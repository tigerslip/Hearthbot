namespace Hearthbot.Core

type GetCommand = {
    card:string
}

type SearchCommand = {
    searchTerm:string
}

type HearthBotCommand = 
    | Get of GetCommand
    | Search of SearchCommand

module HearthstoneApi = 

    open FSharp.Data

    let Query key command = 

        let request key command term = 
            printfn "querying mashape with command %s and term %s" command term
            let url = sprintf "https://omgvamp-hearthstone-v1.p.mashape.com/cards/%s/%s" command term
            Http.RequestString  ( url, headers = [ "X-Mashape-Key", key])

        match command with 
        | Search(search) -> request key "search" search.searchTerm
        | Get(get) -> request key "cards" get.card

module HearthbotCommandParser = 

    open FParsec

    let Parse str = 

        let psearch = pstringCI "search"

        let pget = pstringCI "get"

        let pcommand = spaces >>. (psearch <|> pget) .>> spaces

        let pcardname = restOfLine false |>> (fun name -> Get {card = name})

        let psearchTerm = restOfLine false |>> (fun term -> Search {searchTerm = term})

        let getOrSearch str = 
            match str with
            | "get" -> pcardname
            | "search" -> psearchTerm
            | _ -> fail "command should be get or search"

        let pcommandstring = pcommand >>= getOrSearch

        match run pcommandstring str with
            | Success(result, _, _)   -> Some(result)
            | Failure(errorMsg, _, _) -> None