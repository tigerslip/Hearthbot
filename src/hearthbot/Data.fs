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

type Card = {
    artist:string
    attack:int
    cardId:string
    cardSet:string
    collectible:bool
    cost:int
    elite:bool
    faction:string
    flavor:string
    health:int
    img:string
    imgGold:string
    locale:string
    name:string
    playerClass:string
    rarity:string
    text:string
    ``type``:string
}