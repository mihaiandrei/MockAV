namespace AvService.Domain
{
    public interface IConnectedClientManager
    {
        bool IsClientConected { get; }

        void Connect();
        void Disconect();
    }
}