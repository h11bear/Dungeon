using System;
using Dungeon.Logic.Model;
using System.Xml.Linq;
using System.IO;
using System.Collections.Generic;


namespace Dungeon.Logic.Data {
    public class StoryXmlRepository {
        public Story GetStory(string path)
        {
            List<Room> rooms = new List<Room>();

            XElement catalogRoot = XElement.Load(path);
            string catalogName = Path.GetFileNameWithoutExtension(path);
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

                rooms.Add(new Room(GetRequiredAttribute(roomNode, "name", roomName), GetRequiredContent(roomNode, "narrative", roomName), exits));
            }

            return new Story(catalogName, rooms, rooms[0]);
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