using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using System.IO;
//using Harmony;


// The title of your mod, as displayed in menus
[assembly: AssemblyTitle("DuckGnome")]

// The author of the mod
[assembly: AssemblyCompany("othello7")]

// The description of the mod
[assembly: AssemblyDescription("Adds a few guns (a little trolling)")]

// The mod's version
[assembly: AssemblyVersion("1.1.7.1")]

namespace DuckGame.othello7_mod
{


    public class DuckGnome : Mod
    {

        //public static bool activateConsole = true;
        //public static HarmonyInstance harmony = HarmonyInstance.Create("othello7_mod.DuckGame");
        /*
        internal static void PerformMethodInjection()
        {
            
            try
            {
                string[] EnableOnline = System.IO.File.ReadAllLines(@"C:\Users\kieferz\Documents\DuckGame\Mods\othello7's_mod\EnableDevConsoleOnline.txt");
                if (EnableOnline[0] != "true")
                {
                    activateConsole = false;
                }
            }
            catch(System.IO.FileNotFoundException)
            {

            }
            catch(System.IO.DirectoryNotFoundException)
            {

            }
            catch(System.NullReferenceException)
            {

            }

            if (activateConsole == true)
            {/*
                try
                {
                    var original = typeof(DevConsole).GetMethod("RunCommand");
                    var prefix = typeof(GnomeConsole).GetMethod("RunCommand");
                    var harmonymethodprefix = new HarmonyMethod(prefix);

                    harmony.Patch(original, harmonymethodprefix);

                    DevConsole.Log("Granted command permissions", Color.Green);

                }
                catch (System.NullReferenceException)
                {
                    DevConsole.Log("Failed to grant command permissions", Color.Red);
                    MessageBox.Show("System.NullReferenceException at HaxorEnabler");
                }*
                DevConsole.Log("Command permissions feature not added yet. skipping.", Color.Yellow);
            }
        }*/

        // The mod's priority; this property controls the load order of the mod.
        public override Priority priority
        {
            get { return base.priority; }
        }

        // This function is run before all mods are finished loading.
        protected override void OnPreInitialize()
        {
            MonoMain.loadMessage = "Loading DuckGnome";
            Program.LogLine(string.Format("-- DuckGnome initialized on {1} at {0} --", (object)DateTime.Now.ToLongTimeString(), (object)DateTime.Now.ToShortDateString()));
            Program.main.Window.Title = "DuckGnome";

            base.OnPreInitialize();
        }

        // This function is run after all mods are loaded.
        protected override void OnPostInitialize()
        {
            base.OnPostInitialize();
            Thread thread = new Thread(new ThreadStart(this.ExecuteOnceLoaded));
            thread.Start();
        }

        //bimmod stuff
        private void ExecuteOnceLoaded()
        {
            while (Level.current == null || !(Level.current.ToString() == "DuckGame.TitleScreen") && !(Level.current.ToString() == "DuckGame.TeamSelect2"))
                Thread.Sleep(200);
            UnlockData StartWithSCP = new UnlockData()
            {
                name = "Start with SCP",
                id = "SCPSTART",
                type = UnlockType.Modifier,
                cost = 0,
                description = "Starts all duck with a SuperChestPlate",
                longDescription = "...",
                icon = 3,
                priceTier = UnlockPrice.Cheap
            };
            ((List<UnlockData>)typeof(Unlocks).GetField("_allUnlocks", BindingFlags.Static | BindingFlags.NonPublic).GetValue((object)null)).Add(StartWithSCP);
            Unlocks.modifierToByte[StartWithSCP.id] = (byte)Unlocks.allUnlocks.Count;
            foreach (Profile universalProfile in Profiles.universalProfileList)
            {
                if (!Unlocks.IsUnlocked("SCPSTART", universalProfile))
                    universalProfile.unlocks.Add("SCPSTART");
            }
            UnlockData StartWithGrenade = new UnlockData()
            {
                name = "Start with 'Nade",
                id = "NADESTART",
                type = UnlockType.Modifier,
                cost = 0,
                description = "Starts all duck with a Grenade",
                longDescription = "...",
                icon = 3,
                priceTier = UnlockPrice.Cheap
            };
            ((List<UnlockData>)typeof(Unlocks).GetField("_allUnlocks", BindingFlags.Static | BindingFlags.NonPublic).GetValue((object)null)).Add(StartWithGrenade);
            Unlocks.modifierToByte[StartWithGrenade.id] = (byte)Unlocks.allUnlocks.Count;
            foreach (Profile universalProfile in Profiles.universalProfileList)
            {
                if (!Unlocks.IsUnlocked("NADESTART", universalProfile))
                    universalProfile.unlocks.Add("NADESTART");
            }
            UnlockData ChaosMode = new UnlockData()
            {
                name = "CHAOS MODE! (warning)",
                id = "CHAOSMODE",
                type = UnlockType.Modifier,
                cost = 0,
                description = "AAAAAAAAAAAAAA",
                longDescription = "...",
                icon = 3,
                priceTier = UnlockPrice.Cheap
            };
            ((List<UnlockData>)typeof(Unlocks).GetField("_allUnlocks", BindingFlags.Static | BindingFlags.NonPublic).GetValue((object)null)).Add(ChaosMode);
            Unlocks.modifierToByte[ChaosMode.id] = (byte)Unlocks.allUnlocks.Count;
            foreach (Profile universalProfile in Profiles.universalProfileList)
            {
                if (!Unlocks.IsUnlocked("CHAOSMODE", universalProfile))
                    universalProfile.unlocks.Add("CHAOSMODE");
            }
            UnlockData ImpostorMode = new UnlockData()
            {
                name = "Sus weapon switching",
                id = "IMPOSTORMODE",
                type = UnlockType.Modifier,
                cost = 0,
                description = "random guns are switched out with DGn weapons",
                longDescription = "...",
                icon = 3,
                priceTier = UnlockPrice.Cheap
            };
            ((List<UnlockData>)typeof(Unlocks).GetField("_allUnlocks", BindingFlags.Static | BindingFlags.NonPublic).GetValue((object)null)).Add(ImpostorMode);
            Unlocks.modifierToByte[ImpostorMode.id] = (byte)Unlocks.allUnlocks.Count;
            foreach (Profile universalProfile in Profiles.universalProfileList)
            {
                if (!Unlocks.IsUnlocked("IMPOSTORMODE", universalProfile))
                    universalProfile.unlocks.Add("IMPOSTORMODE");
            }
            DuckGnome.mm = new DuckGnomeMain((Mod)this);
        }


        internal static DuckGnomeMain mm;
    }
}
