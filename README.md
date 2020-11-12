How to start Web proj

1. Drop DirectoryBuild.props
2. Drop and re add App_Data folder (optional?);
3. Unload TicketManagement.Web project 
(that's create roslyn\csc.exe and fix start trouble);
4. Build TicketManagement solution
5. Reload TicketManagement.Web project (that's fix broken links)
6. Add DirectoryBuild.props
7. Restart IDE

! you should run a web project once, before publishing the database project
