module Data

open FSharp.Data

type GetCommand = {
    card:string
}

type SearchCommand = {
    searchTerm:string
}

type HearthBotCommand = 
    | Get of GetCommand
    | Search of SearchCommand

type Cards = JsonProvider<"./sampleCardSearch.json", EmbeddedResource="heartbot, sampleCardSearch.json">
type Card = JsonProvider<"./sampleCard.json", EmbeddedResource="heartbot, sampleCard.json">