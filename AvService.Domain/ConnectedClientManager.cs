namespace AvService.Domain
{
    public class ConnectedClientManager : IConnectedClientManager
    {
        public string ConnectionId { get; private set; }
        public bool IsClientConected => !string.IsNullOrEmpty(ConnectionId);

        public bool Connect(string connectionId)
        {
            if (!IsClientConected)
            {
                this.ConnectionId = connectionId;
                return true;
            }
            return false;
        }
        public void Disconect(string connectionId)
        {
            if (ConnectionId == connectionId)
                this.ConnectionId = null;
        }
        public bool ValidateConnection(string connectionId)
        {
            return this.ConnectionId == connectionId;
        }
    }
}
