How to start Web proj

1. Drop DirectoryBuild.props (from project and catalog. You can save it on another disk for future)
2. Drop and re add App_Data folder (optional);
3. Unload TicketManagement.Web project 
(that's create roslyn\csc.exe and fix start trouble);
4. Build TicketManagement solution
5. Reload, unload and reload TicketManagement.Web project  (that's fix broken links)
6. Add DirectoryBuild.props
7. Restart IDE
8. Build solution
9. Publish database proj
10. Run web proj


How to start AQA tests

1. install Selenium.WebDriver.ChromeDriver latest version
2. install extentions SpecFlow, NUnit 3, NUnit 2
3. Start Web proj
4. Start tests
