//cs_include Scripts/CoreBots.cs
//cs_include Scripts/Darkon/CoreDarkon.cs
using RBot;

public class ThirdErrand
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreDarkon Darkon = new CoreDarkon();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Darkon.ThirdErrand();

        Core.SetOptions(false);
    }
}