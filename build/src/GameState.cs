using System;

namespace DuckGame.othello7_mod
{
    [Flags]
    public enum GameState
    {
        Loading = 1,
        Playing = 2,
        TitleScreen = 4,
        RockThrowIntro = 8,
        RockThrow = 16, // 0x00000010
        Lobby = 32, // 0x00000020
        LevelEditor = 64, // 0x00000040
        InGame = Lobby | Playing, // 0x00000022
        Unknown = 0,
    }
}
