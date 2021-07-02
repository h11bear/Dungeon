using Dungeon.Logic.Model;
using System.Xml;
using System.IO;

namespace Dungeon.Logic.Data {
    public class StoryXmlRepository {
        public RoomCatalog GetRooms(string path)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(path);
            return new RoomCatalog(Path.GetFileNameWithoutExtension(path));
        }
    }
}