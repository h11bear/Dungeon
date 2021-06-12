using System;
using Dungeon.Logic;

namespace Dungeon.Terminal {
    public class TerminalIo : DungeonIo {
        
        public void WriteLine() {
            Console.WriteLine();
        }
        public void WriteLine(string message) {
            Console.WriteLine(message);
        }

        public string ReadLine() {
            return Console.ReadLine();
        }

        public void Exit(int exitCode) {
            Environment.Exit(exitCode);
        }

    }
}