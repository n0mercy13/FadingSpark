namespace Codebase.StaticData
{
    public static class Constants
    {
        public static class Game
        {
            public const float Epsilon = 0.01f;
        }

        public static class Level
        {
            public const string InitialLevelName = "Main";
        }

        public static class AssetPath
        {
            public const string Player = "Player/Player";
            public const string Bootstrapper = "Infrastructure/Bootstraper";

            public const string UIRoot = "UI/UIRoot";
            public const string HUD = "UI/HUD";
            public const string GameOverScreen = "UI/GameOverScreen";
            public const string MainMenu = "UI/MainMenu";
        }

        public static class StaticDataPaths
        {
            public const string Player = "StaticData/Player/PlayerStaticData";
            public const string Enemies = "StaticData/Enemies";
            public const string Weapons = "StaticData/Weapons";
        }

        public static class  Screen
        {
            public const int FPS_30 = 30;
            public const int FPS_60 = 60;
            public const int FPS_120 = 120;

            public const float BottomContainerTopBoundary = 0.15f;
            public const float TopContainerBottomBoundary = 0.9f;
        }
    }
}