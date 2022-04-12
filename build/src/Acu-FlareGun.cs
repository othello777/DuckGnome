using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.othello7_mod
{
    [EditorGroup("DuckGnome")]
    public class FlareSniper : Gun
    {
        public FlareSniper(float xval, float yval)
          : base(xval, yval)
        {
            this.ammo = 4;
            this._ammoType = (AmmoType)new AT9mm();
            this._ammoType.combustable = true;
            this._type = "gun";
            base.graphic = new Sprite(this.GetPath("flareCannon.png"), 0f, 0f);
            this.center = new Vec2(8f, 8f);
            this.collisionOffset = new Vec2(-8f, -4f);
            this.collisionSize = new Vec2(16f, 9f);
            this._barrelOffsetTL = new Vec2(18f, 6f);
            this._fullAuto = true;
            this._fireWait = 1f;
            this._kickForce = 1f;
            this._barrelAngleOffset = 4f;
            this._editorName = "Flare Gun";
            this._bio = "For safety purposes, used to call help. What? No it's not a weapon. NO DON'T USE IT LIKE THAT!";
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
        }

        public override void OnPressAction()
        {
            if (this.ammo > 0)
            {
                --this.ammo;
                SFX.Play("netGunFire", 0.5f, Rando.Float(0.2f) - 0.4f, 0.0f, false);
                this.ApplyKick();
                if (this.receivingPress || !this.isServerForObject)
                    return;
                Vec2 vec2 = this.Offset(this.barrelOffset);
                AcuFlare flare = new AcuFlare(vec2.x, vec2.y, this, 30); // was 8 flames
                this.Fondle((Thing)flare);
                Vec2 vec = Maths.AngleToVec(this.barrelAngle - 0.065f/* + Rando.Float(-0.2f, 0.2f)*/);
                flare.hSpeed = vec.x * 14f;
                flare.vSpeed = vec.y * 14f;
                Level.Add((Thing)flare);
            }
            else
                this.DoAmmoClick();
        }

        public override void Fire()
        {
        }
    }
}