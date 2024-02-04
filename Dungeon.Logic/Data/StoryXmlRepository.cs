using System;
using Dungeon.Logic.Model;
using System.Xml.Linq;
using System.IO;
using System.Collections.Generic;


namespace Dungeon.Logic.Data {
    public class StoryXmlRepository: IStoryRepository {

        private List<Room> _rooms = new List<Room>();
        private string catalogName;
        private XElement catalogRoot;

        public StoryXmlRepository(string xmlPath){
            catalogRoot = XElement.Load(xmlPath);
            catalogName = Path.GetFileNameWithoutExtension(xmlPath);
        }
        
        public Story GetStory(string storyName) {

            IEnumerable<XElement> roomNodes = catalogRoot.Descendants("room");
            foreach(XElement roomNode in roomNodes) 
            {
                string roomName = GetRequiredAttribute(roomNode, "name", catalogName);

                var exitElement = roomNode.Element("exits");
                List<RoomExit> exits = new List<RoomExit>();
                if (exitElement != null)
                {
                    var exitNodes = exitElement.Descendants("exit");

                    foreach(var exitNode in exitNodes) 
                    {
                        RoomExit roomExit = new RoomExit(GetRequiredAttribute(exitNode, "keyword",  $"{roomName} exits"), GetRequiredAttribute(exitNode, "room", $"{roomName} exits"));
                        exits.Add(roomExit);
                    }
                }

                _rooms.Add(new Room(GetRequiredAttribute(roomNode, "name", roomName), GetRequiredContent(roomNode, "narrative", roomName), exits));
            }

            return new Story(storyName, _rooms[0], this);
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

    }
}