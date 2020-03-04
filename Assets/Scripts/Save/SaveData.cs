using System;

[Serializable]
public class SaveData
{
    public bool[] SaveLevels;
    public bool SaveConsoleUnlock;

    public SaveData(GlobalLevels Levels, Console console)
    {
        SaveConsoleUnlock = console.Unlocked;
        SaveLevels = new bool[GlobalLevels.GL.Levels.Length];
        for (int i = 0; i <= GlobalLevels.GL.Levels.Length -1; i++ )
        {
            SaveLevels[i] = GlobalLevels.GL.Levels[i];
        }
    }
}
