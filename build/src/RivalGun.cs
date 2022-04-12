using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

namespace DuckGame
{
    [EditorGroup("DuckGnome")]
    [BaggedProperty("isFatal", false)]
    class RivalGun : Gun
    {
        // Token: 0x0600176F RID: 5999 RVA: 0x000E123C File Offset: 0x000DF43C
        public RivalGun(float xval, float yval) : base(xval, yval)
        {
            this.realammo = 150;
            this.ammo = 99;
            this._ammoType = new ATLaser();
            this._ammoType.range = 170f;
            this._ammoType.accuracy = 0.8f;
            this._type = "gun";
            this._sprite = new SpriteMap(this.GetPath("chaosdart.png"), 32, 32, false);
            this.graphic = this._sprite;
            this.center = new Vec2(16f, 16f);
            this.collisionOffset = new Vec2(-8f, -4f);
            this.collisionSize = new Vec2(16f, 9f);
            this._barrelOffsetTL = new Vec2(29f, 14f);
            this._fireSound = "smg";
            this._fullAuto = true;
            this._fireWait = 1.1f;
            this._wait = _fireWait;
            this._kickForce = 1f;
            this.flammable = 0.8f;
            this._barrelAngleOffset = 8f;
            this.physicsMaterial = PhysicsMaterial.Plastic;
            this._editorName = "Chaos Dart";
        }

        // Token: 0x06001770 RID: 6000 RVA: 0x00005719 File Offset: 0x00003919
        public override void Initialize()
        {
            base.Initialize();
        }

        // Token: 0x06001771 RID: 6001 RVA: 0x00011452 File Offset: 0x0000F652
        public override void UpdateFirePosition(SmallFire f)
        {
            f.position = this.Offset(new Vec2(10f, 0f));
        }

        // Token: 0x06001772 RID: 6002 RVA: 0x000E1368 File Offset: 0x000DF568
        public override void UpdateOnFire()
        {
            if (base.onFire)
            {
                this._burnWait -= 0.01f;
                if (this._burnWait < 0f)
                {
                    Level.Add(SmallFire.New(10f, 0f, 0f, 0f, false, this, false, this, false));
                    this._burnWait = 1f;
                }
                if (this.burnt < 1f)
                {
                    this.burnt += 0.001f;
                }
            }
        }

        // Token: 0x06001773 RID: 6003 RVA: 0x000E13EC File Offset: 0x000DF5EC
        public override void Update()
        {
            if (this._wait > 0f)
            {
                this._wait -= 0.15f;
            }
            if (this._wait < 0f)
            {
                this._wait = 0f;
            }
            if (!this.burntOut && this.burnt >= 1f)
            {
                this._sprite.frame = 1;
                Vec2 vec = this.Offset(new Vec2(10f, 0f));
                Level.Add(SmallSmoke.New(vec.x, vec.y));
                this._onFire = false;
                this.flammable = 0f;
                this.burntOut = true;
            }
            base.Update();
        }

        // Token: 0x06001774 RID: 6004 RVA: 0x0001146F File Offset: 0x0000F66F
        protected override bool OnBurn(Vec2 firePosition, Thing litBy)
        {
            base.onFire = true;
            return true;
        }

        // Token: 0x06001775 RID: 6005 RVA: 0x00005721 File Offset: 0x00003921
        public override void Draw()
        {
            base.Draw();
        }

        // Token: 0x06001776 RID: 6006 RVA: 0x000E1468 File Offset: 0x000DF668
        public override void OnPressAction()
        {
            
        }

        // Token: 0x06001777 RID: 6007 RVA: 0x00003424 File Offset: 0x00001624
        public override void Fire()
        {
            if (ammo < 5 && realammo > 5)
            {
                ammo = 99;
            }
            if(realammo < 5 && ammo > 5)
            {
                ammo = 4;
            }

            if (this.ammo > 0 && this._wait == 0f)
            {
                if (this._burnLife <= 0f)
                {
                    SFX.Play("dartStick", 0.5f, -0.1f + Rando.Float(0.2f), 0f, false);
                    return;
                }
                this.ammo--;
                SFX.Play("dartGunFire", 0.5f, -0.2f + Rando.Float(0.3f), 0f, false);
                this.kick = 1f;
                if (!this.receivingPress && base.isServerForObject)
                {
                    Vec2 vec = this.Offset(base.barrelOffset);
                    for (int i = 1; i < 4; i++)
                    {
                        float num = base.barrelAngle;// + Rando.Float(-0.1f, 0.1f);
                        Dart dart = new Dart(vec.x, vec.y + (i * 9 - 16), this.owner as Duck, -num);
                        base.Fondle(dart);
                        if (base.onFire)
                        {
                            Level.Add(SmallFire.New(0f, 0f, 0f, 0f, false, dart, true, this, false));
                            dart.burning = true;
                            dart.onFire = true;
                        }
                        Vec2 vec2 = Maths.AngleToVec(num);
                        dart.hSpeed = vec2.x * 50f;
                        dart.vSpeed = vec2.y * 20f;
                        Level.Add(dart);
                    }
                    this._wait = this._fireWait;
                    return;
                }
            }
            else if(this._wait == 0f)
            {
                base.DoAmmoClick();
            }
            ammo++;
        }

        // Token: 0x04001520 RID: 5408
        public StateBinding _burnLifeBinding = new StateBinding("_burnLife", -1, false, false);

        // Token: 0x04001521 RID: 5409
        private SpriteMap _sprite;

        // Token: 0x04001522 RID: 5410
        public float _burnLife = 1f;

        // Token: 0x04001523 RID: 5411
        public float _burnWait;

        // Token: 0x04001524 RID: 5412
        public bool burntOut;

        private int realammo;
    }
}
