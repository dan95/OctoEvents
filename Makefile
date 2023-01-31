default: init upmigrations

down:
	powershell -C "wsl docker-compose -f ./docker-compose.yml down"

init:
	powershell -C "wsl docker-compose -f ./docker-compose.yml up --build -d"

clean:
	dotnet clean

buildmigrations: clean
	dotnet ef migrations add InitialScript --project .\src\Infrastructure\OctoEvents.Infrastructure.Data\OctoEvents.Infrastructure.Data.csproj --startup-project .\src\Services\OctoEvents.API\OctoEvents.API.csproj

upmigrations: clean
	dotnet ef database update --project .\src\Infrastructure\OctoEvents.Infrastructure.Data\OctoEvents.Infrastructure.Data.csproj --startup-project .\src\Services\OctoEvents.API\OctoEvents.API.csproj

downmigrations: clean
	dotnet ef database update 0 --project .\src\Infrastructure\OctoEvents.Infrastructure.Data\OctoEvents.Infrastructure.Data.csproj --startup-project .\src\Services\OctoEvents.API\OctoEvents.API.csproj
