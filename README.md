# SimpleBackupService

A windows service that watches for new drives to backup to.  
If a new drive is inserted it looks for a batch file (backup.bat) and executes it.  

The service is extensible with other components to support logging, configuration, a webapi and a control application  

Build Status  
[![Build status](https://ci.appveyor.com/api/projects/status/28q35k5q5jcc8m1i?svg=true)](https://ci.appveyor.com/project/Breakpoint21/simplebackupservice)
