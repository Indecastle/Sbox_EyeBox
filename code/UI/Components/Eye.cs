using System;

namespace Sandbox.UI.Components;

public class Eye : Panel
{
	public Panel pupil;
    
	public Eye(Panel parent,float x, float y) : base(parent)
	{
		Classes = "cornea";
		
		pupil = new() {Classes = "pupil"};
		AddChild(pupil);
    
		Style.Left = x;
		Style.Top = y;
	}

	public void SetDir(Vector2 dir, float cos)
	{
		var mult = Math.Clamp(cos*17, -17, 17);
		pupil.Style.Left = dir.x * mult;
		pupil.Style.Top = dir.y * mult;
	}
}
