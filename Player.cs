using System.Reflection.Metadata;
using System.Runtime.Serialization;
using Godot;

public partial class Player : CharacterBody3D
{
    [Export]
    public int Speed {get; set;} = 20;
    [Export]
    public int jump {get; set;} = 24;
    
    private double mouse_sens = 0.3f;
    private double camara_angle = 0;
    private int _gravity = 9;

    public Node3D head;
    public Camera3D camara;

    private Vector3 _targetVelocity = Vector3.Zero;

    public override void _Ready()
    {
        head = GetNode<Node3D>("Pivot");
        camara = GetNode<Camera3D>("Pivot/UIView");
    }
    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseMotion)
        {
            InputEventMouseMotion mouseMotion = @event as InputEventMouseMotion;
            head.RotateY(-mouseMotion.Relative.X * 0.01f);
            camara.RotateX(-mouseMotion.Relative.Y * 0.01f);

        }
    }

    public override void _PhysicsProcess(double delta)
    {
        //Movement inputs basic factoring pls redo this shit with a fkin switch later on

        var direction = Vector3.Zero;

        if (Input.IsActionPressed("move_up")) 
        {
            direction.Z -= 1.0f;
        }
        if (Input.IsActionPressed("move_back")) 
        {
            direction.Z += 1.0f;
        }
        if (Input.IsActionPressed("move_right")) 
        {
            direction.X += 1.0f;
        }
        if (Input.IsActionPressed("move_left")) 
        {
            direction.X -= 1.0f;
        }


        if (direction != Vector3.Zero)
        {
            direction = direction.Normalized();
            //GetNode<Node3D>("Pivot").Basis = Basis.LookingAt(direction);
        }

        _targetVelocity.X =  direction.X * Speed;
        _targetVelocity.Z =  direction.Z * Speed;

        if (!IsOnFloor())
        {
            _targetVelocity.Y -= _gravity * (float)delta;
        }

        Velocity =  _targetVelocity;
        MoveAndSlide();

    }
}
