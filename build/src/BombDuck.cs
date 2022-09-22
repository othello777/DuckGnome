using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.othello7_mod
{
    [EditorGroup("DuckGnome|Equip")]
    class BombDuck : Equipment
    {
        // Token: 0x060005ED RID: 1517 RVA: 0x00045F1C File Offset: 0x0004411C
        public BombDuck(float xpos, float ypos) : base(xpos, ypos)
        {
            this._sprite = new Sprite(this.GetPath("boomplate2.png"), 0f, 0f);
            this._spriteOver = new Sprite(this.GetPath("boomplate2.png"), 0f, 0f);
            this._pickupSprite = new Sprite(this.GetPath("boomplate2.png"), 0f, 0f);
            this._pickupSprite.CenterOrigin();
            this.graphic = new Sprite(this.GetPath("boomplate2.png"), 0f, 0f);
            this.collisionOffset = new Vec2(-6f, -4f);
            this.collisionSize = new Vec2(11f, 8f);
            this._equippedCollisionOffset = new Vec2(-7f, -5f);
            this._equippedCollisionSize = new Vec2(12f, 11f);
            this._hasEquippedCollision = true;
            this.center = new Vec2(8f, 8f);
            this.physicsMaterial = PhysicsMaterial.Metal;
            this._equippedDepth = 2;
            this._wearOffset = new Vec2(1f, 1f);
            this._isArmor = true;
            this._equippedThickness = 3f;
            this._editorName = "BombDuck";
        }

        /*public override void UnEquip()
        {
            SFX.Play("consoleError");
        }*/

        protected override bool OnDestroy(DestroyType type = null)
        {
            BlowUp();
            return base.OnDestroy(type);
        }

        // Token: 0x060005EE RID: 1518 RVA: 0x00046034 File Offset: 0x00044234
        public override void Update()
        {
            if (this._equippedDuck != null && base.duck == null)
            {
                return;
            }
            if (this._equippedDuck != null && !this.destroyed)
            {
                this.center = new Vec2(16f, 16f);
                this.solid = false;
                this._sprite.flipH = base.duck._sprite.flipH;
                this._spriteOver.flipH = base.duck._sprite.flipH;
                //this.graphic = this._sprite;
            }
            else
            {
                this.center = new Vec2((float)(this._pickupSprite.w / 2), (float)(this._pickupSprite.h / 2));
                this.solid = true;
                //this._sprite.frame = 0;
                this._sprite.flipH = false;
                this.graphic = this._pickupSprite;
            }
            if (this.destroyed)
            {
                base.alpha -= 0.05f;
            }
            if (base.alpha < 0f)
            {
                Level.Remove(this);
            }
            base.Update();
        }

        // Token: 0x060005EF RID: 1519 RVA: 0x00046148 File Offset: 0x00044348
        public override void Draw()
        {
            base.Draw();
            if (this._equippedDuck != null && base.duck == null)
            {
                return;
            }
            if (this._equippedDuck != null)
            {
                this._spriteOver.flipH = this.graphic.flipH;
                this._spriteOver.angle = this.angle;
                this._spriteOver.alpha = base.alpha;
                this._spriteOver.scale = base.scale;
                this._spriteOver.depth = this.owner.depth + ((base.duck.holdObject != null) ? 3 : 11);
                this._spriteOver.center = this.center;
                Graphics.Draw(this._spriteOver, base.x, base.y);
            }
        }

        public void BlowUp()
        {
            foreach (Profile profile66 in Profiles.all)
            {
                if (profile66 != null && profile66.duck != null)
                {
                    DevConsole.Log("" + profile66.rawName + " Terminated.", Color.Red);
                    profile66.duck.Kill(new DTShot(null));
                }
            }
            MakeBlowUpHappen(anchorPosition);
        }

        public void MakeBlowUpHappen(Vec2 pos)
        {
            SFX.Play("explode", 1f, 0f, 0f, false);
            Graphics.FlashScreen();
            float cx = pos.x;
            float cy = pos.y;
            Level.Add(new ExplosionPart(cx, cy, true));
            int num = 6;
            if (Graphics.effectsLevel < 2)
            {
                num = 3;
            }
            for (int i = 0; i < num; i++)
            {
                float dir = (float)i * 60f + Rando.Float(-10f, 10f);
                float dist = Rando.Float(12f, 20f);
                ExplosionPart ins = new ExplosionPart(cx + (float)(Math.Cos((double)Maths.DegToRad(dir)) * (double)dist), cy - (float)(Math.Sin((double)Maths.DegToRad(dir)) * (double)dist), true);
                Level.Add(ins);
            }
        }

        // Token: 0x04000486 RID: 1158
        private Sprite _sprite;

        // Token: 0x04000487 RID: 1159
        private Sprite _spriteOver;

        // Token: 0x04000488 RID: 1160
        private Sprite _pickupSprite;

        //public byte bulletFireIndex;

        public List<Bullet> firedBullets = new List<Bullet>();
    }
}
