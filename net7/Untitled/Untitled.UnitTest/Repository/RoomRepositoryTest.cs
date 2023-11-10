using NUnit.Framework;
using Untitled.Core.Repository;

namespace Untitled.UnitTest.Repository;

[TestFixture]
public class RoomRepositoryTest
{
    private IRoomRepository _roomRepository = null!;

    [SetUp]
    public void Setup()
    {
        _roomRepository = new RoomRepository();
    }

    [TearDown]
    public void TearDown()
    {
        _roomRepository = null!;
    }

    [TestCase("", 0)]
    [TestCase("1,2,3,4", 4)]
    [TestCase("1,2,3,4,5", 5)]
    public void Add_ShouldPerformCorrectly(string names, int expectedCount)
    {
        // Arrange
        if (!string.IsNullOrEmpty(names))
        {
            var nameList = names.Split(',');
            foreach (var name in nameList)
            {
                _roomRepository.Add(name);
            }
        }

        // Act
        var actualCount = _roomRepository.GetAll().Count();

        // Assert
        Assert.AreEqual(expectedCount, actualCount);
    }

    [Test]
    public void Add_ShouldThrowException_WhenRoomNameAlreadyExists()
    {
        // Arrange
        const string name = "1";
        _roomRepository.Add(name);

        // Act
        // Assert
        Assert.Throws<ArgumentException>(() => _roomRepository.Add(name));
    }


    [TestCase("1,2,3,4,5", "1")]
    [TestCase("1,2,3,4,5", "2")]
    [TestCase("1,2,3,4,5", "3")]
    [TestCase("1,2,3,4,5", "4")]
    [TestCase("1,2,3,4,5", "5")]
    public void Get_ShouldPerformCorrectly(string existing, string key)
    {
        // Arrange
        var nameList = existing.Split(',');
        foreach (var name in nameList)
        {
            _roomRepository.Add(name);
        }

        // Act
        var room = _roomRepository.Get(key);

        // Assert
        Assert.AreEqual(key, room.Name);
    }

    [Test]
    public void Get_ShouldThrowException_WhenRoomNameDoesNotExist()
    {
        // Arrange
        const string name = "1";
        _roomRepository.Add(name);

        // Act
        // Assert
        Assert.Throws<KeyNotFoundException>(() => _roomRepository.Get("2"));
    }
}