using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.src
{
    [EditorGroup("DuckGnome")]
    class SuperLaser : Gun
    {
        // Token: 0x06001ABE RID: 6846 RVA: 0x00122AD8 File Offset: 0x00120CD8
        public SuperLaser(float xval, float yval) : base(xval, yval)
        {
            this.ammo = 99;
            this._ammoType = new ATPewPew();
            this._type = "gun";
            this.graphic = new Sprite("pewpewLaser", 0f, 0f);
            this.center = new Vec2(16f, 16f);
            this.collisionOffset = new Vec2(-8f, -3f);
            this.collisionSize = new Vec2(16f, 7f);
            this._barrelOffsetTL = new Vec2(31f, 15f);
            this._fireSound = "laserRifle";
            this._fullAuto = true;
            this._fireWait = 0.5f;
            this._kickForce = 0.3f;
            this._holdOffset = new Vec2(0f, 0f);
        }
    }
}
