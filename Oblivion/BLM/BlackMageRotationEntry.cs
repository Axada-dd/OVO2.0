using AEAssist.CombatRoutine.View.Setting;
using Oblivion.BLM.View;
using Oblivion.BLM.View.Setting;

namespace Oblivion.BLM;

public class BlackMageRotationEntry: IRotationEntry
{
    public string AuthorName { get; set; } = "OVO";
    public Rotation? Build(string settingFolder)
    {
        BlackMageACR.Init(settingFolder);
        return BlackMageACR.Build();
    }

    public IRotationUI GetRotationUI()
    {
      return baseUI.UI;
    }

    public void OnDrawSetting()
    {
        settingUI.Draw();
    }

    public void Dispose()
    {
        
    }
}