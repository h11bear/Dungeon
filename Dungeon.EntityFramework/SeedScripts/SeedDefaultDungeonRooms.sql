use Dungeon
GO

DELETE FROM dbo.Story;
DELETE from dbo.RoomExit;
DELETE FROM dbo.Room;

INSERT dbo.Room
(Name, Narrative)
VALUES('entrance', 'You find yourself in a dark room. You can open the door, explore, or follow the line of torches.');

GO

DECLARE @EntranceRoomId int =
	(SELECT MAX(RoomId) from dbo.Room WHERE Name = 'entrance');

INSERT dbo.RoomExit
(RoomId, Keyword, RoomName)
Values
(@EntranceRoomId, 'door', 'rockRoom'),
(@EntranceRoomId, 'explore', 'exploreRoom'),
(@EntranceRoomId, 'torch', 'torchRoom');



INSERT dbo.Story(Name, EntranceRoomId)
Values('main', @EntranceRoomId);
