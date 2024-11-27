using Godot;
using System;

public partial class CoinDropper : Node2D
{
    [Export] public PackedScene coinScene;  

    public void DropCoin(Vector2 position)
    {
        if (coinScene != null)
        {
            var coin = (Coin)coinScene.Instantiate();
            coin.Position = position; 
            GetParent().AddChild(coin);  
        }
    }
}
