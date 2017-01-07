#r "./packages/FAKE/tools/FakeLib.dll"

open System
open Fake
open Fake.Testing

let buildDir  = "./build/"
let testDir = "./tests/"
let deployDir = "../wwwroot/"
let appReferences  =
    !! "/**/*.csproj"
    ++ "/**/*.fsproj"

let version = "0.1"  // or retrieve from CI server

Target "Clean" (fun _ ->
    CleanDirs [buildDir; testDir; deployDir]
)

Target "Build" (fun _ ->
    // compile all projects below src/app/
    MSBuildDebug buildDir "Build" appReferences
    |> Log "AppBuild-Output: "
)

Target "AzureDeploy" (fun _ -> 
    let buildFiles = !! (sprintf "%s*" buildDir)
    CopyFiles deployDir buildFiles
    )

// Build order
"Clean"
  ==> "Build"
  ==> "AzureDeploy"

// start build
RunTargetOrDefault "AzureDeploy"