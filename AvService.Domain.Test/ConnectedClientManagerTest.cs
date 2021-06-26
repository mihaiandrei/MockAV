using NUnit.Framework;

namespace AvService.Domain.Test
{
    public class ConnectedClientManagerTest
    {
        ConnectedClientManager connectedClientManager;

        [SetUp]
        public void Setup()
        {
            connectedClientManager = new ConnectedClientManager();
        }

        [Test]
        public void WhenAClientIsConnectedThenNoOtherClientsCanConnect()
        {
            connectedClientManager.Connect();
            Assert.IsFalse(connectedClientManager.Connect());
        }

        [Test]
        public void WhenAClientIDisconnectsThenOtherClientsCanConnect()
        {
            connectedClientManager.Connect();
            connectedClientManager.Disconect();
            Assert.IsTrue(connectedClientManager.Connect());
        }
    }
}
