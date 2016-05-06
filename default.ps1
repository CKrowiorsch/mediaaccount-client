Framework('4.5.1')
$erroractionpreference = "Stop"

properties {
  $location = (get-location);
  $outdir = (join-path $location "Build");
  $artifactsdir = (join-path $outdir "Artifacts");
  $bindir = (join-path $outdir "Bin");
}

task default -depends help


task help {
	write-host "Buildscript for MediaAccount client"
	write-host "Tasks: rebuild"
}

task clean {
  [void](rmdir -force -recurse $outdir -ea SilentlyContinue)
}

task nuget-restore {
  exec { .nuget\nuget.exe restore }
}

task rebuild -depends clean,nuget-restore {
  $solution = get-location;
  exec { msbuild /nologo /v:minimal /t:rebuild /p:"Configuration=Release;OutputPath=$bindir/MediaAccount.Client/;SolutionDir=$solution/" "Source/MediaAccount.Client/MediaAccount.Client.csproj" }
}
