$erroractionpreference = "Stop"

properties {
  $location = (get-location)
  $outdir = (join-path $location "Build")
  $bindir = (join-path $outdir "Bin")
}

task default -depends help
task ci -depends build,pack

task help {
  Write-Output "Buildscript for MediaAccount client"
  Write-Output "Tasks: rebuild"
}

task clean {
  [void](Remove-Item -force -recurse $outdir -ea SilentlyContinue)
}

task build -depends clean {
  dotnet build -o "$bindir/MediaAccount.Client" -c Release "Source/MediaAccount.Client/MediaAccount.Client.csproj"
}

task pack -depends clean {
  dotnet pack -o "$bindir/MediaAccount.Client" -c Release "Source/MediaAccount.Client/MediaAccount.Client.csproj"
}