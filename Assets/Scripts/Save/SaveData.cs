using System;

[Serializable]
public class SaveData
{
    public bool[] SaveLevels;
    public bool SaveConsoleUnlock;

    public SaveData(LevelsManager Levels, Console console)
    {
        SaveConsoleUnlock = console.Unlocked;
        SaveLevels = new bool[Levels.LevelsUnlocked.Length];
        for (int i = 0; i <= Levels.LevelsUnlocked.Length -1; i++ )
        {
            SaveLevels[i] = Levels.LevelsUnlocked[i];
        }
    }
}
