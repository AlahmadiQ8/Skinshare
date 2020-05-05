# Get dbContext information
dotnet ef dbcontext info -p ./Skinshare.Data -s ./Skinshare.Web

# Add new migration
dotnet ef migrations add MIGRATION_NAME -p ./Skinshare.Data -s ./Skinshare.Web -o Migrations

# Update database
dotnet ef database update -p ./Skinshare.Data -s ./Skinshare.Web

# Run seed command
dotnet ef database drop -p ./Skinshare.Data -s ./Skinshare.Web && dotnet run -p Skinshare.Seed

# Generate Code
dotnet aspnet-codegenerator razorpage -m Routine -dc Skinshare.Data.RoutineContext -udl -outDir Pages/Generated --referenceScriptLibraries
dotnet aspnet-codegenerator controller -name RoutinesController -async -api -m Skinshare.Core.Entities.Routine -dc Skinshare.Data.RoutineContext -outDir Controllers

# Publish command
cd Skinshare.Web && dotnet publish -c Release -o publish && cd ..

# Execute Docker
docker build --rm -f "Skinshare.Web/Dockerfile" -t skinshare "Skinshare.Web"

# build nginx
docker build --rm -f "reverseproxy/Dockerfile" -t reverseproxy "reverseproxy"
