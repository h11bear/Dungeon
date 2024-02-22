using Dungeon.Logic.Data;
using Dungeon.Logic.Model;
using Microsoft.EntityFrameworkCore;

namespace Dungeon.EntityFramework.Data;

public class StoryContextRepository(IDungeonContext context) : IStoryRepository
{
    public Room FindRoom(Story story, string roomName)
    {
        return context.Rooms.Single(room => room.Name == roomName);
        //throw new NotImplementedException();
    }

    public Story GetStory(string name)
    {
        return context.Stories
            .Include("Entrance")
            .Single(story => story.Name == name);
    }

    public Room Navigate(int roomId)
    {
        Room? targetRoom = context.Rooms.Find(roomId);

        if (targetRoom == null)
        {
            throw new NullReferenceException($"{roomId} not found, unable to navigate!");
        }

        return targetRoom;
    }
}