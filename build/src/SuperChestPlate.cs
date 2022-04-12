using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

namespace DuckGame.othello7_mod
{
    [EditorGroup("DuckGnome|Equip")]
    public class SCP : Equipment
    {
        private SpriteMap _sprite;
        private SpriteMap _spriteOver;
        private Sprite _pickupSprite;
        private Duck DuckEquipCurent;

        public SCP(float xpos, float ypos)
          : base(xpos, ypos)
        {
            this._sprite = new SpriteMap("chestPlateAnim", 32, 32, false);
            this._spriteOver = new SpriteMap(this.GetPath("ironoremantwo.png"), 32, 32, false);
            this._pickupSprite = new Sprite(/*"chestPlatePickup"*/"", 0.0f, 0.0f);
            this._pickupSprite.CenterOrigin();
            this.graphic = this._pickupSprite;
            this.collisionOffset = new Vec2(-6f, -4f);
            this.collisionSize = new Vec2(11f, 19f);
            this._equippedCollisionOffset = new Vec2(-7f, -5f);
            this._equippedCollisionSize = new Vec2(12f, 19f);
            this._hasEquippedCollision = true;
            this.center = new Vec2(8f, 8f);
            this.physicsMaterial = PhysicsMaterial.Metal;
            this._equippedDepth = 2;
            this._wearOffset = new Vec2(1f, 1f);
            this._isArmor = true;
            this._equippedThickness = 3f;
            this._editorName = "Super ChestPlate";
        }


        public override void Equip(Duck d)
        {
            DuckEquipCurent = d;
            d.Unequip(d.hat);
            base.Equip(d);
            d.alpha = 0;
            //d._sprite = new SpriteMap(this.GetPath("ironoremantwo.png"), 32, 32, false); 
        }

        public override void UnEquip()
        {
            SFX.Play("ting2");
            /* if (DuckEquipCurent != null)
             {
                 DuckEquipCurent.Equip(new SuperChestPlate(equippedDuck.x, equippedDuck.y));
             }
             DuckEquipCurent = null;*/

            //base.UnEquip();
        }

        protected override bool OnDestroy(DestroyType type = null)
        {
            return false;
        }

        public override void Update()
        {
            if (this._equippedDuck != null && this.duck == null)
                return;
            if (this._equippedDuck != null && !this.destroyed)
            {
                this._spriteOver.frame = this._equippedDuck.spriteImageIndex;

                this.center = new Vec2(17f, 22f);
                //x, y  0 is bottom right 
                // first number sideways second number updown less is down and forward
                this.solid = false;
                this._sprite.flipH = this.duck._sprite.flipH;
                this._spriteOver.flipH = this.duck._sprite.flipH;
                this.graphic = (Sprite)this._sprite;
            }
            else
            {
                this.center = new Vec2((float)(this._pickupSprite.w / 2), (float)(this._pickupSprite.h / 2));
                this.solid = true;
                this._sprite.frame = 0;
                this._sprite.flipH = false;
                this.graphic = this._pickupSprite;
            }

            if (this.destroyed)
            {
                this.alpha -= 0.05f;
                if (DuckEquipCurent != null)
                {
                    DuckEquipCurent.Equip(new SCP(equippedDuck.x, equippedDuck.y));
                }

                //Equipment plate = new SuperChestPlate(duck.x, duck.y);
                //Level.Add(plate);
                //Type typeFromHandle = typeof(SuperChestPlate);
                //Equipment thing2 = Editor.CreateThing(typeFromHandle) as Equipment;

                //this.duck.Equip(plate, false, false);
            }
            if ((double)this.alpha < 0.0)
            {
                Level.Remove((Thing)this);
            }

            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
            if (this._equippedDuck != null && this.duck == null || this._equippedDuck == null)
                return;
            this._spriteOver.flipH = this.graphic.flipH;
            this._spriteOver.angle = this.angle;
            this._spriteOver.alpha = this.alpha;
            this._spriteOver.scale = this.scale;
            this._spriteOver.depth = this.owner.depth + (this.duck.holdObject != null ? 3 : 11);
            this._spriteOver.center = this.center;
            Graphics.Draw((Sprite)this._spriteOver, this.x, this.y);
        }
    }
}
