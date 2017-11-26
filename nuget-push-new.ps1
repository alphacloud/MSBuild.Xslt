# Push if current version does not exists on nuget. Expects package to be in current folder.
# Taken from https://github.com/NuGet/Home/issues/1630#issuecomment-308382165
param([String]$packageName,[String]$apiKey, [String]$source)

$latestrelease = nuget.exe list packageid:$packageName -allversions -source $source | Select-String -Pattern $packageName

if($latestrelease -eq $null)
{
  $srvpkg = $null
}
else 
{
  $srvpkg = $latestrelease.Line.split(" ")[0] + "." + $latestrelease.Line.split(" ")[1] + ".nupkg"
}

$newpkg = get-childitem -path . | where {$_.Name -Match $packageName + "\.\d" } | where {$_.Name -NotMatch "symbols.nupkg" }
Write-Host ("Server version: " + $srvpkg + ", currentVersion: "+ $newPkg.Name)

if($srvpkg -ieq $newpkg.Name)
{
  Write-Host("Skip pushing " + $newpkg + " as it already exist on server")
}
else 
{
  nuget.exe push $newpkg.FullName -source $source -ApiKey $apiKey
  Write-Host("Pushed " + $newpkg.Name)
}
