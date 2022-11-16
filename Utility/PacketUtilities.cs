namespace LPRT;

public static class PacketUtilities
{
    public static string ParsePacketName(string packetType)
    {
        var split = packetType.Split('.');
        var packetName = split[split.Length - 1].Split(',')[0];

        return packetName;
    }
}