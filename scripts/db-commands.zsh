# Get dbContext information
dotnet ef dbcontext info -p ./Skinshare.Data -s ./Skinshare.Web

# Add new migration
dotnet ef migrations add MIGRATION_NAME -p ./Skinshare.Data -s ./Skinshare.Web -o Migrations

# Update database
dotnet ef database update -p ./Skinshare.Data -s ./Skinshare.Web

# Run seed command
dotnet ef database drop -p ./Skinshare.Data -s ./Skinshare.Web && dotnet run -p Skinshare.Seed

# Generate Razor Pages

dotnet aspnet-codegenerator razorpage -m Routine -dc Skinshare.Data.RoutineContext -udl -outDir Pages/Generated --referenceScriptLibraries
