using MbDotNet;
using MbDotNet.Enums;
using MbDotNet.Models.Imposters;
using MbDotNet.Models.Predicates;
using MbDotNet.Models.Predicates.Fields;
using MbDotNet.Models.Responses;
using MbDotNet.Models.Responses.Fields;
using MbDotNet.Models.Stubs;
using Newtonsoft.Json;

namespace MountebankCustomProtocol;

public class TelnetImposter : Imposter
{
    public TelnetImposter(int? port, string name, bool recordRequests) : base(port, "telnet", name, recordRequests)
    {
    }
}

public class TelnetStub : Stub
{
    public TelnetStub OnCommandEquals(string command)
    {
        var fields = new TelnetPredicateFields
        {
            Command = command
        };

        Predicates.Add(new EqualsPredicate<TelnetPredicateFields>(fields));

        return this;
    }

    public TelnetStub ReturnsResponse(string response)
    {
        var fields = new TelnetResponseFields
        {
            Response = response
        };
        
        Responses.Add(new IsResponse<TelnetResponseFields>(fields));

        return this;
    }

    public TelnetStub ReturnsProxy(Uri to, ProxyMode proxyMode,
        IEnumerable<MatchesPredicate<TelnetBooleanPredicateFields>> predicateGenerators)
    {
        var fields = new ProxyResponseFields<TelnetBooleanPredicateFields>
        {
            To = to,
            Mode = proxyMode,
            PredicateGenerators = predicateGenerators.ToList()
        };
        
        Responses.Add(new ProxyResponse<ProxyResponseFields<TelnetBooleanPredicateFields>>(fields));

        return this;
    }
}

public class TelnetPredicateFields : PredicateFields
{
    [JsonProperty("command", NullValueHandling = NullValueHandling.Ignore)]
    public string Command { get; set; }
}

public class TelnetBooleanPredicateFields : PredicateFields
{
    [JsonProperty("command", NullValueHandling = NullValueHandling.Ignore)]
    public bool? Command { get; set; }
}

public class TelnetResponseFields : ResponseFields
{
    [JsonProperty("response", NullValueHandling = NullValueHandling.Ignore)]
    public string Response { get; set; }
}

public class CustomMountebankClient : MountebankClient
{
    public async Task<TelnetImposter> CreateTelnetImposterAsync(int port, string name, bool recordRequests,
        CancellationToken cancellationToken = default) =>
        await ConfigureAndCreateImposter(new TelnetImposter(port, name, recordRequests), _ => { }, cancellationToken);
}