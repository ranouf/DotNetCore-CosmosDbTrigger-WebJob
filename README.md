# DotNetCore CosmosDBTrigger WebJob

## Presentation
On this repository, you will have an .Net Core 2.0 WebJob using Dependency Injection and Application Insights which will listen CosmosDB data modifications.

## Prerequisites
For more information about :
- how to convert a .Net Core 2.0 App Console to a WebJob, read <a href="https://github.com/ranouf/DotNetCore-AppConsole-Azure-WebJob">DotNetCore-AppConsole-Azure-WebJob</a>,
- how to configure the DocumentDB on CosmosDB, read <a href="https://docs.microsoft.com/en-us/azure/cosmos-db/create-documentdb-dotnet">Azure Cosmos DB: Build a DocumentDB API web app with .NET and the Azure portal<a>

## Steps

### App Function
On https://portal.azure.com/ create a new 'Function App'.
Then add a new Function based on 'CosmosDbTrigger'.

### Azure Function
Go to DocumentDB settings then click on 'Add Azure Function'.
Select the Collection you want to monitor, the App function you have just created and the other fields then click on 'Save'.

### Code
Here is the code of the function you could use:
```
#r "Microsoft.Azure.Documents.Client"
using System;
using System.Collections.Generic;
using Microsoft.Azure.Documents;

public static void Run(IReadOnlyList<Document> input, ICollector<string> returnValues, TraceWriter log)
{
    var count = (input != null) ? input.Count : 0;

    log.Verbose($"There are {count} document(s).");

    for (var i = 0; i < count; i++)
    {
        log.Verbose($"input[{i}] = '{input[i]}'");
        returnValues.Add($"{input[i]}");
    }
}
```
It will allow you to log the data entered in the database.

### Integration
In 'Integrate' section, add a new Output, select 'AzureQueueStorage' and complete the field before saving.
Note: If you want to 

### Rename the QueueName
In CosmosDbTriggerWebJob.App, 


### Run my code
Final step, run the Cos

