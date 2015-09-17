<Query Kind="Program" />

// C# primer on extension methods
void Main()
{
	string name = "Dan";
	string message = name.Sleepy();
	message.Dump();
	message = name.Sleepy(3);
	message.Dump();
}

// Define other methods and classes here
public static class StringExtensions
{
	public static string Sleepy(this string text)
	{
		return text + " - Yawn! Are we there yet?";
	}
	public static string Sleepy(this string text, int count)
	{
		string tired = "Yawn";
		while (count > 0)
		{
			text = text + " - " + tired;
			count--;
		}
		return text + "! Aren't we there yet?";
	}
}





