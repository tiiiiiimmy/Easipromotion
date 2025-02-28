Set-Location -Path "src\EasiPromotion.Api"

$packages = @(
    "Microsoft.EntityFrameworkCore.Sqlite",
    "Microsoft.EntityFrameworkCore.Design",
    "Microsoft.AspNetCore.Authentication.JwtBearer",
    "CsvHelper",
    "SixLabors.ImageSharp",
    "QuestPDF"
)

foreach ($package in $packages) {
    Write-Host "Installing package: $package"
    dotnet add package $package
}

Set-Location -Path ".." 