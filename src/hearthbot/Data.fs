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

type SlackAttachment = {
    fallback:string // fallback text
    color:string // e.g. "#36a64f"
    pretext:string // "Optional text that appears above the attachment block",
    author_name:string // "Bobby Tables",
    author_link:string // "http://flickr.com/bobby/",
    author_icon:string // "http://flickr.com/icons/bobby.jpg",
    title:string // "Slack API Documentation",
    title_link:string // "https://api.slack.com/",
    text:string // "Optional text that appears within the attachment",
    image_url:string // "http://my-website.com/path/to/image.jpg",
    thumb_url:string // "http://example.com/path/to/thumb.png",
    footer:string // "Slack API",
    footer_icon:string // "https://platform.slack-edge.com/img/default_application_icon.png",
    ts:int // 123456789
}

type SlackResponse = {
    text:string
    attachments:SlackAttachment array
}