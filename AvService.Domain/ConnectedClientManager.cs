namespace AvService.Domain
{
    public class ConnectedClientManager : IConnectedClientManager
    {
        public bool IsClientConected { get; private set; }
        public bool Connect()
        {
            if (!IsClientConected)
            {
                IsClientConected = true;
                return true;
            }
            return false;
        }

        public void Disconect()
        {
            IsClientConected = false;
        }
    }
}
