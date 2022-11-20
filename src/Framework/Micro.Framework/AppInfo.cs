namespace Micro.Framework;

public sealed record AppInfo(string Name, string Version)
{
    public override string ToString() => $"{Name} {Version}";
}