using System;
using Sandbox;
using Sandbox.code;
using Sandbox.UI;
using Sandbox.UI.Components;

public partial class BoxScreen : Panel {
    private Eye eyeLeft, eyeRight;

    public WorldPanel parentWorldPanel;
    public EyeBox pawn;
    private Vector3 offset = new(0, 0, 22.5f);

    public BoxScreen(WorldPanel worldPanel, EyeBox pawn) {
        StyleSheet.Load("UI/BoxScreen.scss");
        this.pawn = pawn;
        parentWorldPanel = worldPanel;
        
        eyeLeft = new Eye(this, 25, 50 );
        eyeRight = new Eye(this, 140, 50 );
    }
    
    public override void Tick() {
        if (pawn is null || !pawn.IsValid) Delete();

        var (dirLeft, cosLeft) = GetEyeDir(new(0, -6f, 0));
        var (dirRight, cosRight) = GetEyeDir(new(0, 5.5f, 0));
        eyeLeft.SetDir(dirLeft, cosLeft);
        eyeRight.SetDir(dirRight, cosRight);
    }

    private (Vector2 dir, float cos) GetEyeDir(Vector3 additionalOffset)
    {
        var offsetPos = parentWorldPanel.Transform.PointToWorld(offset + additionalOffset);
        var cameraLocalPos = parentWorldPanel.Transform.WithPosition(offsetPos).PointToLocal(Camera.Position).Normal;
        
        var vec2 = new Vector2(cameraLocalPos.y, -cameraLocalPos.z);
        var cos = vec2.Distance(Vector2.Zero);

        return (vec2.Normal, cos);
    }
}
