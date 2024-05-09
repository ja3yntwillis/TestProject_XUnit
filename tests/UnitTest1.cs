using Xunit;
using Moq;
using MyBasicConsoleApplication.code;

public class CreateEmployeeTests
{
    [Fact]
    public void createEmployeeonenew_SuccessfullyInsertsData()
    {
        // Arrange
        var mockConnection = new Mock<ISqlConnection>();
        var mockCommand = new Mock<ISqlCommand>();

        mockConnection.Setup(c => c.CreateCommand()).Returns(mockCommand.Object);
        mockCommand.SetupProperty(c => c.CommandText);

        var createEmployee = new CreateEmployee(mockConnection.Object);
        var query = "INSERT INTO Employees";

        // Act
        createEmployee.createEmployeeonenew(query);

        // Assert
        mockConnection.Verify(c => c.Open(), Times.Once);
        mockCommand.VerifySet(c => c.CommandText = query, Times.Once);
        mockCommand.Verify(c => c.ExecuteNonQuery(), Times.Once);
        mockConnection.Verify(c => c.Close(), Times.Once);
    }
}
