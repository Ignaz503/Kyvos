using Kyvos.Maths;
using Kyvos.ImGUI;
using Kyvos.Core.Logging;

namespace FullyAutomatedGayLuxurySpaceCommunism;

public class MyUI : IUIComponent
{
    const int min = 3;
    const int max = 5;
    
    MainMenuBar bar;

    public MyUI()
    {
        bar = new MainMenuBar();

        var val = GetRandomNumberBetweenMinAnMax();
        for (int i = 0; i < val; i++)
        {
            var subChildren = GetRandomNumberBetweenMinAnMax();
            bar.AppendChild(SomeMenu(i, subChildren));
        }

        int GetRandomNumberBetweenMinAnMax()
            => Mathf.RoundToInt(Kyvos.Maths.Mathf.Map(Kyvos.Maths.Random.Value, 0, 1, min, max));
    }

    public void Show()
    {
        bar.Show();
    }

    Menu SomeMenu(int idx, int items) 
    {
        var menu = new Menu($"Menu {idx}");

        for (int i = 0; i < items; i++)
        {
            var text = $"Menu {idx} Item {i}";
            menu.AppendChild(new MenuItem(text, () => LogText(text)));
        }

        return menu;
    }

    void LogText(string text) 
    {
        Log.Information(text);
    }

}
