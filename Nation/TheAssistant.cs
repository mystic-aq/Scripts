﻿//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
using RBot;

public class TheAssistant
{
    public CoreBots Core => CoreBots.Instance;
    public CoreNation Nation = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Nation.TheAssistant();

        Core.SetOptions(false);
    }
}