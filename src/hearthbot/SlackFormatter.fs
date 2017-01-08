module SlackFormatter

open Data
open System
open FSharp.Data
open FSharp.Collections

let rec last n xs =
  if List.length xs <= n then xs
  else last n xs.Tail


let formatCards (cards:string) = 

    let printCard card = 
        sprintf "%s %s" card.name card.cost

    cards
    |> Cards.Parse
    |> Array.map printCard