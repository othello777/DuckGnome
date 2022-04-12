using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.othello7_mod
{
    [EditorGroup("DuckGnome")]
    public class TurboAk47 : Gun
    {
    public TurboAk47(float xval, float yval) : base(xval, yval)
        {
            this.ammo = 450;
            //this.infiniteAmmoVal = true;
            this._ammoType = (AmmoType)new AT9mm();
            this._ammoType.range = 200f;
            this._ammoType.accuracy = 0.85f;
            this._ammoType.penetration = 2f;
            this._type = "gun";
            base.graphic = new Sprite(this.GetPath("buffak47.png"), 0f, 0f);
            this.center = new Vec2(16f, 15f);
            this.collisionOffset = new Vec2(-8f, -3f);
            this.collisionSize = new Vec2(18f, 10f);
            this._barrelOffsetTL = new Vec2(32f, 14f);
            this._fireSound = "deepMachineGun2";
            this._fullAuto = true;
            this._fireWait = 0.1f;
            this._kickForce = 3f;
            this.loseAccuracy = 0.1f;
            this.maxAccuracyLost = 0.5f;
            this._editorName = "Turbo AK47";
        }
    }
}
