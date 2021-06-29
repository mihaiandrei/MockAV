using AvService.Shared;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace AvService.Domain.Test
{
    public class NotifierTest
    {
        Notifier notifier;
        Mock<IScanHub> scanHubMock;
        Mock<IConnectedClientManager> connectedClientManagerMock;
        Mock<INotificationRepository> notificationPersisterMock;

        [SetUp]
        public void Setup()
        {
            scanHubMock = new Mock<IScanHub>();
            connectedClientManagerMock = new Mock<IConnectedClientManager>();
            notificationPersisterMock = new Mock<INotificationRepository>();

            notifier = new Notifier(scanHubMock.Object, connectedClientManagerMock.Object, notificationPersisterMock.Object);
        }

        [Test]
        public async Task WhenAClientIsConnectedTheNotificationIsSent()
        {
            connectedClientManagerMock.Setup(cc => cc.IsClientConected).Returns(true);
            await notifier.SendAsync(new Notification());
            scanHubMock.Verify(sh => sh.SendNotification(It.IsAny<Notification>(), It.IsAny<string>()));
            notificationPersisterMock.Verify(np => np.AddNotificationAsync(It.IsAny<Notification>()), Times.Never);
        }

        [Test]
        public async Task WhenNoClientIsConnectedTheNotificationIsPersisted()
        {
            connectedClientManagerMock.Setup(cc => cc.IsClientConected).Returns(false);
            await notifier.SendAsync(new Notification());
            scanHubMock.Verify(sh => sh.SendNotification(It.IsAny<Notification>(), It.IsAny<string>()), Times.Never);
            notificationPersisterMock.Verify(np => np.AddNotificationAsync(It.IsAny<Notification>()));
        }

        [Test]
        public async Task WhenNoClientPushNotificationsDoesNotSendAny()
        {
            connectedClientManagerMock.Setup(cc => cc.IsClientConected).Returns(false);
            await notifier.PushUnsentNotifications();
            scanHubMock.Verify(sh => sh.SendNotification(It.IsAny<Notification>(), It.IsAny<string>()), Times.Never);
            notificationPersisterMock.Verify(np => np.RemoveNotificationsAsync(), Times.Never);
        }

        [Test]
        public async Task WhenPushNotificationsClearsNotifications()
        {
            notificationPersisterMock.Setup(np => np.GetNotificationsAsync()).ReturnsAsync(new[] { new Notification() });

            connectedClientManagerMock.Setup(cc => cc.IsClientConected).Returns(true);
            await notifier.PushUnsentNotifications();
            scanHubMock.Verify(sh => sh.SendNotification(It.IsAny<Notification>(), It.IsAny<string>()));
            notificationPersisterMock.Verify(np => np.RemoveNotificationsAsync());
        }
    }
}
