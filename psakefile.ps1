Framework('4.7.1')
$erroractionpreference = "Stop"

properties {
  $location = (get-location);
  $outdir = (join-path $location "Build");
  $artifactsdir = (join-path $outdir "Artifacts");
  $bindir = (join-path $outdir "Bin");
}

task default -depends help
task ci -depends rebuild,build-tests,create-nuget


task help {
	write-host "Buildscript for MediaAccount client"
	write-host "Tasks: rebuild"
}

task clean {
  [void](rmdir -force -recurse $outdir -ea SilentlyContinue)
}

task create-nuget -depends rebuild {
  push-location "$bindir/"
  copy "$location/MediaAccount.Client.nuspec" $bindir
  $version = ([System.Diagnostics.FileVersionInfo]::GetVersionInfo("$bindir\MediaAccount.Client\MediaAccount.Client.dll").productVersion);

  # create standardpackage
  exec { ..\..\.NuGet\NuGet.exe pack "MediaAccount.Client.nuspec" /version "$version" }
  pop-location
}

task nuget-restore {
  exec { .nuget\nuget.exe restore }
}

task rebuild -depends clean,nuget-restore {
  $solution = get-location;
  exec { msbuild /nologo /v:minimal /t:rebuild /p:"Configuration=Release;OutputPath=$bindir/MediaAccount.Client/;SolutionDir=$solution/" "Source/MediaAccount.Client/MediaAccount.Client.csproj" }
}

task build-tests -depends clean,nuget-restore {
  $solution = get-location;
  exec { msbuild /nologo /v:minimal /t:rebuild /p:"Configuration=Release;OutputPath=$bindir/MediaAccount.Client.Tests/;SolutionDir=$solution/" "Source/MediaAccount.Client.Tests/MediaAccount.Client.Tests.csproj" }
}
