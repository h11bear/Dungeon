using Dungeon.Logic.Model;

namespace Dungeon.Logic.Data;
public interface IStoryRepository {

    public Story GetStory(string name);
    public Room FindRoom(Story story, string roomName);
}