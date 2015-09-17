<Query Kind="Program" />

// C# Primer on nullable-types
void Main()
{
	int? totalCount = null;
	totalCount.Dump();
	
	if(totalCount.HasValue) // .HasValue is false if there is no int value
	{
		"It has a value".Dump();
	}
	else
	{
		"It does not have a value - it is 'null'".Dump();
	}
	totalCount = 99;
	totalCount.Dump();
}

// Define other methods and classes here
