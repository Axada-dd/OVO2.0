using AEAssist.CombatRoutine.View.JobView;

namespace Oblivion.Utils.JobView.HotKey;

public class HotKeyControl
{
    internal readonly string Name;
    internal IHotkeyResolver Slot;
    internal string ToolTip = "";

    internal HotKeyControl(string name) => Name = name;
}