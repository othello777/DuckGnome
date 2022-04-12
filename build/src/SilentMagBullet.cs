using Microsoft.Xna.Framework.Graphics;

namespace DuckGame.othello7_mod
{
    public class SilentMagBullet : Bullet
    {
        private Texture2D _beem;
        private float _thickness;

        public SilentMagBullet(float xval, float yval, AmmoType type, float ang = -1f, Thing owner = null, bool rbound = false, float distance = -1f, bool tracer = false, bool network = false)
          : base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
        {
            this._thickness = type.bulletThickness;
            this._beem = Content.Load<Texture2D>("magBeam");
        }

        public override void Draw()
        {
            if (this._tracer || (double)this._bulletDistance <= 0.100000001490116)
                return;
            float length = (this.drawStart - this.drawEnd).length;
            float val = 0.0f;
            float num1 = (float)(1.0 / ((double)length / 8.0));
            float num2 = 0.0f;
            float num3 = 8f;
            while (true)
            {
                bool flag = false;
                if ((double)val + (double)num3 > (double)length)
                {
                    num3 = length - Maths.Clamp(val, 0.0f, 99f);
                    flag = true;
                }
                num2 += num1;
                DuckGame.Graphics.DrawTexturedLine((Tex2D)this._beem, this.drawStart + this.travelDirNormalized * val, this.drawStart + this.travelDirNormalized * (val + num3), Color.White * num2, this._thickness, (Depth)0.6f);
                if (!flag)
                    val += 8f;
                else
                    break;
            }
        }

        protected override void OnHit(bool destroyed)
        {
            if (!destroyed)
                return;
            ExplosionPart explosionPart = new ExplosionPart(this.x, this.y, true);
            explosionPart.xscale *= 0.7f;
            explosionPart.yscale *= 0.7f;
            Level.Add((Thing)explosionPart);
            SFX.Play("", 0.7f, Rando.Float(-0.5f, -0.3f), 0.0f, false);
            Thing owner = this.owner;
            foreach (MaterialThing materialThing in Level.CheckCircleAll<MaterialThing>(this.position, 14f))
            {
                if (materialThing != owner)
                    materialThing.Destroy((DestroyType)new DTShot((Bullet)this));
            }
        }

        protected override void Rebound(Vec2 pos, float dir, float rng)
        {
            SilentMagBullet magBullet = new SilentMagBullet(pos.x, pos.y, this.ammo, dir, (Thing)null, this.rebound, rng, false, false);
            magBullet._teleporter = this._teleporter;
            magBullet.firedFrom = this.firedFrom;
            Level.current.AddThing((Thing)magBullet);
            Level.current.AddThing((Thing)new LaserRebound(pos.x, pos.y));
        }
    }
}