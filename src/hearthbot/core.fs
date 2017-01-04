namespace Hearthbot.Data

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

    let query command key = 

        let request key command term = 
            let url = sprintf "https://omgvamp-hearthstone-v1.p.mashape.com/cards/%s/%s" command term
            Http.RequestString  ( url, headers = [ "X-Mashape-Key", key])

        match command with 
        | Search(search) -> request "search" search.searchTerm key
        | Get(get) -> request "cards" get.card key

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
            | Success(result, _, _)   -> printfn "Success: %A" result
            | Failure(errorMsg, _, _) -> printfn "Failure: %s" errorMsg