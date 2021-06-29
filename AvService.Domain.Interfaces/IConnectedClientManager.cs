namespace AvService.Domain
{
    public interface IConnectedClientManager
    {
        bool IsClientConected { get; }
        string ConnectionId { get; }
        bool Connect(string connectionId);
        void Disconect(string connectionId);
        bool ValidateConnection(string connectionId);
    }
}