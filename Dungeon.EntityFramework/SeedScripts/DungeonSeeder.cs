using Dungeon.EntityFramework.Data;
using Dungeon.Logic.Model;
using Microsoft.Data.SqlClient;

namespace Dungeon.EntityFramework.SeedScripts;

public class DungeonSeeder : IDisposable
{

    public DungeonSeeder(string databaseServer)
    {
        SqlConnectionStringBuilder cb = new();
        cb.IntegratedSecurity = true;
        cb.DataSource = databaseServer;
        cb.InitialCatalog = "Dungeon";
        cb.TrustServerCertificate = true;

        _dungeonContext = new DungeonContext(cb.ConnectionString);


    }
    private DungeonContext _dungeonContext;

    public void SeedDefaultDungeon()
    {
        if (!_dungeonContext.Stories.Any())
        {
            Room entranceRoom = new("entrance", "You find yourself in a dark room. You can open the door, explore, or follow the line of torches.");
            Room exploreRoom = new("exploreRoom", "You look around the room and see a faint light glowing around the corner. There is also what looks like a bottomless pit across the room. What do you inspect first?");
            Room torchRoom = new("torchRoom", "You follow the line of torches and are greeted with a huge brown door. You carefully open it and see a mossy green monster staring at you hungrily! Do you fight it or run away?");
            Room glowingLightRoom = new("glowingLightRoom", "What a disappointment, nothing is going on in the glowing light room!");
            Room pitRoom = new("pitRoom", "You jump down the pit and with great relief splatter on the rocks below.  This is much better than getting eaten by a monster!");
            Room rockRoom = new("rockRoom", "You open the door and find a rock blocking your path. Do you move the rock? Or squeeze by it and continue on the path?");
            Room moveRockRoom = new("moveRockRoom", "You move the rock and see a bright blue sky with billowing white clouds. You've escaped!");
            Room monsterDoorRoom = new("monsterDoorRoom", "You squeeze past the rock and continue on your journey to try and escape this dungeon. You notice that there are some torches lining the walls. You look up and see a giant snake with two heads staring at you! Do you fight or try to turn around?");
            Room fightChoiceRoom = new("fightChoiceRoom", "Next to you is a sword and a magic wand. Which do you choose to use agaist the beastly snake?");
            Room eatToDeathRoom = new("eatToDeathRoom", "You turn around and begin running back the way you came. The snake grabs your shirt with it's mouth from behind and swallows you whole.You coward.");
            Room magicWandRoom = new("magicWandRoom", "You pick up the magic wand and try to zap a lightning bolt at the snake. The wand falters and instead electricutes you. You are fried.");
            Room swordRoom = new("swordRoom", "You grab the sword, run up to the snake, jump, and slice off its head before it has time to react. You successfully slay the beast. Do you climb up the ladder at the back of the room or do you explore?");
            Room elephantRoom = new("elephantRoom", "You climb up the tall ladder and step onto the creaky wooden floor. You gasp. A gigantic elephant with mean red eyes stares at you. Do you fight it? Or try to get back down the ladder and look for another exit.");
            Room babySnakeRoom = new("babySnakeRoom", "You look around the room and see a giant web-like structure in the far left corner. You walk up to it and poke it with your sword. Thousands of tiny baby snakes come pouring out of the structure. You try to fight them off but there are too may. They eat you alive.");
            Room fightElephantRoom = new("fightElephantRoom", "You charge torwards the elephant with your sword still in hand. The elephant sucks you up with its trunk before you are able to stab it. You die.");
            Room runAwayRoom = new("runAwayRoom", "You turn around and try to climb down the ladder as fast as you can. It begins to rock and you fall off. The elephant tries to come down the ladder but it breaks off and it crashes into cave wall opening a small crack outside.  You crawl through and escape the safety, whew!");


            _dungeonContext.Rooms.AddRange([
                entranceRoom,
                exploreRoom,
                glowingLightRoom,
                pitRoom,
                rockRoom,
                moveRockRoom,
                monsterDoorRoom,
                fightChoiceRoom,
                eatToDeathRoom,
                magicWandRoom,
                swordRoom,
                elephantRoom,
                babySnakeRoom,
                fightElephantRoom,
                runAwayRoom
            ]);

            _dungeonContext.Stories.Add(new Story("main", entranceRoom));
            _dungeonContext.SaveChanges();

            entranceRoom.WithExit("explore", exploreRoom).WithExit("follow", torchRoom).WithExit("door", moveRockRoom);
            exploreRoom.WithExit("light", glowingLightRoom).WithExit("pit", pitRoom);
            rockRoom.WithExit("move", moveRockRoom).WithExit("squeeze", monsterDoorRoom);
            monsterDoorRoom.WithExit("fight", fightChoiceRoom).WithExit("turn", eatToDeathRoom);
            fightChoiceRoom.WithExit("wand", magicWandRoom).WithExit("sword", swordRoom);
            swordRoom.WithExit("ladder", elephantRoom).WithExit("explore", babySnakeRoom);
            elephantRoom.WithExit("fight", fightElephantRoom).WithExit("ladder", runAwayRoom);


            _dungeonContext.SaveChanges();
        }
    }

    public void Dispose()
    {
        _dungeonContext.Dispose();
    }
}