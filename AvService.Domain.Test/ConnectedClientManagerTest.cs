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
            connectedClientManager.Connect("connectionId");
            Assert.IsFalse(connectedClientManager.Connect("newconnectionId"));
        }

        [Test]
        public void WhenAClientIDisconnectsThenOtherClientsCanConnect()
        {
            connectedClientManager.Connect("connectionId");
            connectedClientManager.Disconect("connectionId");
            Assert.IsTrue(connectedClientManager.Connect("newConnectionId"));
        }
    }
}
