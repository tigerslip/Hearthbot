// include Fake libs
#r "./packages/FAKE/tools/FakeLib.dll"

open Fake
open Fake.Testing

// Directories
let buildDir  = "./build/"
let deployDir = "./deploy/"
let testDir = "./tests/"

// Filesets
let appReferences  =
    !! "/**/*.csproj"
    ++ "/**/*.fsproj"

// version info
let version = "0.1"  // or retrieve from CI server

// Targets
Target "Clean" (fun _ ->
    CleanDirs [buildDir; deployDir; testDir]
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

Target "Deploy" (fun _ ->
    !! (buildDir + "/**/*.*")
    -- "*.zip"
    |> Zip buildDir (deployDir + "ApplicationName." + version + ".zip")
)

// Build order
"Clean"
  ==> "Build"
  ==> "NUnitTest"
  ==> "Deploy"

// start build
RunTargetOrDefault "NUnitTest"
