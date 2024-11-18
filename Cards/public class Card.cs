using System;
public class Card
{
    public string Name { get; set; }
    public string Type { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public string Description { get; set; }
    public Action<Card, Card> Effect { get; set; } // Beispiel: Effekt der Karte


    public Card(string name, string type, int attack, int defense, string description, Action<Card, Card> effect = null)
    {
        Name = name;
        Type = type;
        Attack = attack;
        Defense = defense;
        Description = description;
        Effect = effect;
    }

    public void ApplyEffect(Card target)
    {
        Effect?.Invoke(this, target);
    }
}