$erroractionpreference = "Stop"

properties {
  $location = (get-location);
  $outdir = (join-path $location "Build");
  $artifactsdir = (join-path $outdir "Artifacts");
  $bindir = (join-path $outdir "Bin");
}

task default -depends help
task ci -depends rebuild,build-tests

task help {
	write-host "Buildscript for MediaAccount client"
	write-host "Tasks: rebuild"
}

task clean {
  [void](rmdir -force -recurse $outdir -ea SilentlyContinue)
}

task rebuild -depends clean {
  dotnet --list-runtimes
  dotnet --list-sdks
  
  dotnet build -c Release "Source/MediaAccount.Client/MediaAccount.Client.csproj" -nologo
  dotnet pack -o "$bindir/MediaAccount.Client" -c Release "Source/MediaAccount.Client/MediaAccount.Client.csproj"
}

task build-tests -depends clean {
  dotnet build -c Release -o "$bindir/MediaAccount.Client.Tests" "Source/MediaAccount.Client.Tests/MediaAccount.Client.Tests.csproj" -nologo
}
