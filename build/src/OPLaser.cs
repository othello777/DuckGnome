using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;
using DuckGame.othello7_mod;

namespace DuckGame.othello7_mod
{
    [EditorGroup("DuckGnome")]
    public class OPlaser : Gun
    {

        public OPlaser(float xval, float yval)
          : base(xval, yval)
        {
            this.ammo = 99;
            this._ammoType = (AmmoType)new ATSilentMag();
            this._ammoType.penetration = 20;
            this._ammoType.range = 200f;
            this._accuracyLost = 0.0f;
            this.maxAccuracyLost = 1f;
            this._fullAuto = true;
            this._fireWait = 0.3f;
            this._type = "gun";
            base.graphic = new Sprite(this.GetPath("OPLaser.png"), 0f, 0f);
            this.center = new Vec2(12f, 15f);
            this.collisionOffset = new Vec2(-7f, -3f);
            this.collisionSize = new Vec2(14f, 7f);
            this._barrelOffsetTL = new Vec2(25f, 13f);
            this._fireSound = "deepMachineGun2";
            this._kickForce = 0.3f;
            this._holdOffset = new Vec2(-1f, 0.0f);
            this._bio = "Old faithful, the- FREAKING DEATH GUN!!";
            this._editorName = "Death Gun";
            this.physicsMaterial = PhysicsMaterial.Metal;
            
        }

        protected override void PlayFireSound()
        {
            SFX.Play(this._fireSound, 1f, 0.6f + Rando.Float(0.2f), 0.0f, false);
        }

        public override void Update()
        {
           // if (this._sprite.currentAnimation == "fire" && this._sprite.finished)
           //     this._sprite.SetAnimation("idle");
            base.Update();
        }

        public override void OnPressAction()
        {
            if (this.ammo > 0)
            {
             //   this._sprite.SetAnimation("fire");
                for (int index = 0; index < 3; ++index)
                {
                    Vec2 vec2 = this.Offset(new Vec2(-9f, 0.0f));
                    Vec2 hitAngle = this.barrelVector.Rotate(Rando.Float(1f), Vec2.Zero);
                    Level.Add((Thing)Spark.New(vec2.x, vec2.y, hitAngle, 0.1f));
                }
            }
            else
             //   this._sprite.SetAnimation("empty");
            this.Fire();
        }
    }
}
