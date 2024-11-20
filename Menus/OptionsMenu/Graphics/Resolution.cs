using Godot;
using System;

public partial class Resolution : HBoxContainer
{
	public static readonly Godot.Collections.Dictionary RESOLUTIONDICTIONARY = new Godot.Collections.Dictionary
    {
        {"1152 x 648", new Vector2I(1152, 648)},
        {"1280 x 720", new Vector2I(1280, 720)},
        {"1920 x 1080", new Vector2I(1920, 1080)},
    };

	 public OptionButton resolutionOptionButton;

    public override void _Ready()
    {
        resolutionOptionButton = GetNode<OptionButton>("ResolutionOptionButton");
        AddResolutionItem();

        resolutionOptionButton.ItemSelected += OnResolutionSelected;
    }

	public void AddResolutionItem()
    {
        foreach (string key in RESOLUTIONDICTIONARY.Keys)
        {
            resolutionOptionButton.AddItem(key);
        }
    }

	public void OnResolutionSelected(long index)
{
    var key = resolutionOptionButton.GetItemText((int)index); 
    if (RESOLUTIONDICTIONARY.ContainsKey(key))
    {
        var resolution = (Vector2I)RESOLUTIONDICTIONARY[key];
        if (DisplayServer.WindowGetMode() == DisplayServer.WindowMode.Fullscreen)
        {
            // Temporär aus Fullscreen wechseln, dann Größe setzen, und wieder zurück
            DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
            DisplayServer.WindowSetSize(resolution);
            DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
        }
        else
        {
            DisplayServer.WindowSetSize(resolution);
        }
    }
}
}
