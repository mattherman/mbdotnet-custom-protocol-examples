
using MountebankCustomProtocol;

var client = new CustomMountebankClient();

await client.CreateTelnetImposterAsync(830, "TestTelnetImposter", true);