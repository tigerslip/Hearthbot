module SlackFormatter

open Data
open System
open FSharp.Data
open FSharp.Collections

let rec last n xs =
  if List.length xs <= n then xs
  else last n xs.Tail

let FormatCard card = 
    sprintf "%s\n%s" card.name card.img

let FormatCards cards = 

    cards 
    |> List.map FormatCard
    |> String.concat "\n"
