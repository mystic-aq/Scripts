﻿//cs_include Scripts/CoreBots.cs
//cs_include Scripts/Legion/CoreLegion.cs
using RBot;

public class BrightParagonLightvsDark
{
	public CoreBots Core = new CoreBots();
	public CoreLegion Legion = new CoreLegion();

	public void ScriptMain(ScriptInterface bot)
	{
		Core.SetOptions();

		Core.AddDrop("Legion Token", "Legion Token Pile");

		Legion.LTBrightParagon();

		Core.SetOptions(false);
	}
}