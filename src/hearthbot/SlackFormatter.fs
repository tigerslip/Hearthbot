module SlackFormatter

open Data
open System
open FSharp.Data
open FSharp.Collections
open Newtonsoft.Json

let serialize<'a> x = 
    JsonConvert.SerializeObject(x)

let FormatCard card = 
    sprintf "%s\n%s" card.name card.img

let cardsToSlackResponse (cards:Card list) : SlackResponse = 
    
   (* let cardToAttachment card = 
        {
            fallback = "";
            color = "";
            pretext = "";
            author_name = "";
            author_icon = "";
            author_link = "";
            title = "";
            title_link = "";
            text = "";
            image_url = card.img;
            thumb_url = "";
            footer = "";
            footer_icon = "";
            ts = 0
        }*)
    
    let imgUrls = 
        cards
        |> Seq.truncate 3
        |> Seq.map (fun c -> c.img)
        |> Seq.toArray

    let text = String.Join("\n", cards)
    
(*    let attachments = 
        cards 
        |> Seq.truncate 3
        |> Seq.map cardToAttachment
        |> Seq.toArray*)

    {text = text; username = "hearthbot"; mrkdwn = false; unfurl_links = true; attachments = Array.empty}

let FormatCards cards = 
    cards 
    |> cardsToSlackResponse
    |> serialize
