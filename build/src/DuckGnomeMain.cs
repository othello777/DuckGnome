using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

namespace DuckGame.othello7_mod
{
    internal class DuckGnomeMain : IAutoUpdate
    {
        public DuckGnomeMain(Mod parent)
        {
            this._parentmod = parent;
            AutoUpdatables.Add(this);
            this.doneDucks1 = new List<Duck>();
            this.doneDucks2 = new List<Duck>();
            this.doneDucks3 = new List<Duck>();
        }

        public void Update()
        {

            if (Level.current != null)
            {
                if (Level.current.GetType().Name == "TitleScreen")
                {
                    foreach (Thing thing10 in Level.current.things[typeof(Thing)])
                    {
                        if (thing10.GetType().Name.ToLower() == "bigtitle")
                        {
                            thing10.graphic = new Sprite(Thing.GetPath<DuckGnome>("duckGnomeTitle.png"), 0.0f, 0.0f);
                            //Level.current.RemoveThing(thing10);
                        }
                    }
                    /*if(DuckGnome.activateConsole)
                    {
                        DuckGame.DevConsole.RunCommand = GnomeConsole.RunCommand;
                    }*/
                }
            }

            if (Level.current != this.lastlevel)
            {
                string lstring = Level.current.ToString();
                bool validLevel = true;
                foreach (string mstring in DuckGnomeMain.blacklist_levels)
                {
                    if (lstring == mstring)
                    {
                        validLevel = false;
                        break;
                    }
                }
                if (TeamSelect2.Enabled("SCPSTART", false) && validLevel)
                {
                    this.give_scp = true;
                    doneDucks1.Clear();
                }
                if (TeamSelect2.Enabled("NADESTART", false) && validLevel)
                {
                    this.give_nade = true;
                    doneDucks2.Clear();
                }
                if (TeamSelect2.Enabled("CHAOSMODE", false) && validLevel)
                {
                    this.chaosmode = true;
                    doneDucks2.Clear();
                }
                this.lastlevel = Level.current;
            }
            if (this.give_scp)
            {
                int dcount = 0;
                foreach (Thing thing in Level.current.things[typeof(Duck)])
                {
                    Duck d = (Duck)thing;
                    dcount++;
                    if (d.localSpawnVisible && !this.doneDucks1.Contains(d))
                    {
                        if (!d.isServerForObject || InputProfile.core._virtualProfiles.ContainsValue(d.inputProfile))
                        {
                            this.doneDucks1.Add(d);
                        }
                        else
                        {
                            SCP newscp = new SCP(0f, 0f);

                            if (newscp != null)
                            {
                                newscp.position = d.position;
                                Level.Add(newscp);
                                d.Equip(newscp, true, false);
                            }
                            this.doneDucks1.Add(d);
                        }
                    }
                }
                if (this.doneDucks1.Count == dcount && dcount > 0)
                {
                    this.give_scp = false;
                }
            }
            if (this.give_nade)
            {
                int dcount = 0;
                foreach (Thing thing in Level.current.things[typeof(Duck)])
                {
                    Duck d = (Duck)thing;
                    dcount++;
                    if (d.localSpawnVisible && !this.doneDucks2.Contains(d))
                    {
                        if (!d.isServerForObject || InputProfile.core._virtualProfiles.ContainsValue(d.inputProfile))
                        {
                            this.doneDucks2.Add(d);
                        }
                        else
                        {
                            Grenade newitem = new Grenade(0f, 0f);

                            if (newitem != null)
                            {
                                newitem.position = d.position;
                                Level.Add(newitem);
                                d.GiveHoldable(newitem);
                            }
                            this.doneDucks2.Add(d);
                        }
                    }
                }
                if (this.doneDucks2.Count == dcount && dcount > 0)
                {
                    this.give_nade = false;
                }
            }
            if (this.chaosmode)
            {
                int dcount = 0;
                foreach (Thing thing in Level.current.things[typeof(Duck)])
                {
                    Duck d = (Duck)thing;
                    dcount++;
                    if (d.localSpawnVisible && !this.doneDucks3.Contains(d))
                    {
                        if (!d.isServerForObject || InputProfile.core._virtualProfiles.ContainsValue(d.inputProfile))
                        {
                            this.doneDucks3.Add(d);
                        }
                        else
                        {
                            int number = 0;
                            while (number < 10)
                            {
                                DeathCrate crate = new DeathCrate(d.x + 30, (d.y - 1000) + (50 * number));
                                Level.Add(crate);
                                number++;
                            }
                            this.doneDucks3.Add(d);
                        }
                    }
                }
                if (this.doneDucks3.Count == dcount && dcount > 0)
                {
                    this.give_scp = false;
                }
            }
        }


        // Token: 0x04000053 RID: 83
        private static string[] blacklist_levels = new string[]
        {
            "DuckGame.TitleScreen",
            "DuckGame.HighlightLevel",
            "DuckGame.RockIntro",
            "DuckGame.TeamSelect2",
            "DuckGame.ArcadeLevel"
        };

        // Token: 0x04000054 RID: 84
        private Mod _parentmod;

        // Token: 0x04000055 RID: 85
        private Level lastlevel;

        // Token: 0x04000056 RID: 86
        private bool give_scp;

        private bool give_nade;

        private bool chaosmode;

        // Token: 0x04000057 RID: 87
        private List<Duck> doneDucks1;

        private List<Duck> doneDucks2;

        private List<Duck> doneDucks3;
    }
}
