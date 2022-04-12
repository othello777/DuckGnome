namespace DuckGame.othello7_mod
{
    public class ATSilentMag : AmmoType
    {
        public bool angleShot = true;

        public ATSilentMag()
        {
            this.accuracy = 0.95f;
            this.range = 250f;
            this.penetration = 1f;
            this.bulletSpeed = 40f;
            this.bulletThickness = 0.3f;
            this.bulletType = typeof(SilentMagBullet);
            this.combustable = true;
        }
    }
}