using System;
using System.Collections.Generic;
using DuckGame;

namespace DuckGame.othello7_mod
{
    [EditorGroup("DuckGnome")]
    public class SDP : Gun
    {
        public SDP(float xval, float yval) : base(xval, yval)
        {
            this.ammo = 4;
            this._ammoType = new ATPlasmaBlaster();
            this._ammoType.range = 500f;
            this._ammoType.accuracy = 0.8f;
            this._ammoType.penetration = 10f;
            this._type = "gun";
            base.graphic = new Sprite(this.GetPath("supertiny.png"), 0f, 0f);
            this.center = new Vec2(16f, 16f);
            this.collisionOffset = new Vec2(-6f, -4f);
            this.collisionSize = new Vec2(12f, 8f);
            this._barrelOffsetTL = new Vec2(20f, 15f);
            this._fireSound = (GetPath("SFX\\blorst"));
            this._kickForce = 5f;
            this._editorName = "SuperDualingPistol";
            this._numBulletsPerFire = 5;
            

        }

    }
}

