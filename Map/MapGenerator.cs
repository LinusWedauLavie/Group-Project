using Godot;
using System;


public partial class MapGenerator : Node2D
{
	public Node2D room1;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		room1 = (Node2D)ResourceLoader.Load<PackedScene>("res://Map/Scenes/Room1.tscn").Instantiate();
		
		Generator();
	}
		
	public void Generator ()
	{
		Random random = new Random();
		
		byte[] seed = new byte[10];
		random.NextBytes(seed);
		
		if (Convert.ToInt64(seed[0].ToString()) < 128)
		{
			Sprite2D Map = GetNode<Sprite2D>("StartRoom");
			Map.AddChild(room1);
		}
		if(Convert.ToInt64(seed[1].ToString()) < 128)
		{
			//Dann soll ein raum nach links gebaut werden
		}
		if(Convert.ToInt64(seed[2].ToString()) < 128)
		{
			//Dann soll ein raum nach rechts gebaut werden
		}
		if(Convert.ToInt64(seed[3].ToString()) < 128)
		{
			//Dann soll ein raum nach unten gebaut werden
		}
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
