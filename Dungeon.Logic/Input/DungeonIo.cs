namespace Dungeon.Logic.Input {
    public interface DungeonIo {
        void WriteLine();
        void WriteLine(string message);
        string ReadLine();
        void Exit(int exitCode);
    }
}