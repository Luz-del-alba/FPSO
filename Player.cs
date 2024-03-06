using System.Runtime.Serialization;
using Godot;

public partial class Player : CharacterBody3D
{
    [Export]
    public int speed {get; set;} = 20;
    [Export]
    public int jump {get; set;} = 24;

    public override void _PhysicsProcess(double delta)
    {
        //Movement inputs basic factoring pls redo this shit with a fkin switch later on

        var direction = Vector3.Zero;

        if (Input.IsActionPressed("move_up")) 
        {
            direction.Y += 1.0f;
        }
        if (Input.IsActionPressed("move_down")) 
        {
            direction.Y -= 1.0f;
        }
        if (Input.IsActionPressed("move_right")) 
        {
            direction.X += 1.0f;
        }
        if (Input.IsActionPressed("move_left")) 
        {
            direction.X -= 1.0f;
        }

        

    }
}
