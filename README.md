# AspNetCore.Health [![MyGet CI](https://img.shields.io/myget/aspnetcore-health/v/AspNetCore.Health.svg)](http://myget.org/gallery/aspnetcore-health)

### What Is Application Health Check?

Health checking is the process where [load balancers] (https://en.wikipedia.org/wiki/Load_balancing_(computing)) or application delivery controller does periodic check on our applications to make sure that they are up and responding without any problems. If our applications are down for every reason or any of the system that our applications depends on (A database, a distributed cache, web service, ect) are down, the load balancer should detect this and stop sending traffic its way.

### Why AspNetCore.Health?

AspNetCore.Health enables load balancers to monitor the status of deployed Web applications.

Examples in the wiki.

### Scenarios

AspNetCore.Health enables you to do the following tasks:

* Monitor the performance of an AspNetCore application to make sure that it is healthy.
* Rapidly diagnose applications or systems that are failing.

### Install AspNetCore Health

You should install [AspNetCore.Health with NuGet](https://www.nuget.org/packages/AspNetCore.Health):

    Install-Package AspNetCore.Health
    
This command from Package Manager Console will download and install AspNetCore.Health and all required dependencies.

### Continous integration build

| Platform                    | Status                                                                                                                                  |
|-----------------------------|-----------------------------------------------------------------------------------------------------------------------------------------|
| AppVeyor (.NET Core) | [![Build status](https://ci.appveyor.com/api/projects/status/nxoyeq5r03tk6cpq/branch/master?svg=true)](https://ci.appveyor.com/project/lurumad/aspnetcore-health/branch/master) |

### Copyright

Copyright © 2016 Luis Ruiz
