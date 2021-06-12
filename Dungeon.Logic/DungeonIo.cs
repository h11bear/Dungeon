namespace Dungeon.Logic {
    public interface DungeonIo {
        void WriteLine();
        void WriteLine(string message);
        string ReadLine();
        void Exit(int exitCode);
    }
}