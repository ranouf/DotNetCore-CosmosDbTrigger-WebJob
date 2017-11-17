# AzureWebJob

## Presentation
On this repository, you will have an .Net Core 2.0 App Console using Dependency Injection and Application Insights.
This App Console will be compatible to Azure WebJobs.

## How to convert a .Net Core App Console to an Azure WebJob?

### Files
To make an App Console compatible to Azure Webjobs, you need to add 2 files:
- <b>run.cmd</b>, with .Net Core, the console app won't generate a .exe on publish, as it was with the traditional .Net Console app, but a dll, so you need to create the command file and add the following code:
```
@echo off
dotnet {NAMEOFYOURDLL}.dll
```
- <b>settings.job</b> (optional), this file will specify to Azure which periodicity you want, in my case, every 5 min will be:
```
"schedule": "0 0/5 * * * *"
```

### Publish
Publish your App Console to a local folder, <i>bin\Release\PublishOutput</i> by default, then zip the <i>PublishOutput</i> folder.

### Configure Azure

#### Create a new WebApp
Nothing different than a classic Web, enter a name and the select the options you need, then create.

#### Add a new WebJob
Enter a name, select the Zip file, select triggered, let triggers to manual, this option will overwritten if you have set a <i>settings.job</i> file,
