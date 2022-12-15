use Dungeon
GO


DELETE FROM dbo.Story;
DELETE FROM dbo.RoomCatalog;
DELETE from dbo.RoomExit;
DELETE FROM dbo.Room;

INSERT dbo.Story(Name)
Values('main');

Declare @MainStoryId int = SCOPE_IDENTITY();

INSERT dbo.RoomCatalog(Name) 
values('main catalog');

Declare @CatalogId int = SCOPE_IDENTITY();

INSERT dbo.Room
(Name, Narrative)
VALUES('entrance', 'You find yourself in a dark room. You can open the door, explore, or follow the line of torches.');


DECLARE @EntranceRoomId int = SCOPE_IDENTITY();



INSERT dbo.RoomExit
(RoomId, Keyword, RoomName)
Values
(@EntranceRoomId, 'door', 'rockRoom'),
(@EntranceRoomId, 'explore', 'exploreRoom'),
(@EntranceRoomId, 'torch', 'torchRoom');


