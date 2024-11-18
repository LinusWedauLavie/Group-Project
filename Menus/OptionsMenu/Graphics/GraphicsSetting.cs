using Godot;
using System;

public partial class GraphicsSetting : TabBar
{
	private WindowSetting windowSetting;
	private Resolution resolution;
    public override void _Ready()
    {
		windowSetting = GetNode<WindowSetting>("/root/Options_Menu/TabController/Graphics/PanelContainer/VBoxContainer/Window");

		resolution = GetNode<Resolution>("/root/Options_Menu/TabController/Graphics/PanelContainer/VBoxContainer/Resolution");
    }


}

public partial class WindowSetting : HBoxContainer
{
	static readonly string[] WINDOWMODEARRAY = {"Full-Screen", "Window Mode", "Borderless Window WIP"};
	public OptionButton windowOptionButton;
	public override void _Ready()
	{
		windowOptionButton = GetNode<OptionButton>("WindowOptionButton");
		AddWindowModeItems();
	}

	public void AddWindowModeItems(){
		foreach (var item in WINDOWMODEARRAY)
		{
			windowOptionButton.AddItem(item);
		}
		
	}
	public void OnWindowModeSelected(int index){
		switch (index)
		{
			case 0: // Fullscreen
				DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
				DisplayServer.WindowSetFlag(DisplayServer.WindowFlags.Borderless, true);
				break;
			case 1: //Window Mode
				DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
				DisplayServer.WindowSetFlag(DisplayServer.WindowFlags.Borderless, false);
				break;
			case 2: // Borderless Window
				DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
				DisplayServer.WindowSetMode(DisplayServer.WindowMode.Maximized);
				DisplayServer.WindowSetFlag(DisplayServer.WindowFlags.Borderless, true);
				break;
		}
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}

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
