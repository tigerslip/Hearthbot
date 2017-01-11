module Tests

open Expecto
open Data
open HearthbotCommands

[<Tests>]
let tests =
  testList "parsing" [

    testCase "parsing commands from slack" <| fun _ -> 
      let command = "search ysera"
      let parsed = Parse command
      let expected = {searchTerm = "ysera"}

      match parsed with
        | Some(cmd) -> 
            match cmd with
             | Search(srch) -> Expect.equal srch.searchTerm "ysera" "is a command to search for ysera"
             | _ -> failtest "should have been a search cmd"
        | None -> failtest "should be a result"
  ]