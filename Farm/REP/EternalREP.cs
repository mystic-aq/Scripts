﻿//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/ThroneofDarkness/CoreToD.cs
using RBot;
public class EternalREP
{
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreToD TOD = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        TOD.FourthDimensionalPyramid();

        //Farm.UseBoost(ChangeToBoostID, RBot.Items.BoostType.Reputation, false);

        Farm.EternalREP();

        Core.SetOptions(false);
    }
}