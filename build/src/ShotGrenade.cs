using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

namespace othello7_mod.src
{
    [EditorGroup("DuckGnome")]
    public class ShotGrenade : TampingWeapon
    {
        // Token: 0x06000250 RID: 592 RVA: 0x0001D740 File Offset: 0x0001B940
        public ShotGrenade(float xval, float yval) : base(xval, yval)
        {
            this.ammo = 3;
            this._type = "gun";
            this.graphic = new Sprite(this.GetPath("shotgunade.png"), 0f, 0f);
            this.center = new Vec2(17f, 13f);
            this.collisionOffset = new Vec2(-8f, -3f);
            this.collisionSize = new Vec2(16f, 8f);
            this._barrelOffsetTL = new Vec2(3f, 0f);
            this._kickForce = 5f;
            this._holdOffset = new Vec2(2f, -2f);
            this.loseAccuracy = 0f;
            this.maxAccuracyLost = 0f;
            this._bio = "BEWMMMMMMMM";
            this._editorName = "Grenade Shotgun";
            this.physicsMaterial = PhysicsMaterial.Metal;
            base.bouncy = 0.4f;
            this.weight = 3f;
        }

        // Token: 0x06000251 RID: 593 RVA: 0x000025AC File Offset: 0x000007AC
        public override void Fire()
        {
        }

        // Token: 0x06000252 RID: 594 RVA: 0x0001D888 File Offset: 0x0001BA88
        public override void OnPressAction()
        {
            bool flag = this.ammo > 0;
            if (flag)
            {
                bool isServerForObject = base.isServerForObject;
                if (isServerForObject)
                {
                    if (true)
                    {
                        for (int index = 0; index < 5; index++)
                        {
                            
                            Grenade grenade = new Grenade(0f, 0f);
                            grenade.position = this.Offset(this._barrelOffsetTL);
                            grenade.vSpeed = (base.barrelVector.Rotate((index * 40) - 40, barrelPosition)).y * 12f;
                            grenade.hSpeed = (base.barrelVector.Rotate((index * 40) - 40, barrelPosition)).x * 12f;
                            grenade._pin = false;
                            grenade._timer = 0.1f;
                            bool flag3 = base.duck != null;
                            if (flag3)
                            {
                                grenade.responsibleProfile = base.duck.profile;
                            }
                            Level.Add(grenade);
                        }
                    }
                    /*else
                    {
                        Grenade grenade2 = new Grenade(0f, 0f);
                        grenade2.position = this.Offset(this._barrelOffsetTL);
                        grenade2.vSpeed = base.barrelVector.y * 12f - 1f;
                        grenade2.hSpeed = base.barrelVector.x * 12f;
                        grenade2._pin = false;
                        grenade2._timer = 0.1f;
                        bool flag4 = base.duck != null;
                        if (flag4)
                        {
                            grenade2.responsibleProfile = base.duck.profile;
                        }
                        Level.Add(grenade2);
                    }*/
                }
                base.OnPressAction();
                base.ApplyKick();
                SFX.Play("deepMachineGun2", 1f, 0f, 0f, false);
                this.ammo--;
            }
        }

        // Token: 0x040002AD RID: 685
        public float vSpeedSaver;

        // Token: 0x040002AE RID: 686
        public float hSpeedSaver;

        //private SpriteMap _sprite;
    }
}

