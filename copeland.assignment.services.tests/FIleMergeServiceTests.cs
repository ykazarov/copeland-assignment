using AutoMapper;
using copeland.assignment.core;
using copeland.assignment.models;
using copeland.assignment.services.mappers;
using Moq;

namespace copeland.assignment.services.tests;

public class FileMergeServiceTests
{
    [TestFixture]
    public class ConstructorTests : FileMergeServiceTestsBuilder
    {
        [Test]
        public void Should_Construct()
        {
            Assert.DoesNotThrow(() => Build());
        }
        
        [Test]
        public void Should_Not_Construct_Without_Reader1()
        {
            Reader1 = null;
            Assert.That(() => Build(), Throws.ArgumentNullException);
        }
        
        [Test]
        public void Should_Not_Construct_Without_Reader2()
        {
            Reader2 = null;
            Assert.That(() => Build(), Throws.ArgumentNullException);
        }
        
        [Test]
        public void Should_Not_Construct_Without_TableWriter()
        {
            TableWriter = null;
            Assert.That(() => Build(), Throws.ArgumentNullException);
        }
        
        [Test]
        public void Should_Not_Construct_Without_Mapper()
        {
            Mapper = null;
            Assert.That(() => Build(), Throws.ArgumentNullException);
        }
    }

    public class MergeTests : FileMergeServiceTestsBuilder
    {
        [Test]
        public void Should_Merge()
        {
            var target= Build();
            
            target.Merge<DeviceDataTableModel>("foo1", "foo2", "table");

            TableWriterMock.Verify(t => 
                    t.WriteToFile(It.IsAny<string>(), It.Is<DeviceDataTableModel>(dto => dto.Records.Count == 4)), Times.Once);
            
            TableWriterMock.Verify(t => 
                    t.WriteToFile(It.IsAny<string>(), It.Is<DeviceDataTableModel>(dto => !dto.Records.Any(r => r.AverageHumdity > 0))), Times.Once);
            
            TableWriterMock.Verify(t => 
                t.WriteToFile(It.IsAny<string>(), It.Is<DeviceDataTableModel>(dto => dto.Records.Any(r => r.AverageTemperature == 1))), Times.Once);
            
            TableWriterMock.Verify(t => 
                t.WriteToFile(It.IsAny<string>(), It.Is<DeviceDataTableModel>(dto => dto.Records.Any(r => r.AverageTemperature == 2))), Times.Once);

            TableWriterMock.Verify(t => 
                t.WriteToFile(It.IsAny<string>(), It.Is<DeviceDataTableModel>(dto => dto.Records.Any(r => r.AverageTemperature == 3))), Times.Once);

            TableWriterMock.Verify(t => 
                t.WriteToFile(It.IsAny<string>(), It.Is<DeviceDataTableModel>(dto => dto.Records.Any(r => r.AverageTemperature == 4))), Times.Once);

            // ... other verifications. the models can be made very complex and appropriate number of tests would need to be developed.
        }
    }
}

public class FileMergeServiceTestsBuilder
{
    protected Mock<IFileIOService<DeviceDataFoo1Model>> Reader1Mock { get; private set; }
    protected Mock<IFileIOService<DeviceDataFoo2Model>> Reader2Mock { get; private set; }
    
    protected Mock<IFileIOService<DeviceDataTableModel>> TableWriterMock { get; private set; }
    
    protected IFileIOService<DeviceDataFoo1Model> Reader1 { get; set; }
    protected IFileIOService<DeviceDataFoo2Model> Reader2 { get; set; }
    
    protected IFileIOService<DeviceDataTableModel> TableWriter { get; set; }
    
    protected IMapper Mapper { get; set; }
    
    [SetUp]
    public void Setup()
    {
        Reader1Mock = new Mock<IFileIOService<DeviceDataFoo1Model>>();
        Reader2Mock = new Mock<IFileIOService<DeviceDataFoo2Model>>();
        TableWriterMock = new Mock<IFileIOService<DeviceDataTableModel>>();
        
        
        // setup mocks
        Reader1Mock.Setup(r => r.ReadFromFile(It.IsAny<string>()))
            .Returns(ResourceProvider.GetFoo1());
        Reader2Mock.Setup(r => r.ReadFromFile(It.IsAny<string>()))
            .Returns(ResourceProvider.GetFoo2());
        
        // setup target
        Reader1 = Reader1Mock.Object;
        Reader2 = Reader2Mock.Object;
        TableWriter = TableWriterMock.Object;
        Mapper = MapperConfigurator.ConfigureMapper().CreateMapper();
    }

    protected IFileMergeService<DeviceDataTableModel> Build()
    {
        return new FileMergeService(Reader1, Reader2, TableWriter, Mapper);
    }
}