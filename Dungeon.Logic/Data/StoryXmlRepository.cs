using System;
using Dungeon.Logic.Model;
using System.Xml.Linq;
using System.IO;
using System.Collections.Generic;


namespace Dungeon.Logic.Data {
    public class StoryXmlRepository {
        public RoomCatalog GetCatalog(string path)
        {
            RoomCatalog catalog = new RoomCatalog(Path.GetFileNameWithoutExtension(path));

            XElement catalogRoot = XElement.Load(path);
            IEnumerable<XElement> roomNodes = catalogRoot.Descendants("room");
            foreach(XElement roomNode in roomNodes) 
            {
                string roomName = GetRequiredAttribute(roomNode, "name", catalog.Name);

                Room room = new Room(0, GetRequiredAttribute(roomNode, "name", roomName), GetRequiredContent(roomNode, "narrative", roomName));

                var exits = roomNode.Element("exits");

                if (exits != null)
                {
                    var exitNodes = exits.Descendants("exit");

                    foreach(var exitNode in exitNodes) 
                    {
                        RoomExit roomExit = new RoomExit(GetRequiredAttribute(exitNode, "keyword",  $"{roomName} exits"), GetRequiredAttribute(exitNode, "room", $"{roomName} exits"));

                        room.AddExit(roomExit);
                    }

                }

                catalog.AddRoom(room);
            }

            return catalog;
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