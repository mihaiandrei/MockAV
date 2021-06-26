using AvService.Domain.Notifications;
using Moq;
using NUnit.Framework;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AvService.Domain.Test
{
    public class ScannerManagerTests
    {
        Mock<IScanner> scannerMock;
        Mock<INotifier> notifierMock;

        ScannerManager scannerManager;

        [SetUp]
        public void Setup()
        {
            scannerMock = new Mock<IScanner>();
            notifierMock = new Mock<INotifier>();

            scannerManager = new ScannerManager(scannerMock.Object, notifierMock.Object);
        }


        [Test]
        public async Task WhenAnOnDemandScanIsStartedThenANotificationIsSent()
        {
            var scanStarted = await scannerManager.StartOnDemandScanAsync();
            notifierMock.Verify(n => n.SendAsync(It.IsAny<StartScanOnDemandNotification>()));
            scannerMock.Verify(s => s.ScanAsync(It.IsAny<CancellationToken>()));
            Assert.IsTrue(scanStarted);
        }

        [Test]
        public async Task WhenAScanIsInProgressThenTheStartScanCanNotBeStarted()
        {
            scannerMock.Setup(s => s.ScanAsync(It.IsAny<CancellationToken>())).Returns(async () =>
            {
                await Task.Delay(100);
                return Enumerable.Empty<InfectedObject>();
            });

            await scannerManager.StartOnDemandScanAsync();

            var scanStarted = await scannerManager.StartOnDemandScanAsync();
            notifierMock.Verify(n => n.SendAsync(It.IsAny<StartScanOnDemandNotification>()), Times.Once);
            scannerMock.Verify(s => s.ScanAsync(It.IsAny<CancellationToken>()), Times.Once);
            Assert.IsFalse(scanStarted);
        }

        [Test]
        public async Task WhenAnOnDemandScanIsStoppedThenANotificationIsSent()
        {
            scannerMock.Setup(s => s.ScanAsync(It.IsAny<CancellationToken>())).Returns(async () =>
            {
                await Task.Delay(100);
                return Enumerable.Empty<InfectedObject>();
            });
            await scannerManager.StartOnDemandScanAsync();
            scannerManager.StopOnDemandScan();
            await Task.Delay(200);
            notifierMock.Verify(n => n.SendAsync(It.IsAny<StopScanOnDemandNotification>()));
        }

        [Test]
        public async Task WhenAnOnDemandScanIsFinishedThenANotificationIsSent()
        {
            await scannerManager.StartOnDemandScanAsync();
            notifierMock.Verify(n => n.SendAsync(It.IsAny<StopScanSuccessNotification>()));
        }

        [Test]
        public async Task WhenInfectedObjectAreFoundThenAThreatFoundNotificationIsSent()
        {
            scannerMock.Setup(s => s.ScanAsync(It.IsAny<CancellationToken>())).ReturnsAsync(() => new[] { new InfectedObject() });

            await scannerManager.StartOnDemandScanAsync();
            notifierMock.Verify(n => n.SendAsync(It.IsAny<ThreatFoundNotification>()));
        }

        [Test]
        public async Task WhenInfectedObjectAreNotFoundThenAThreatFoundNotificationIsNotSent()
        {
            scannerMock.Setup(s => s.ScanAsync(It.IsAny<CancellationToken>())).ReturnsAsync(() => Enumerable.Empty<InfectedObject>());

            await scannerManager.StartOnDemandScanAsync();
            notifierMock.Verify(n => n.SendAsync(It.IsAny<ThreatFoundNotification>()), Times.Never);
        }
    }
}