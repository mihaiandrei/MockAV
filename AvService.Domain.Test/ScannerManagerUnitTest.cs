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
            scannerManager = new ScannerManager(scannerMock.Object, notifierMock.Object);
        }

        [Test]
        public async Task Test1()
        {
            await scannerManager.StartOnDemandScanAsync();
            notifierMock.Verify(n => n.SendAsync(It.IsAny<StartScanOnDemandNotification>()));
        }
    }
}