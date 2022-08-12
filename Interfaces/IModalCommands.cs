namespace LPRT.Interfaces
{
    public interface IModalCommands
    {
        void Publish_PacketTimeLine();
        void Publish_PacketFilters();
        void Publish_FilteredPacketTimeLine();
    }
}