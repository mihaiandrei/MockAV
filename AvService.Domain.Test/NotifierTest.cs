﻿using AvService.Domain.Notifications;
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
        Mock<INotificationPersister> notificationPersisterMock;

        [SetUp]
        public void Setup()
        {
            scanHubMock = new Mock<IScanHub>();
            connectedClientManagerMock = new Mock<IConnectedClientManager>();
            notificationPersisterMock = new Mock<INotificationPersister>();

            notifier = new Notifier(scanHubMock.Object, connectedClientManagerMock.Object, notificationPersisterMock.Object);
        }

        [Test]
        public async Task WhenAClientIsConnectedTheNotificationIsSent()
        {
            connectedClientManagerMock.Setup(cc => cc.IsClientConected).Returns(true);
            await notifier.SendAsync(new Notification());
            scanHubMock.Verify(sh => sh.SendMessage(It.IsAny<Notification>()));
            notificationPersisterMock.Verify(np => np.AddNotification(It.IsAny<Notification>()), Times.Never);
        }

        [Test]
        public async Task WhenNoClientIsConnectedTheNotificationIsPersisted()
        {
            connectedClientManagerMock.Setup(cc => cc.IsClientConected).Returns(false);
            await notifier.SendAsync(new Notification());
            scanHubMock.Verify(sh => sh.SendMessage(It.IsAny<Notification>()), Times.Never);
            notificationPersisterMock.Verify(np => np.AddNotification(It.IsAny<Notification>()));
        }

        [Test]
        public async Task WhenNoClientPushNotificationsDoesNotSendAny()
        {
            connectedClientManagerMock.Setup(cc => cc.IsClientConected).Returns(false);
            await notifier.PushUnsentNotifications();
            scanHubMock.Verify(sh => sh.SendMessage(It.IsAny<Notification>()), Times.Never);
            notificationPersisterMock.Verify(np => np.RemoveNotification(It.IsAny<Notification>()), Times.Never);
        }

        [Test]
        public async Task WhenPushNotificationsClearsNotifications()
        {
            notificationPersisterMock.Setup(np => np.GetNotifications()).Returns(new[] { new Notification() });

            connectedClientManagerMock.Setup(cc => cc.IsClientConected).Returns(true);
            await notifier.PushUnsentNotifications();
            scanHubMock.Verify(sh => sh.SendMessage(It.IsAny<Notification>()));
            notificationPersisterMock.Verify(np => np.RemoveNotification(It.IsAny<Notification>()));
        }
    }
}