using System;
namespace DuckGame.src
{
    [EditorGroup("DuckGnome")]
    public class catcarrier : Holdable, IPlatform
    {
        private Sprite _pickupSprite;
        private Sprite _sprite;

        public catcarrier(float xval, float yval) : base(xval, yval)
        {
            //this.ammo = 1;
            this.graphic = new Sprite("", 0f, 0f);
            this._pickupSprite = new Sprite(this.GetPath("boomplate2.png"), 0f, 0f);
            this._sprite = new Sprite(this.GetPath("boomplate2.png"), 0f, 0f);
            this.center = new Vec2(6f, 18f);
            this.collisionOffset = new Vec2(-8f, -3f);
            this.collisionSize = new Vec2(16f, 9f);
            this._holdOffset = new Vec2(-1f, 0f);
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
            DevConsole.Log("CCVelocity: " + this.velocity.ToString());
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
