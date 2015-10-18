using UnityEngine;
using System.Collections;
using NUnit.Framework;
using NSubstitute;


/// <summary>
/// 
/// Tests the Room class.
///
/// 
/// </summary>
[TestFixture]
public class TestRoom
{
    ScriptInstantiator instantiator = new ScriptInstantiator();

    [Test]
    public void TestCreateRoom()
    {
        Room room = instantiator.InstantiateScript<Room>();
        Assert.IsNotNull(room);
    }

    [Test]
    public void TestCheckRoomStandard()
    {
        Room room = instantiator.InstantiateScript<Room>();
        Assert.IsFalse(room.bigRoom);
        Assert.AreEqual(room.BIG_ROOM_CAMERA_SIZE, 8f);
        Assert.AreEqual(room.SMALL_ROOM_CAMERA_SIZE, 3.3222f);
        Assert.AreEqual(Room.ROOM_HEIGHT, 5.4f);
        Assert.AreEqual(Room.ROOM_WIDTH, 9.0f);
  
    }
}