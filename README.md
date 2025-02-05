Please create locally file appsettings.json in directory EmployeesTimeControl/Project/, as below:
{
  "ConnectionStrings": {
    "EmployeesTCCon": "Server=localhost;Database=YOURDATABASE;Port=5432;User Id=YOURUSERID;Password=YOURPASSWORD"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "Jwt": {
    "SecretKey": "PLEASE GENERATE A SECRET KEY AND PASTE HERE - IT MUST BE LONG ENOUGH SO JWT ACCEPTS IT",
    "Issuer": "http://localhost:5126",
    "Audience": "http://localhost:5126"
  },
  "AllowedHosts": "*"
}
