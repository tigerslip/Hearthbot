#r "../../packages/suave/lib/net40/suave.dll"
#r "../../build/hearthbot.exe"
#r "../../packages/FSharp.Data/lib/net40/FSharp.Data.dll"

open FSharp.Data

let sample = Cards.GetSamples()

Array.take 2 sample
let first = Array.item 0 sample