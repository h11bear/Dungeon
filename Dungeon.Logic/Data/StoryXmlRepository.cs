using System;
using Dungeon.Logic.Model;
using System.Xml.Linq;
using System.IO;
using System.Collections.Generic;


namespace Dungeon.Logic.Data {
    public class StoryXmlRepository {
        public RoomCatalog GetRooms(string path)
        {
            RoomCatalog catalog = new RoomCatalog(Path.GetFileNameWithoutExtension(path));

            XElement catalogRoot = XElement.Load(path);
            IEnumerable<XElement> roomNodes = catalogRoot.Descendants("room");
            foreach(XElement roomNode in roomNodes) 
            {
                Room room = new Room(GetRequiredAttribute(roomNode, "name"), GetRequiredAttribute(roomNode, "narrative"));

                var exits = roomNode.Element("exits");

                if (exits != null)
                {
                    var exitNodes = exits.Descendants("exit");

                    foreach(var exitNode in exitNodes) 
                    {
                        RoomExit roomExit = new RoomExit(GetRequiredAttribute(exitNode, "keyword"), GetRequiredAttribute(exitNode, "room"));

                        room.AddExit(roomExit);
                    }

                }

                catalog.AddRoom(room);
            }

            return catalog;
        }

        private static string GetRequiredAttribute(XElement node, string name) 
        {
            XAttribute attribute = node.Attribute(name);
            if (attribute == null || string.IsNullOrWhiteSpace(attribute.Value)) 
            {
                string nodeXml = node.CreateReader().ReadOuterXml();
                throw new StoryDataException($"{name} attribute is missing in Room XML, please review node:{Environment.NewLine}{nodeXml}");
            }

            return attribute.Value;
        }
    }
}