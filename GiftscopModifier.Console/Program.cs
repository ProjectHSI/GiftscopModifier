namespace GiftscopModifier.Console
{
	internal class Program
	{
		static void Main(string[] args)
		{
			UniversalFlags.ParseFlags(args);

			foreach (FlagContainer flag in UniversalFlags.Flags)
            {
				System.Console.WriteLine("--- Flag ---");

				/*
				System.Console.WriteLine($"Name: {flag.Name}");
				System.Console.WriteLine($"Value: {flag.Value}");
				System.Console.WriteLine($"Type: {nameof(flag.Value)}");
				*/
			}
        }
	}
}
