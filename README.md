How to start checking DAL and BL projects

1. build TicketManagement solution;
2. publich TicketManagement.Database;
3. in TicketManagement.IntegrationTests/App.config change connectionStrings if you need;
4. start tests

How to start Web proj
0. Drop DirectoryBuild.props
1. Drop and re add App_Data folder (optional?);
2. unload and reload TicketManagement.Web project 
(that's create roslyn\csc.exe and fix start trouble);
3. build TicketManagement solution

(repeat 2 and 3 cases until broken references become normal 
 and project start become without errors)
