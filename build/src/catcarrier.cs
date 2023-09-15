using System;
namespace DuckGame.src
{
    [EditorGroup("DuckGnome")]
    public class catcarrier : Holdable, IPlatform
    {
        public catcarrier(float xval, float yval) : base(xval, yval)
        {
            //this.ammo = 1;
            this.graphic = new Sprite(this.GetPath("catcarrier.png"));
            this.center = new Vec2(6f, 6f);
            this.collisionOffset = new Vec2(-4f, -6f);
            this.collisionSize = new Vec2(18f, 14f);
            this._holdOffset = new Vec2(-1f, -3f);
            //this._bio = "Best prop in the game";
            this._editorName = "Cat Carrier";
            this.physicsMaterial = PhysicsMaterial.Plastic;
        }

        public override void OnPressAction()
        {
            if (this.duck == null)
                return;

            this.duck.ThrowItem();
            this.velocity = new Vec2(velocity.x * 8, velocity.y);
            //DevConsole.Log("CCVelocity: " + this.velocity.ToString());
        }

        public override void OnImpact(MaterialThing with, ImpactedFrom from)
        {
            if (Math.Abs(velocity.x) > 4f)
            {
                if (with is Duck)
                {
                    ((Duck)with).Kill(new DTImpact(this));
                    SFX.Play((GetPath("SFX\\gmoddeath.wav")), 20f, 0.0f, 0.0f, false);
                }
                SFX.Play((GetPath("SFX\\gmodhit.wav")), 20f, 0.0f, 0.0f, false);
            }
            base.OnImpact(with, from);
        }
    }
}
