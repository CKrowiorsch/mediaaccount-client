$erroractionpreference = "Stop"

properties {
  $location = (get-location);
  $outdir = (join-path $location "Build");
  $bindir = (join-path $outdir "Bin");
}

task default -depends help
task ci -depends build

task help {
  write-host "Buildscript for MediaAccount client"
  write-host "Tasks: rebuild"
}

task clean {
  [void](rmdir -force -recurse $outdir -ea SilentlyContinue)
}

task build -depends clean {
  dotnet build -o "$bindir/MediaAccount.Client" -c Release "Source/MediaAccount.Client/MediaAccount.Client.csproj"
}

task pack -depends clean {
  dotnet pack -o "$bindir/MediaAccount.Client" -c Release "Source/MediaAccount.Client/MediaAccount.Client.csproj"
}