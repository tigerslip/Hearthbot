module HearthbotCommands 

open FParsec
open Data

let Parse str = 

    let psearch = pstringCI "search"

    let pget = pstringCI "get"

    let pcommand = spaces >>. (psearch <|> pget) .>> spaces

    let pcard = manyCharsTill (noneOf "-") eof

    let pcardname = pcard |>> (fun name -> Get {card = name; golden = false})

    let psearchTerm = restOfLine false |>> (fun term -> Search {searchTerm = term})

    let getOrSearch str = 
        match str with
        | "get" -> pcardname
        | "search" -> psearchTerm
        | _ -> fail "command should be get or search"

    let pcommandstring = pcommand >>= getOrSearch

    let printAndNone (error:string) : HearthBotCommand option = 
        let a = sprintf "%s" error
        None

    match run pcommandstring str with
        | Success(result, _, _)   -> Some(result)
        | Failure(errorMsg, _, _) -> printAndNone errorMsg
