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