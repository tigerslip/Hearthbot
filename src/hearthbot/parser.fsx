#r "../../packages/fparsec/lib/portable-net45+netcore45+wpa81+wp8/FParsecCS.dll"
#r "../../packages/fparsec/lib/portable-net45+netcore45+wpa81+wp8/FParsec.dll"

open System
open FParsec

let test p str =
    match run p str with
    | Success(result, _, _)   -> printfn "Success: %A" result
    | Failure(errorMsg, _, _) -> printfn "Failure: %s" errorMsg

let psearch = pstringCI "search"
let pget = pstringCI "get"

let pcommand = psearch <|> pget |>> id

test pcommand "bob"