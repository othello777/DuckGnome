﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

namespace DuckGame.othello7_mod
{
    [EditorGroup("DuckGnome")]
    public class RockLauncher : Gun
    {
        private SpriteMap _barrelSteam;
        private SpriteMap _netGunGuage;

        public RockLauncher(float xval, float yval)
          : base(xval, yval)
        {
            this.ammo = 9;
            this._ammoType = (AmmoType)new ATLaser();
            this._ammoType.range = 170f;
            this._ammoType.accuracy = 0.8f;
            this._ammoType.penetration = -1f;
            this._type = "gun";
            this.graphic = new Sprite(this.GetPath("rockLauncher.png"), 0.0f, 0.0f);
            // first number sideways second number updown less is down and back
            this.center = new Vec2(11f, 7f);
            this.collisionOffset = new Vec2(-8f, -4f);
            this.collisionSize = new Vec2(16f, 9f);
            this._barrelOffsetTL = new Vec2(27f, 6f);
            this._fireSound = "smg";
            this._fullAuto = true;
            this._fireWait = 1f;
            this._kickForce = 5f;
            this._netGunGuage = new SpriteMap("netGunGuage", 8, 8, false);
            this._barrelSteam = new SpriteMap("steamPuff", 16, 16, false);
            this._barrelSteam.center = new Vec2(0.0f, 14f);
            this._barrelSteam.AddAnimation("puff", 0.4f, false, 0, 1, 2, 3, 4, 5, 6, 7);
            this._barrelSteam.SetAnimation("puff");
            this._barrelSteam.speed = 0.0f;
            this._bio = "C02 powered, shoots rocks, hits ducks. Is that stubborn duck not moving? Why not whack it on the head.";
            this._editorName = "Rock Launcher";
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update()
        {
            this._netGunGuage.frame = 4 - Math.Min(this.ammo + 1, 4);
            if ((double)this._barrelSteam.speed > 0.0 && this._barrelSteam.finished)
                this._barrelSteam.speed = 0.0f;
            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
            if ((double)this._barrelSteam.speed > 0.0)
            {
                this._barrelSteam.alpha = 0.6f;
                this.Draw((Sprite)this._barrelSteam, new Vec2(9f, 1f), 1);
            }
            // first number sideways second number updown less is down and forward
            this.Draw((Sprite)this._netGunGuage, new Vec2(-10f, -6f), 1);
        }

        public override void OnPressAction()
        {
            if (this.ammo > 0)
            {
                --this.ammo;
                SFX.Play("netGunFire", 1f, 0.0f, 0.0f, false);
                this._barrelSteam.speed = 1f;
                this._barrelSteam.frame = 0;
                this.ApplyKick();
                Vec2 vec2 = this.Offset(this.barrelOffset);
                if (this.receivingPress)
                    return;
                Rock net = new Rock(vec2.x, vec2.y - 2f);
                Level.Add((Thing)net);
                this.Fondle((Thing)net);
                if (this.owner != null)
                    net.responsibleProfile = this.owner.responsibleProfile;
                net.clip.Add(this.owner as MaterialThing);
                net.hSpeed = this.barrelVector.x * 5f;
                net.vSpeed = (float)((double)this.barrelVector.y * 30.0 - 1.5);
            }
            else
                this.DoAmmoClick();

            if (ammo > 0)
            {
                base.OnPressAction();
                int num = 0;
                for (int index = 0; index < 2; ++index)
                {
                    RockSmoke musketSmoke = new RockSmoke((float)((double)this.x - 16.0 + (double)Rando.Float(32f) + (double)this.offDir * 10.0), this.y - 16f + Rando.Float(32f));
                    musketSmoke.depth = (Depth)((float)(0.899999976158142 + (double)index * (1.0 / 1000.0)));
                    if (num < 6)
                        musketSmoke.move.x -= (float)this.offDir * Rando.Float(0.1f);
                    if (num > 5 && num < 10)
                        musketSmoke.fly.x += (float)this.offDir * (2f + Rando.Float(7.8f));
                    Level.Add((Thing)musketSmoke);
                    ++num;
                }
                //this._tampInc = 0.0f;
                //this._tampTime = 0.5f;
                //this._tamped = false;
            }
        }

        public override void Fire()
        {
        }
    }
}

