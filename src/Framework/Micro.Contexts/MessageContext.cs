namespace Micro.Contexts;

public sealed record MessageContext(string MessageId, IContext Context);