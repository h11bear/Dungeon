use Dungeon
GO


EXEC sp_MSforeachtable "ALTER TABLE ? NOCHECK CONSTRAINT all"

DELETE FROM dbo.Story;
DELETE from dbo.RoomExit;
DELETE FROM dbo.Room;


INSERT dbo.Story(Name)
Values('main');

Declare @MainStoryId int = SCOPE_IDENTITY();


INSERT dbo.Room
(Name, Narrative, StoryId)
VALUES('entrance', 'You find yourself in a dark room. You can open the door, explore, or follow the line of torches.', @MainStoryId);

DECLARE @EntranceRoomId int = SCOPE_IDENTITY();

update dbo.Story set EntranceRoomId = @EntranceRoomId where StoryId = @MainStoryId;


INSERT dbo.RoomExit
(RoomId, Keyword, RoomName)
Values
(@EntranceRoomId, 'door', 'rockRoom'),
(@EntranceRoomId, 'explore', 'exploreRoom'),
(@EntranceRoomId, 'torch', 'torchRoom');


exec sp_MSforeachtable @command1="print '?'", @command2="ALTER TABLE ? WITH CHECK CHECK CONSTRAINT all"

