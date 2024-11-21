using Godot;
using System;


public partial class MapGenerator : Node2D
{
	public Node2D lastRoom;
	public Node2D shouldPlacedRoom;
	public Node2D Room1;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Room1 = (Node2D)ResourceLoader.Load<PackedScene>("res://Map/Scenes/Room1.tscn").Instantiate();
		lastRoom = GetNode<Node2D>("StartRoom");
		shouldPlacedRoom = Room1;
		Generator();
		
	}
		
	public void Generator ()
	{
		Random random = new Random();
		
		byte[] seed = new byte[10];
		random.NextBytes(seed);
		
		if (Convert.ToInt64(seed[0].ToString()) < 575)
		{
			Vector2I oben = new Vector2I();
			oben.X = 0;
			oben.Y = -324;
			shouldPlacedRoom.Position = lastRoom.Position + oben;
			lastRoom.AddChild(shouldPlacedRoom);
			lastRoom = shouldPlacedRoom;
			
			
		}
		if(Convert.ToInt64(seed[2].ToString()) <= 128)
		{
			
			Vector2I left = new Vector2I();
			left.X = 576;
			left.Y = 0;
			shouldPlacedRoom.Position = lastRoom.Position + left;
			lastRoom.AddChild(shouldPlacedRoom);
			lastRoom = shouldPlacedRoom;
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
