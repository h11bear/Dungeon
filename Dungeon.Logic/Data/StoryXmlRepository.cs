using System;
using Dungeon.Logic.Model;
using System.Xml.Linq;
using System.IO;
using System.Collections.Generic;
using System.Linq;


namespace Dungeon.Logic.Data
{
    public class StoryXmlRepository : IStoryRepository
    {

        private List<Room> _rooms = new List<Room>();
        private string catalogName;
        private XElement catalogRoot;

        int nextRoomId = 0;
        private class ExitConfiguration(Room sourceRoom, string keyword, string targetRoomName)
        {
            public Room SourceRoom => sourceRoom;
            public string Keyword => keyword;
            public string TargetRoomName => targetRoomName;
        }

        public StoryXmlRepository(string xmlPath)
        {
            catalogRoot = XElement.Load(xmlPath);
            catalogName = Path.GetFileNameWithoutExtension(xmlPath);
        }

        public Story GetStory(string storyName)
        {

            List<ExitConfiguration> exitConfigurations = new List<ExitConfiguration>();

            IEnumerable<XElement> roomNodes = catalogRoot.Descendants("room");
            foreach (XElement roomNode in roomNodes)
            {
                nextRoomId++;

                string roomName = GetRequiredAttribute(roomNode, "name", catalogName);
                Room currentRoom = new Room(GetRequiredAttribute(roomNode, "name", roomName), GetRequiredContent(roomNode, "narrative", roomName), nextRoomId);
                _rooms.Add(currentRoom);

                var exitElement = roomNode.Element("exits");
                if (exitElement != null)
                {
                    var exitNodes = exitElement.Descendants("exit");

                    foreach (var exitNode in exitNodes)
                    {
                        exitConfigurations.Add(new ExitConfiguration(currentRoom,
                            GetRequiredAttribute(exitNode, "keyword", $"{roomName} exits"),
                            GetRequiredAttribute(exitNode, "room", $"{roomName} exits")));
                    }
                }

            }

            foreach (ExitConfiguration exitConfiguration in exitConfigurations)
            {
                Room exitRoom = _rooms.SingleOrDefault(r => r.Name == exitConfiguration.TargetRoomName);
                if (exitRoom == null)
                {
                    throw new StoryDataException($"{exitConfiguration.SourceRoom.Name} has an exit keyword '{exitConfiguration.Keyword}' that points to a room '{exitConfiguration.TargetRoomName}' that does not exist!");
                } 
                else
                {
                    exitConfiguration.SourceRoom.WithExit(exitConfiguration.Keyword, exitRoom);
                }
            }

            return new Story(storyName, _rooms[0]);
        }

        public Room FindRoom(Story story, string roomName)
        {
            return _rooms.Find(r => r.Name.Equals(roomName, StringComparison.CurrentCultureIgnoreCase));
        }

        private static string GetRequiredContent(XElement node, string name, string customMessage)
        {
            XElement contentNode = node.Element(name);
            if (string.IsNullOrWhiteSpace(contentNode.Value))
            {
                throw new StoryDataException($"{name} is missing for the {customMessage}");
            }

            return contentNode.Value;
        }

        private static string GetRequiredAttribute(XElement node, string name, string customMessage)
        {
            XAttribute attribute = node.Attribute(name);
            if (attribute == null || string.IsNullOrWhiteSpace(attribute.Value))
            {
                throw new StoryDataException($"{name} attribute is missing for the {customMessage}");
            }

            return attribute.Value;
        }

        public Room Navigate(int roomId)
        {
            return _rooms.Single(r => r.RoomId == roomId);
        }
    }
}