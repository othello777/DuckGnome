using System;
namespace DuckGame.src
{
    public class prototypemagblaster : Gun
    {
        public prototypemagblaster(float xval, float yval)
          : base(xval, yval)
        {
            ammo = 12;
            _ammoType = new ATMag
            {
                penetration = 0.4f,
                range = 470f,
                rangeVariation = 70f,
                accuracy = 0.2f
            };
            wideBarrel = true;
            barrelInsertOffset = new Vec2(3f, 1f);
            _type = "gun";
            this.graphic = new Sprite(this.GetPath("prototypemag.png"));
            center = new Vec2(12f, 8f);
            collisionOffset = new Vec2(-8f, -7f);
            collisionSize = new Vec2(16f, 14f);
            _barrelOffsetTL = new Vec2(20f, 5f);
            _fireSound = "magShot";
            _kickForce = 6f;
            _fireRumble = RumbleIntensity.Kick;
            _holdOffset = new Vec2(1f, 0f);
            _numBulletsPerFire = 6;
            loseAccuracy = 0.2f;
            maxAccuracyLost = 0.12f;
            _editorName = "Prototype Mag Blaster";
            editorTooltip = "The extra preferred gun for enacting justice in a post-apocalyptic megacity.";
            physicsMaterial = PhysicsMaterial.Metal;
        }

    }
}
