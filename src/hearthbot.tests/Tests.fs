module Tests

open Expecto
open Data
open HearthbotCommands

[<Tests>]
let tests =
  testList "parsing" [

    testCase "a simple search command" <| fun _ -> 
      let command = "search ysera"
      let expected = {searchTerm = "ysera"}

      match Parse command with
        | Some(cmd) -> 
            match cmd with
             | Search(srch) -> Expect.equal srch.searchTerm "ysera" "is a command to search for ysera"
             | _ -> failtest "should have been a search cmd"
        | None -> failtest "should be a result"

    testCase "a simple get command" <| fun _ -> 
      let command = "get ysera"
      let expected = {card = "ysera"; golden = false}

      match Parse command with
        | Some(cmd) -> 
            match cmd with
             | Get(get) -> Expect.equal get.card "ysera" "should be get ysera"
             | _ -> failtest "should have been a get cmd"
        | None -> failtest "should be a result"

    testCase "get with -g asks for the golden card" <| fun _ -> 
      let command = "get ysera -g"
      let expected = {card = "ysera"; golden = true}

      match Parse command with
        | Some(cmd) -> 
            match cmd with
             | Get(get) -> Expect.equal get.golden true "should be get golden ysera"
             | _ -> failtest "should have been a get gold card cmd"
        | None -> failtest "should be a result"
  ]