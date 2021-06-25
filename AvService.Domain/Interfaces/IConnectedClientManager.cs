namespace AvService.Domain
{
    public interface IConnectedClientManager
    {
        bool IsClientConected { get; }

        bool Connect();
        void Disconect();
    }
}