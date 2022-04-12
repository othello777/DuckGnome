using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.othello7_mod
{
    [EditorGroup("DuckGnome")]
    class FakeSword : Gun
    {
        // Token: 0x0600091C RID: 2332 RVA: 0x00049EE4 File Offset: 0x000480E4
        public FakeSword(float xval, float yval) : base(xval, yval)
        {
            this.ammo = 1;
            this._ammoType = new AT9mm();
            this._ammoType.range = 700f;
            this._type = "gun";
            this.graphic = new Sprite("sword", 0f, 0f);
            this.center = new Vec2(6f, 18f);
            this.collisionOffset = new Vec2(-8f, -3f);
            this.collisionSize = new Vec2(16f, 9f);
            this._barrelOffsetTL = new Vec2(18f, 8f);
            this._fireSound = "shotgunFire2";
            this._kickForce = 7f;
            this._numBulletsPerFire = 7;
            this._holdOffset = new Vec2(-1f, 0f);
            this._bio = "Old faithful, the 9MM sword.";
            this._editorName = "Totally Sword";
            this.physicsMaterial = PhysicsMaterial.Metal;
        }

        // Token: 0x0600091D RID: 2333 RVA: 0x0004A051 File Offset: 0x00048251
        public override void Update()
        {
            base.Update();
        }

        // Token: 0x0600091E RID: 2334 RVA: 0x0004A090 File Offset: 0x00048290
        public override void OnPressAction()
        {
            if (this.ammo > 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    Vec2 pos = this.Offset(new Vec2(-9f, 0f));
                    Vec2 rot = base.barrelVector.Rotate(Rando.Float(1f), Vec2.Zero);
                    Level.Add(Spark.New(pos.x, pos.y, rot, 1f));
                }
            }
            base.Fire();
        }
    }
}
