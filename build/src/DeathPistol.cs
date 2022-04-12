using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

namespace othello7_mod.src
{
    [EditorGroup("DuckGnome")]
    public class DeathPistol : Gun
    {
        // Token: 0x06001536 RID: 5430 RVA: 0x000CE360 File Offset: 0x000CC560
        public DeathPistol(float xval, float yval) : base(xval, yval)
        {
            this.ammo = 6;
            this._ammoType = new AT9mm();
            this._type = "gun";
            this._sprite = new SpriteMap(this.GetPath("deathpistol.png"), 18, 20, false);
            SpriteMap sprite = this._sprite;
            string name = "idle";
            float speed = 1f;
            bool looping = true;
            int[] frames = new int[1];
            sprite.AddAnimation(name, speed, looping, frames);
            this._sprite.AddAnimation("fire", 0.1f, false, new int[]
            {
                3,
                3,
                2,
                2,
                1,
                1
            });
            this._sprite.AddAnimation("empty", 1f, true, new int[]
            {
                0
            });
            this.graphic = this._sprite;
            this.center = new Vec2(9f, 14f);
            this.collisionOffset = new Vec2(-8f, -3f);
            this.collisionSize = new Vec2(16f, 9f);
            this._barrelOffsetTL = new Vec2(18f, 2f);
            this._fireSound = "pistolFire";
            this._kickForce = 3f;
            this._holdOffset = new Vec2(-1f, 0f);
            this.loseAccuracy = 0.1f;
            this.maxAccuracyLost = 0.6f;
            this._bio = "Dr.Death realized the Deathray took too long to scan items, so he made this prototype...";
            this._editorName = "Death Pistol";
            this.physicsMaterial = PhysicsMaterial.Metal;
        }
        public override void Update()
        {
            if (this._sprite.currentAnimation == "fire" && this._sprite.finished)
            {
                this._sprite.SetAnimation("idle");
            }
            base.Update();
        }

        // Token: 0x06001538 RID: 5432 RVA: 0x000CE4D0 File Offset: 0x000CC6D0
        public override void OnPressAction()
        {
            if (this.ammo > 0)
            {
                --this.ammo;

                this._sprite.SetAnimation("fire");
                for (int i = 0; i < 3; i++)
                {
                    Vec2 vec2  = this.Offset(new Vec2(-9f, 0f));
                    Vec2 hitAngle = base.barrelVector.Rotate(Rando.Float(1f), Vec2.Zero);
                    Level.Add(Spark.New(vec2.x, vec2.y, hitAngle, 0.1f));
                }

                #region lasershoot
                SFX.Play("laserBlast", 1f, 0f, 0f, false);
                Duck duck = this.owner as Duck;
                if (duck != null)
                {
                    duck.sliding = true;
                    duck.crouch = true;
                    Vec2 barrelVector = base.barrelVector;
                    duck.hSpeed -= barrelVector.x * 9f;
                    duck.vSpeed -= barrelVector.y * 9f + 3f;
                    duck.CancelFlapping();
                }
                Vec2 vec = this.Offset(base.barrelOffset);
                Vec2 target = this.Offset(base.barrelOffset + new Vec2(300f, 0f)) - vec;
                if (base.isServerForObject)
                {
                    //Global.data.laserBulletsFired.valueInt++;
                }
                Level.Add(new DeathBeam(vec, target)
                {
                    isLocal = base.isServerForObject
                });
                //this.doBlast = true;
                #endregion

            }
            else
            {
                this._sprite.SetAnimation("empty");
                DoAmmoClick();
            }

            //base.Fire();
        }

        private SpriteMap _sprite;
    }
}