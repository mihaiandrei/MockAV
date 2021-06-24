using AvService.Domain.Notifications;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace AvService.Domain.Test
{
    public class Tests
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
            scannerMock.Verify(s => s.StartAsync());
            Assert.IsTrue(scanStarted);
        }

        [Test]
        public async Task WhenAScanIsInProgressThenTheStartScanCanNotBeStarted()
        {
            var scanStarted = await scannerManager.StartOnDemandScanAsync();
            notifierMock.Verify(n => n.SendAsync(It.IsAny<StartScanOnDemandNotification>()), Times.Never);
            scannerMock.Verify(s => s.StartAsync(), Times.Never);
            Assert.IsFalse(scanStarted);
        }
    }
}