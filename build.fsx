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

let testDlls = !! (buildDir + "/*.tests.dll")

Target "NUnitTest" (fun _ ->
        testDlls
        |> NUnit3 (fun p ->
            {p with
                OutputDir = testDir + "TestResults.xml"
                ToolPath = "C:/Program Files (x86)/NUnit.org/nunit-console/nunit3-console.exe"})
)

// Build order
"Clean"
  ==> "Build"
  ==> "NUnitTest"

// start build
RunTargetOrDefault "NUnitTest"