// This will be moved into a seperate repository and project at some point. But it'll be in here still.

using System.Reflection;

namespace GiftscopModifier.Console
{
	internal enum PARSE_TYPE
	{
		PARSE,
		TRY_PARSE
	}

	internal record Flag<T>
	{
		public string Name { get; }
		public T? Value { get; }

		private PARSE_TYPE ParseType;
		
		public Flag(string name, string value) {
			Name = name;

			Type type = typeof(T);

			try
			{
				MethodInfo? ParseMethod = typeof(T).GetMethod("Parse", [ typeof(string) ]);

				if (ParseMethod != null) {
					System.Console.WriteLine($"Value String: {value}");

					object? ParseMethodReturn = ParseMethod.Invoke(null, [value]);

					System.Console.WriteLine($"Value: {ParseMethodReturn}");

					if (ParseMethodReturn != null)
					{
						Value = ( T ) ParseMethodReturn;
					}
				}
			}
			catch (Exception ex)
			{
				System.Console.WriteLine("Invalid Universal Flag, exiting.");
				System.Console.WriteLine("This was raised internally by an exception.");
				System.Console.WriteLine("The exception details are below.");
				System.Console.WriteLine();
				System.Console.WriteLine($"Message: '{ex.Message}'.");
				System.Console.WriteLine($"Type: '{ex.GetType().FullName}'.");
				System.Console.WriteLine("");
				System.Console.WriteLine($"Stack Trace:");
				System.Console.WriteLine($"{ex.StackTrace}");
				Environment.Exit(1);
			}

			/*
			MethodInfo[] Methods = type.GetMethods();

            foreach (MethodInfo Method in Methods)
            {
				if (Method.GetParameters().Length != 1 && !Method.GetParameters()[0].ParameterType.Equals("".GetType())) continue;

				if (Method.Name == "Parse")
				{

				}

				object? output;

				try
				{
					output = Method.Invoke("Parse", [ value ]);

					if (output == null || output.GetType().Equals(type))
					{
						continue;
					}
				}
				catch
				{
					continue;
				}

				return;
			}
			*/
        }
	}

	internal record FlagComponents
	{
		public string Name { get; }
		public string Type { get; }
		public string Value { get; }

		public FlagComponents(string flag)
		{
			try
			{
				Name  = flag.Split("=")[0].Split(":")[0].Split("--")[1];
				Type  = flag.Split("=")[0].Split(":")[1];
				Value = flag.Split("=")[1];
			}
			catch
			{
				System.Console.WriteLine("Invalid Universal Flag, exiting (1).");
				Environment.Exit(1);
			}
		}
	}

	internal struct FlagContainer
	{
		public Type type { get; set; }
		public dynamic flag { get; set; }
	}

	internal class UniversalFlags
	{
		public static List<FlagContainer>? Flags { get; private set; }

		public static void ParseFlags(string[] args)
		{
			Flags = [];

            foreach (string flag in args)
			{
				//System.Console.WriteLine($"New flag! ({flag})");
				if (!flag.StartsWith("--")) continue;
				//System.Console.WriteLine($"Starts with '--'.");

				FlagComponents flagComponents = new(flag);

				//System.Console.WriteLine($"Able to construct components.");

				Type? flagType = Type.GetType(flagComponents.Type);

				if (flagType == null)
				{
					System.Console.WriteLine("Invalid Universal Flag, exiting (2).");
					Environment.Exit(1);
				}

				System.Console.WriteLine($"Got flag {flagComponents}.");

				Type genericFlagType = typeof(Flag<>).MakeGenericType(flagType);
				dynamic initalizedFlag = Activator.CreateInstance(genericFlagType, [ flagComponents.Name, flagComponents.Value ]);

				System.Console.WriteLine(initalizedFlag);
				System.Console.WriteLine(initalizedFlag.GetType());

				if (initalizedFlag == null)
				{
					System.Console.WriteLine("Invalid Universal Flag, exiting (3).");
					Environment.Exit(1);
				}

				//System.Console.WriteLine($"Constructed flag.");

				FlagContainer flagContainer = new FlagContainer();

				flagContainer.flag = initalizedFlag;
				flagContainer.type = genericFlagType;

				Flags.Add(flagContainer);

				//System.Console.WriteLine($"Added flag, yay!");
			}
        }
	}
}
