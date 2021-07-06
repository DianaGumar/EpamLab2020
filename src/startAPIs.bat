cd TicketManagement.VenueManager.API
start "TicketManagement.VenueManager.API" dotnet watch run
cd ..
cd TicketManagement.EventManager.API
start "TicketManagement.EventManager.API" dotnet watch run
cd ..
:: cd TicketManagement.UserManager.API
:: start "TicketManagement.UserManager.API" dotnet watch run
:: cd ..
:: cd TicketManagement.WebServer
:: start "TicketManagement.WebServer" dotnet watch run
:: cd ..
cd TicketManagement.WebClient
start "TicketManagement.WebClient" dotnet watch run
cd ..