#r "./packages/FAKE/tools/FakeLib.dll"

open Fake
open Fake.Testing

let buildDir  = "./build/"
let testDir = "./tests/"
let deployDir = "./deploy/"

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
    let buildFiles = !! "./build/*"
    CopyFiles "./deploy" buildFiles
    )

// Build order
"Clean"
  ==> "Build"
  ==> "AzureDeploy"

// start build
RunTargetOrDefault "Build"