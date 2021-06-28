using AvService.Shared;
using Moq;
using NUnit.Framework;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AvService.Domain.Test
{
    public class ScannerServiceTest
    {
        Mock<IScanner> scannerMock;
        Mock<INotifier> notifierMock;
        Mock<IConnectedClientManager> connectedClientManagerMock;

        ScannerService scannerManager;

        string connectionId = "connectionId ";

        [SetUp]
        public void Setup()
        {
            scannerMock = new Mock<IScanner>();
            notifierMock = new Mock<INotifier>();
            connectedClientManagerMock = new Mock<IConnectedClientManager>();

            scannerManager = new ScannerService(scannerMock.Object,
                                                notifierMock.Object,
                                                connectedClientManagerMock.Object);

            connectedClientManagerMock.Setup(cm => cm.ValidateConnection(It.IsAny<string>())).Returns(true);
        }

        [Test]
        public async Task WhenAnOnDemandScanIsStartedThenANotificationIsSent()
        {
            await scannerManager.StartOnDemandScanAsync(connectionId);
            notifierMock.Verify(n => n.SendAsync(It.IsAny<StartScanOnDemandNotification>()));
            scannerMock.Verify(s => s.ScanAsync(It.IsAny<CancellationToken>()));
            notifierMock.Verify(n => n.SendAsync(It.IsAny<ScanInProgressNotification>()), Times.Never);
        }

        [Test]
        public async Task WhenAScanIsInProgressThenTheStartScanCanNotBeStarted()
        {
            scannerMock.Setup(s => s.ScanAsync(It.IsAny<CancellationToken>())).Returns(async () =>
            {
                await Task.Delay(100);
                return Enumerable.Empty<InfectedObject>();
            });

            await scannerManager.StartOnDemandScanAsync(connectionId);

            await scannerManager.StartOnDemandScanAsync(connectionId);
            notifierMock.Verify(n => n.SendAsync(It.IsAny<StartScanOnDemandNotification>()), Times.Once);
            scannerMock.Verify(s => s.ScanAsync(It.IsAny<CancellationToken>()), Times.Once);
            notifierMock.Verify(n => n.SendAsync(It.IsAny<ScanInProgressNotification>()));
        }

        [Test]
        public async Task WhenAnOnDemandScanIsStoppedThenANotificationIsSent()
        {
            scannerMock.Setup(s => s.ScanAsync(It.IsAny<CancellationToken>())).Returns(async () =>
            {
                await Task.Delay(100);
                return Enumerable.Empty<InfectedObject>();
            });
            await scannerManager.StartOnDemandScanAsync(connectionId);
            scannerManager.StopOnDemandScan(connectionId);
            await Task.Delay(200);
            notifierMock.Verify(n => n.SendAsync(It.IsAny<StopScanOnDemandNotification>()));
        }

        [Test]
        public async Task WhenAnOnDemandScanIsFinishedThenANotificationIsSent()
        {
            await scannerManager.StartOnDemandScanAsync(connectionId);
            notifierMock.Verify(n => n.SendAsync(It.IsAny<StopScanSuccessNotification>()));
        }

        [Test]
        public async Task WhenInfectedObjectAreFoundThenAThreatFoundNotificationIsSent()
        {
            scannerMock.Setup(s => s.ScanAsync(It.IsAny<CancellationToken>())).ReturnsAsync(() => new[] { new InfectedObject() });

            await scannerManager.StartOnDemandScanAsync(connectionId);
            notifierMock.Verify(n => n.SendAsync(It.IsAny<ThreatFoundNotification>()));
        }

        [Test]
        public async Task WhenInfectedObjectAreNotFoundThenAThreatFoundNotificationIsNotSent()
        {
            scannerMock.Setup(s => s.ScanAsync(It.IsAny<CancellationToken>())).ReturnsAsync(() => Enumerable.Empty<InfectedObject>());

            await scannerManager.StartOnDemandScanAsync("connectionId");
            notifierMock.Verify(n => n.SendAsync(It.IsAny<ThreatFoundNotification>()), Times.Never);
        }
    }
}