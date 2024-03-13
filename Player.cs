using System;
using System.Reflection.Metadata;
using System.Runtime.Serialization;
using Godot;

public partial class Player : CharacterBody3D
{
    [Export]
    public int Speed {get; set;} = 20;
    [Export]
    public int jump {get; set;} = 8;
    
    private double mouse_sens = 0.3f;
    private double camara_angle = 0;
    private int _gravity = 18;

    const float acceleration = 0.5f;

    public Node3D head;
    public Camera3D camara;

    private Vector3 _targetVelocity = Vector3.Zero;

    public override void _Ready()
    {
        head = GetNode<Node3D>("Pivot");
        camara = GetNode<Camera3D>("Pivot/UIView");

        Input.MouseMode = Input.MouseModeEnum.Captured;
    }
    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseMotion)
        {
            InputEventMouseMotion mouseMotion = @event as InputEventMouseMotion;
            head.RotateY(-mouseMotion.Relative.X * 0.01f);
            camara.RotateX(-mouseMotion.Relative.Y * 0.01f);

            Vector3 camaraRot =  camara.Rotation;
            camaraRot.Y =  Mathf.Clamp(camaraRot.Y, Mathf.DegToRad(-80f), Mathf.DegToRad(80f));
            camara.Rotation = camaraRot;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        //Movement inputs basic factoring pls redo this shit with a fkin switch later on

        Vector2 inputDir = Input.GetVector("move_left", "move_right", "move_up", "move_back");
        Vector3 direction = (head.GlobalTransform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();


        if (direction != Vector3.Zero)
        {
            _targetVelocity.X =  direction.X * Speed;
            _targetVelocity.Z =  direction.Z * Speed;
            //GetNode<Node3D>("Pivot").Basis = Basis.LookingAt(direction);
        } else 
        {
            _targetVelocity.X = Mathf.MoveToward(_targetVelocity.X, 0 , 1.2f);
            _targetVelocity.Z = Mathf.MoveToward(_targetVelocity.Z, 0 , 1.2f);
        }

        if (IsOnFloor() && Input.IsActionPressed("jump"))
        {
            _targetVelocity.Y += jump;
        }

        if (!IsOnFloor())
        {
            _targetVelocity.Y -= _gravity * (float)delta;
        }

        Velocity =  _targetVelocity;
        MoveAndSlide();

    }
}
