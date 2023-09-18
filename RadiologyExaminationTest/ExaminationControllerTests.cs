using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RadiologyExaminationAPI.Controllers;
using RadiologyExaminationAPI.Repo;

namespace RadiologyExaminationTest;

public class ExaminationControllerTests
{
    string fakeCNP = "1234567890123";
    string fakeDate = "20230131";
    string fakePath = "fake\\path";

    Mock<ILogger<ExaminationController>> mockLogger;
    Mock<IExaminationDbRepo> mockDbService;

    [SetUp]
    public void Init()
    {
        mockLogger = new Mock<ILogger<ExaminationController>>();
        mockDbService = new Mock<IExaminationDbRepo>();
    }


    [Test]
    public void Get_Examination_By_Id()
    {
        mockDbService.Setup(x => x.GetExaminations(fakeCNP)).Returns(new List<string>{ fakePath });        
        var controller = new ExaminationController(mockLogger.Object, mockDbService.Object);

        //Act
        var actual = controller.Get(fakeCNP);        
        var list = (actual.Result as OkObjectResult)?.Value as IEnumerable<string>;
        
        //Assert
        Assert.IsNotNull(list);
        Assert.IsTrue(list.Count() == 1);
        Assert.That(list, Has.Exactly(1).Matches<string>(x => x == fakePath));
    }

    [Test]
    public void Get_Examination_By_Id_And_Date()
    {
        mockDbService.Setup(x => x.GetExaminations(fakeCNP, fakeDate)).Returns(new List<string>{ fakePath });
        var controller = new ExaminationController(mockLogger.Object, mockDbService.Object);

        //Act
        var actual = controller.Get(fakeCNP, fakeDate);        
        var list = (actual.Result as OkObjectResult)?.Value as IEnumerable<string>;
        
        //Assert
        Assert.IsNotNull(list);
        Assert.IsTrue(list.Count() == 1);
        Assert.That(list, Has.Exactly(1).Matches<string>(x => x == fakePath));
    }

    [Test]
    public void Should_Call_GetExaminations_Only_Once()
    {
        Expression<Func<IExaminationDbRepo, IEnumerable<string>>> call = x => x.GetExaminations(fakeCNP);
        mockDbService.Setup(call).Returns(new List<string>(){fakePath}).Verifiable("Method not called!");

        var controller = new ExaminationController(mockLogger.Object, mockDbService.Object);
        _ = controller.Get(fakeCNP);

        mockDbService.Verify(call, Times.Once);
    }

    [Test]
    public void Should_Not_Call_LogError_From_Logger()
    {        
        Expression<Action<ILogger<ExaminationController>>> callLogger = x => x.Log(
            It.Is<LogLevel>(logLevel => logLevel == LogLevel.Error),
            It.IsAny<EventId>(),
            It.IsAny<It.IsAnyType>(),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>());
        mockLogger.Setup(callLogger).Verifiable("Error Raised!");
        
        var controller = new ExaminationController(mockLogger.Object, mockDbService.Object);
        _ = controller.Get(fakeCNP);

        mockLogger.Verify(callLogger, Times.Never);
    }
}
