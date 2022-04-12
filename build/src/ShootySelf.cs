using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

namespace othello7_mod.src
{
    [EditorGroup("DuckGnome")]
    public class ShootySelf : Gun
    {
        private SpriteMap _barrelSteam;
        private SpriteMap _netGunGuage;

        public ShootySelf(float xval, float yval)
          : base(xval, yval)
        {
            this.ammo = 4;
            this._ammoType = (AmmoType)new ATLaser();
            this._ammoType.range = 170f;
            this._ammoType.accuracy = 0.8f;
            this._ammoType.penetration = -1f;
            this._type = "gun";
            this.graphic = new Sprite(this.GetPath("shootyself.png"), 0.0f, 0.0f);
            // first number sideways second number updown less is down and back
            this.center = new Vec2(25f, 15f);
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
            this._bio = "AT LAST! THE SOLUTION TO INFINITE MATTER!";
            this._editorName = "Shootyself";
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
            int index = 0;
            foreach(Thing thing in Level.current.things)
            {
                if (thing is ShootySelf)
                    index++;
            }

            if (this.ammo > 0 && index < 200)
            {
                --this.ammo;
                SFX.Play("netGunFire", 1f, 0.0f, 0.0f, false);
                this._barrelSteam.speed = 1f;
                this._barrelSteam.frame = 0;
                this.ApplyKick();
                Vec2 vec2 = this.Offset(this.barrelOffset);
                if (this.receivingPress)
                    return;
                ShootySelf net = new ShootySelf(vec2.x, vec2.y - 2f);
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
        }

        public override void Fire()
        {
        }
    }
}
