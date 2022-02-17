using Moq;
using TestNinja.Mocking;
using Xunit;

namespace TestNinja.Tests.Mocking
{
    public class EmployeeControllerTests
    {
        private Mock<IEmployeeStorage> _storage;
        private EmployeeController _employeeController;

        public EmployeeControllerTests()
        {
            _storage = new Mock<IEmployeeStorage>();
            _employeeController = new EmployeeController(_storage.Object);
        }

        [Fact]
        public void DeleteEmployee_DeleteEmployeeFromDb()
        {
            //Arrange
            var id = 1;

            //Act
            _employeeController.DeleteEmployee(id);

            //Assert
            _storage.Verify(st => st.DeleteEmployee(id));
        }

        [Fact]
        public void DeleteEmployee_RedirectsToEmployees()
        {
            //Arrange
            var id = 1;

            //Act
            var result = _employeeController.DeleteEmployee(id);

            //Assert
            Assert.IsType<RedirectResult>(result);
        }
    }
}
