# Get dbContext information
dotnet ef dbcontext info -p ./Skinshare.Data -s ./Skinshare.Web

# Add new migration
dotnet ef migrations add MIGRATION_NAME -p ./Skinshare.Data -s ./Skinshare.Web -o Migrations

# Update database
dotnet ef database update -p ./Skinshare.Data -s ./Skinshare.Web

# Run seed command
dotnet ef database drop -p ./Skinshare.Data -s ./Skinshare.Web && dotnet run -p Skinshare.Seed

