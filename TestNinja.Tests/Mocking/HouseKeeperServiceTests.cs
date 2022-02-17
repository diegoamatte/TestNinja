using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using TestNinja.Mocking;
using Xunit;

namespace TestNinja.Tests.Mocking
{
    public class HouseKeeperServiceTests
    {
        private Mock<IEmailSender> _emailSender;
        private HouseKeeperService _houseKeeperService;
        private Mock<IStatementGenerator> _statementGenerator;
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IXtraMessageBox> _xtraMessageBox;
        private Housekeeper _housekeeper;
        private DateTime _statementDate = new DateTime(2022, 1, 1);

        public HouseKeeperServiceTests()
        {
            _emailSender = new Mock<IEmailSender>();
            _statementGenerator = new Mock<IStatementGenerator>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _xtraMessageBox = new Mock<IXtraMessageBox>();
            _houseKeeperService = new HouseKeeperService(_unitOfWork.Object, _statementGenerator.Object, _emailSender.Object, _xtraMessageBox.Object);
            _housekeeper = new Housekeeper()
            {
                Email = "mock@email.com",
                FullName = "Fullname",
                Oid = 1,
                StatementEmailBody = "EmailBody"
            };

            _unitOfWork.Setup(uof => uof.Query<Housekeeper>())
                .Returns(new List<Housekeeper>()
                {
                                _housekeeper
                }.AsQueryable());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData(" ")]
        public void SendStatementEmails_NotSavesStatement_WhenEmailIsNullOrWhitespace(string email)
        {
            //Arrange
            _housekeeper.Email = email;

            //Act
            _houseKeeperService.SendStatementEmails(_statementDate);

            //Assert
            _statementGenerator.Verify(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate), Times.Never);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData(" ")]
        public void SendStatementEmails_NotSendEmails_WhenStatementFilenameIsNullOrWhitespace(string filename)
        {
            //Arrange
            _statementGenerator.Setup(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate)).Returns(filename);

            //Act
            _houseKeeperService.SendStatementEmails(_statementDate);

            //Assert
            _emailSender.Verify(es => es.EmailFile(_housekeeper.Email, _housekeeper.StatementEmailBody, filename, It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void SendStatementEmails_SendsEmail()
        {
            //Arrange
            _statementGenerator.Setup(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate)).Returns("filename");

            //Act
            _houseKeeperService.SendStatementEmails(_statementDate);

            //Assert
            _emailSender.Verify(es => es.EmailFile(_housekeeper.Email, _housekeeper.StatementEmailBody, It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void SendStatementEmails_ShowsMessageBox_WhenExceptionOccurs()
        {
            //Arrange
            _statementGenerator.Setup(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate)).Returns("filename");
            _emailSender.Setup(es => 
                es.EmailFile(_housekeeper.Email, 
                _housekeeper.StatementEmailBody, 
                It.IsAny<string>(), 
                It.IsAny<string>()
            )).Throws<Exception>();

            //Act
            _houseKeeperService.SendStatementEmails(_statementDate);

            //Assert
            _xtraMessageBox.Verify(mb => mb.Show(It.IsAny<string>(), It.IsAny<string>(), MessageBoxButtons.OK), Times.Once);
        }
    }
}
