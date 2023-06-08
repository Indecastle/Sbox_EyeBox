



using Sandbox.UI;

namespace Sandbox.code;

[Spawnable]
[Library( "ent_eyebox_entity", Title = "Eye Box" )]
public partial class EyeBox : Prop {
	public WorldPanel panel;
	
	public override void Spawn()
	{
		base.Spawn();
		SetModel( "models/citizen_props/crate01.vmdl" );
	}

	protected override void UpdatePropData( Model model )
	{
		base.UpdatePropData(model);

		Health = 50;
	}	

	[GameEvent.Client.Frame]
	public void Frame()
	{
		if ( Health < 0 )
		{
			panel?.Delete();
			return;
		}

		if (panel is null)
		{
			panel = new();
			panel.PanelBounds = new Rect(-250, -630, 500, 485);
			panel.AddChild(new BoxScreen(panel, this));
		}
		else
		{
			panel.Rotation = Transform.RotationToWorld(Rotation.From(new(0, 180, 0)));
			panel.Position = Position - Rotation.Forward * 18.0f + Rotation.Up * 1;
		}

		// var pos = panel.Transform.PointToWorld(new Vector3(0, 0, 22.5f) + new Vector3(0, 5.5f, 0));
		// DebugOverlay.Sphere( pos, 0.5f, Color.White );
	}
	
	

	protected override void OnDestroy() {
		ClientOnDestroy();
		base.OnDestroy();
	}
	
	private void ClientOnDestroy() {
		panel?.Delete();
	}
}
