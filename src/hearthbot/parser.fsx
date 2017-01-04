#r "../../packages/fparsec/lib/portable-net45+netcore45+wpa81+wp8/FParsecCS.dll"
#r "../../packages/fparsec/lib/portable-net45+netcore45+wpa81+wp8/FParsec.dll"

open System
open FParsec

type GetCommand = {
    card:string
}

type SearchCommand = {
    searchTerm:string
}

type HearthBotCommand = 
    | Get of GetCommand
    | Search of SearchCommand

let test p str =
    match run p str with
    | Success(result, _, _)   -> printfn "Success: %A" result
    | Failure(errorMsg, _, _) -> printfn "Failure: %s" errorMsg

let psearch = pstringCI "search"
let pget = pstringCI "get"
let pcommand = spaces >>. (psearch <|> pget) .>> spaces

test pcommand " get "

let pcardname = restOfLine false |>> (fun name -> Get {card = name})

let psearchTerm = restOfLine false |>> (fun term -> Search {searchTerm = term})

test pcardname "lord of bones"

let getOrSearch str = 
    match str with
     | "get" -> pcardname
     | "search" -> psearchTerm
     | _ -> fail "hearthbot can only get or search"

let pcommandstring = pcommand >>= getOrSearch

test pcommandstring "search bob of bob"

test pcommandstring "do something else"