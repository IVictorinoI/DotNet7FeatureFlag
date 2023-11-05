Run:
docker-compose up -d

Migration add:
dotnet ef migrations add FirstMigration --project ".\DotNet7FeatureFlag.Domain\DotNet7FeatureFlag.App.csproj" --startup-project ".\DotNet7FeatureFlag\DotNet7FeatureFlag.Api.csproj"

Migration update database:
dotnet ef database update --verbose --project ".\DotNet7FeatureFlag.Domain\DotNet7FeatureFlag.App.csproj" --startup-project ".\DotNet7FeatureFlag\DotNet7FeatureFlag.Api.csproj"
