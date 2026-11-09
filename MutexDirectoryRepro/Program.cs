var userDir = Environment.GetEnvironmentVariable("HOME") ?? throw new InvalidOperationException("Environment variable HOME is not available.");
var dotnetDir = Path.Combine(userDir, ".dotnet");
if (!Directory.Exists(dotnetDir))
{
	Directory.CreateDirectory(dotnetDir, UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.UserExecute);
}

using (File.Create(Path.Combine(dotnetDir, "corefx")))
{
}

try
{
	using var mutex = new Mutex(true, "Global\\test-mutex", new NamedWaitHandleOptions { CurrentSessionOnly = false, CurrentUserOnly = false }, out _);
}
catch (Exception e)
{
	Console.WriteLine("Failed!");
	Console.WriteLine(e);

	return;
}

Console.WriteLine("Success!");
