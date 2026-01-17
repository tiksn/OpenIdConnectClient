param(
    # Build Version
    [Parameter()]
    [string]
    $Version,
    # Build Instance
    [Parameter()]
    [string]
    $Instance
)

Exit-Build {
    [PSCustomObject]@{
        TrashFolder = $script:trashFolder
        NextVersion = $Script:NextVersion
    }
}

# Synopsis: Initialize
Task Init {
    $date = Get-Date -Format 'yyyyMMddHHmmss'
    $trashFolder = Join-Path -Path . -ChildPath '.trash'
    $script:trashFolder = Join-Path -Path $trashFolder -ChildPath $date
    New-Item -Path $script:trashFolder -ItemType Directory | Out-Null
    $script:trashFolder = Resolve-Path -Path $script:trashFolder

    $script:solution = Resolve-Path -Path 'OpenIdConnectClient.slnx'
}

# Synopsis: Clean previous build leftovers
Task Clean Init, {
    Get-ChildItem -Directory
    | Where-Object { -not $_.Name.StartsWith('.') }
    | ForEach-Object { Get-ChildItem -Path $_ -Recurse -Directory }
    | Where-Object { ( $_.Name -eq 'bin') -or ( $_.Name -eq 'obj') }
    | ForEach-Object { Remove-Item -Path $_ -Recurse -Force }
}

# Synopsis: Ensure Central Package Versions compliance
Task EnsureCentralPackageVersions Clean, {

    $projectFiles = Get-ChildItem -Path . `
        -Recurse `
        -Include *.csproj, *.fsproj, *.vbproj `
        -File

    $violations = @()

    foreach ($projectFile in $projectFiles) {
        try {
            [xml]$xml = Get-Content $projectFile.FullName -Raw
        }
        catch {
            throw "Failed to parse XML: $($projectFile.FullName)"
        }

        $ns = New-Object System.Xml.XmlNamespaceManager($xml.NameTable)
        $ns.AddNamespace('msb', $xml.DocumentElement.NamespaceURI)

        $nodes = $xml.SelectNodes('//*[@VersionOverride]', $ns)

        foreach ($node in $nodes) {
            $violations += [PSCustomObject]@{
                File  = $projectFile.FullName
                Node  = $node.Name
                Value = $node.GetAttribute('VersionOverride')
            }
        }
    }

    if ($violations.Count -gt 0) {
        throw "Build failed: VersionOverride attributes are not allowed. File: $($violations[0].File) Node: <$($violations[0].Node)>"
    }
}

# Synopsis: Format
Task Format Restore, {
    Exec { dotnet format $script:solution }
}

# Synopsis: Restore workloads
Task RestoreWorkloads Clean, {
    Exec { dotnet workload restore }
}

# Synopsis: Restore tools
Task RestoreTools Clean, {
    Exec { dotnet tool restore }
}

# Synopsis: Restore packages
Task RestorePackages Clean, EnsureCentralPackageVersions, {
    Exec { dotnet restore $script:solution }
}

# Synopsis: Restore workloads, tools, packages
Task Restore RestoreWorkloads, RestoreTools, RestorePackages

# Synopsis: Estimate Build Version
Task EstimateVersion Restore, {
    if ($Version) {
        $Script:NextVersion = $Version
    }
    else {
        $Script:NextVersion = '0.1.0'
    }
}

# Synopsis: Build
Task Build EstimateVersion, Format, {
    Exec { dotnet build $script:solution }
}

# Synopsis: Test
Task Test Build, {
    Exec { dotnet test $script:solution }
}

# Default Task
Task . Build, Test
