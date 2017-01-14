module HearthbotCommands 

open FParsec
open Data

let Parse str = 

    let psearch = pstringCI "search"

    let pget = pstringCI "get"

    let pcommand = spaces >>. (psearch <|> pget) .>> spaces

    let pcardname = restOfLine false |>> (fun name -> Get {card = name; golden = false})

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