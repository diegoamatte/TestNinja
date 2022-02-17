using Moq;
using System.Net;
using TestNinja.Mocking;
using Xunit;

namespace TestNinja.Tests.Mocking
{
    public class InstallerHelperTests
    {
        private string _customerName = "customerName";
        private string _installerName = "installerName";
        private Mock<IFileDownloader> _fileDownloader;
        private InstallerHelper _installerHelper;

        public InstallerHelperTests()
        {
            _fileDownloader = new Mock<IFileDownloader>();
            _installerHelper = new InstallerHelper(_fileDownloader.Object);
        }

        [Fact]
        public void DownloadInstaller_ReturnsTrue_WhenCalled()
        {
            //Act
            var result = _installerHelper.DownloadInstaller(_customerName, _installerName);
            //Assert
            Assert.True(result);
        }

        [Fact]
        public void DownloadInstaller_ReturnsFalse_WhenCannotDownload()
        {
            //Arrange
            _fileDownloader.Setup(fd => fd.DownloadFile(It.IsAny<string>(), It.IsAny<string>())).Throws<WebException>();

            //Act
            var result = _installerHelper.DownloadInstaller(_customerName, _installerName);

            //Assert
            Assert.False(result);
            
        }
    }
}
