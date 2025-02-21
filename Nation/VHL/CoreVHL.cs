//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Nation/AssistingCragAndBamboozle[Mem].cs
using RBot;

public class CoreVHL
{
    // [Can Change]
    // True = When possible, it will use "Assisting Crag and Bamboozle" to get an additional Elders' Blood per day. Needs Crag and Bamboozle and is Legend-Only.
    // False = It will automatically check if you got the things, but if you want to turn this off either way, heres the option.
    // Recommended: true
    private bool UseSparrowMethod = true;

    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();
    public CoreDailies Daily = new();
    public CoreNation Nation = new();
    public AssistingCragAndBamboozle ACAB = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.RunCore();
    }

    public void GetVHL(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Void Highlord"))
            return;

        VHLChallenge(15);
        VHLCrystals();

        Core.BuyItem("tercessuinotlim", 1355, "Void Highlord");

        if (rankUpClass)
            Adv.rankUpClass("Void Highlord");
    }

    public void VHLChallenge(int quant)
    {
        if (Core.CheckInventory("Roentgenium of Nulgath", quant))
            return;

        if (Core.CBOBool("VHL_Sparrow", out bool _UseSparrowMethod))
            UseSparrowMethod = _UseSparrowMethod;

        Core.Logger("Getting Void HighLord Challenge prerequisites");
        Farm.Experience(80);
        Core.AddDrop(Nation.bagDrops);
        if (!Core.CheckInventory(ChallengeRewards, toInv: false))
            Core.AddDrop(ChallengeRewards);
        Core.AddDrop("Roentgenium of Nulgath");

        Core.KillMonster("tercessuinotlim", "m4", "Right", "Shadow of Nulgath", "Hadean Onyx of Nulgath", isTemp: false);

        Core.FarmingLogger("Roentgenium of Nulgath", quant);
        int CurrentRoent = Bot.Inventory.GetQuantity("Roentgenium of Nulgath");
        while (!Bot.ShouldExit() && !Core.CheckInventory("Roentgenium of Nulgath", quant))
        {
            Core.EnsureAccept(5660);

            Nation.FarmVoucher(false);
            Farm.BlackKnightOrb();
            if (!Core.CheckInventory("Nulgath Shaped Chocolate"))
            {
                Farm.Gold(2000000);
                Core.BuyItem("citadel", 44, 38316);
            }
            Core.BuyItem("yulgar", 16, "Aelita's Emerald");
            Nation.FarmUni13(1);
            Nation.FarmGemofNulgath(20);
            Nation.EmblemofNulgath(20);
            Nation.EssenceofNulgath(50);
            Nation.SwindleBulk(100);
            Nation.ApprovalAndFavor(300, 300);

            if (!Core.CheckInventory("Elders' Blood", ((quant - CurrentRoent) > 5 ? 5 : (quant - CurrentRoent))))
                Daily.EldersBlood();
            _SparrowMethod(((quant - CurrentRoent) > 5 ? 5 : (quant - CurrentRoent)));

            if (!Core.CheckInventory("Elders' Blood"))
                Core.Logger($"Not enough \"Elders' Blood\", please do the daily {2 - Bot.Inventory.GetQuantity("Elders' Blood")} more times (not today)", messageBox: true, stopBot: true);

            Core.EnsureComplete(5660);
            Bot.Wait.ForPickup("Roentgenium of Nulgath");
        }

        Core.ToBank(ChallengeRewards);
    }
    private string[] ChallengeRewards = { "Void Highlord Armor", "Helm of the Highlord", "Highlord's Void Wrap" };

    public void VHLCrystals()
    {
        if (Core.CheckInventory("Void Crystal A") && Core.CheckInventory("Void Crystal B"))
            return;

        if (Core.CBOBool("VHL_Sparrow", out bool _UseSparrowMethod))
            UseSparrowMethod = _UseSparrowMethod;

        Core.Logger("Obtaining Void Crystal A & Void Crystal B");
        Core.AddDrop(Nation.bagDrops);

        Nation.FarmUni13(1);
        Nation.FarmUni10(200);
        Nation.FarmGemofNulgath(150);
        Nation.FarmDarkCrystalShard(200);
        Nation.FarmDiamondofNulgath(200);
        Nation.FarmBloodGem(30);
        Nation.FarmTotemofNulgath(15);
        Nation.SwindleBulk(200);

        if (!Core.CheckInventory("Elders' Blood", 2))
            Daily.EldersBlood();
        _SparrowMethod(2);

        if (!Core.CheckInventory("Elders' Blood", 2))
            Core.Logger($"Not enough \"Elders' Blood\", please do the daily {2 - Bot.Inventory.GetQuantity("Elders' Blood")} more times (not today)", messageBox: true, stopBot: true);

        Core.BuyItem("tercessuinotlim", 1355, "Void Crystal A");
        Core.BuyItem("tercessuinotlim", 1355, "Void Crystal B");
    }

    private void _SparrowMethod(int EldersBloodQuant)
    {
        if (!UseSparrowMethod || !Core.IsMember || !Core.CheckInventory(Nation.CragName) || Core.CheckInventory("Elders' Blood", EldersBloodQuant))
            return;

        Core.Logger("Sparrow Method is enabled, the bot will now max out Totems, BloodGems, Uni19 and Vouchers in order to get another Elders' Blood. This may take a while");

        Core.AddDrop("Totem of Nulgath", "Blood Gem of Nulgath", "Voucher of Nulgath", "Voucher of Nulgath (non-mem)");
        Nation.FarmTotemofNulgath();
        Nation.FarmBloodGem();
        if (!Core.CheckInventory("Unidentified 19"))
        {
            while (!Bot.ShouldExit() && !Core.CheckInventory("Receipt of Swindle", 6))
                Nation.SwindleReturn();
            Core.BuyItem("tercessuinotlim", 1951, "Unidentified 19");
        }
        Nation.FarmVoucher(false);
        Nation.FarmVoucher(true);
        ACAB.AssistingCandB();
    }
}