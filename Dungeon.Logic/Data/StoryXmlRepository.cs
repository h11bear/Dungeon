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

            XElement dungeonStory = XElement.Load(path);
            IEnumerable<XElement> rooms = dungeonStory.Descendants("room");
            foreach(XElement room in rooms) 
            {
                XAttribute roomName = room.Attribute("name");

                if (roomName == null || string.IsNullOrWhiteSpace(roomName.Value)) 
                {
                    throw new StoryDataException($"Room name is missing in {catalog.Name}, please review XML:{Environment.NewLine}{room.Value}");
                }

                XElement narrative = room.Element("narrative");
                if (string.IsNullOrWhiteSpace(narrative.Value)) 
                {
                    throw new StoryDataException($"Narrative is missing for the {roomName.Value} room, please fix!");
                }

                catalog.AddRoom(new Room(roomName.Value, narrative.Value));
            }

            return catalog;
        }
    }
}