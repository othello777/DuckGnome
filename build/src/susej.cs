using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

namespace othello7_mod.src
{
    [EditorGroup("DuckGnome")]
    public class susej : Holdable, IPlatform
    {
        // Token: 0x060017A2 RID: 6050 RVA: 0x000E2F8C File Offset: 0x000E118C
        public susej(float xpos, float ypos) : base(xpos, ypos)
        {
            this._maxHealth = 15f;
            this._hitPoints = 15f;
            this._sprite = new SpriteMap(this.GetPath("susejanim.png"), 16, 16, false);
            this.graphic = this._sprite;
            this.center = new Vec2(8f, 8f);
            this.collisionOffset = new Vec2(-8f, -5f);
            this.collisionSize = new Vec2(16f, 12f);
            base.depth = -0.5f;
            this.thickness = 2f;
            this.weight = 5f;
            this.buoyancy = 1f;
            this.flammable = 0.3f;
            base.collideSounds.Add("rockHitGround2");
            this.physicsMaterial = PhysicsMaterial.Metal;
            this._editorName = "Jesus Rock";
        }

        public override void OnPressAction()
        {
            SFX.Play((GetPath("SFX\\susejchoir.wav")), 20f, 0.0f, 0.0f, false);
            Profile profile = DevConsole.ProfileByName("player" + Rando.Int(1, 4));
            bool loop = true;
            while(loop)
            {
                profile = DevConsole.ProfileByName("player" + Rando.Int(1, 4));
                if(profile != null)
                {
                    if (profile.duck != null)
                    {
                        if (!profile.duck.dead)
                        {
                            loop = false;
                            DevConsole.Log("SUSEJ at " + profile.name, Color.Blue, 2, -1);
                        }
                    }
                }
            }
            this.duck.ThrowItem();
            this.x = profile.duck.x;
            this.y = profile.duck.y - 25f;
            this.velocity = new Vec2(0, -1f);
            profile.duck.Kill(new DTCrush(this));
            base.OnPressAction();
        }

        // Token: 0x060017A3 RID: 6051 RVA: 0x000E304C File Offset: 0x000E124C
        public override bool Hit(Bullet bullet, Vec2 hitPos)
        {
            if (bullet.isLocal && TeamSelect2.Enabled("EXPLODEYCRATES", false))
            {
                if (base.duck != null)
                {
                    base.duck.ThrowItem(true);
                }
                this.Destroy(new DTShot(bullet));
                Level.Remove(this);
                Level.Add(new GrenadeExplosion(base.x, base.y));
            }
            return base.Hit(bullet, hitPos);
        }

        protected override bool OnDestroy(DestroyType type = null)
        {
            if(!dead)
                SFX.Play((GetPath("SFX\\SUSEJ01.wav")), 20f, 0.0f, 0.0f, false);

            dead = true;
            Level.Remove(this);
            return base.OnDestroy(type);
        }

        // Token: 0x04001541 RID: 5441
        private SpriteMap _sprite;
        private bool dead = false;
    }
}
