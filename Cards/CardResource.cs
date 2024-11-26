using Godot;

[Tool]
public partial class CardResource : Resource
{
    [Export] public string Name { get; set; }
    [Export] public string Type { get; set; }
    [Export] public int Attack { get; set; }
    [Export] public int Defense { get; set; }
    [Export] public string Description { get; set; }

    public CardResource() {}

    public CardResource(string name, string type, int attack, int defense, string description)
    {
        Name = name;
        Type = type;
        Attack = attack;
        Defense = defense;
        Description = description;
    }
}