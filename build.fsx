#r "./packages/FAKE/tools/FakeLib.dll"

open Fake
open Fake.Testing

let buildDir  = "./build/"
let testDir = "./tests/"

let appReferences  =
    !! "/**/*.csproj"
    ++ "/**/*.fsproj"

let version = "0.1"  // or retrieve from CI server

Target "Clean" (fun _ ->
    CleanDirs [buildDir; testDir]
)

Target "Build" (fun _ ->
    // compile all projects below src/app/
    MSBuildDebug buildDir "Build" appReferences
    |> Log "AppBuild-Output: "
)

// Build order
"Clean"
  ==> "Build"

// start build
RunTargetOrDefault "Build"