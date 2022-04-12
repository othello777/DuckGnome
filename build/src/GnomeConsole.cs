/*using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using DuckGame;

namespace DuckGame.othello7_mod
{
    public class GnomeConsole
    {
        public static bool fancyMode = false;
        private static DevConsoleCore _core = new DevConsoleCore();
        private static bool _enableNetworkDebugging = false;
        public static bool fuckUpPacketOrder = false;
        public static List<DCLine> debuggerLines = new List<DCLine>();
        private static bool _oldConsole;
        public static Sprite _tray;
        public static Sprite _scan;

        public static DevConsoleCore core
        {
            get
            {
                return GnomeConsole._core;
            }
            set
            {
                GnomeConsole._core = value;
            }
        }

        public static bool open
        {
            get
            {
                return GnomeConsole._core.open;
            }
        }

        public static void SuppressDevConsole()
        {
            GnomeConsole._oldConsole = GnomeConsole._enableNetworkDebugging;
            GnomeConsole._enableNetworkDebugging = false;
        }

        public static void RestoreDevConsole()
        {
            GnomeConsole._enableNetworkDebugging = GnomeConsole._oldConsole;
        }

        public static bool enableNetworkDebugging
        {
            get
            {
                return GnomeConsole._enableNetworkDebugging;
            }
            set
            {
                GnomeConsole._enableNetworkDebugging = value;
            }
        }

        public static bool splitScreen
        {
            get
            {
                return GnomeConsole._core.splitScreen;
            }
            set
            {
                GnomeConsole._core.splitScreen = value;
            }
        }

        public static bool rhythmMode
        {
            get
            {
                return GnomeConsole._core.rhythmMode;
            }
            set
            {
                GnomeConsole._core.rhythmMode = value;
            }
        }

        public static bool qwopMode
        {
            get
            {
                return GnomeConsole._core.qwopMode;
            }
            set
            {
                GnomeConsole._core.qwopMode = value;
            }
        }

        public static bool showIslands
        {
            get
            {
                return GnomeConsole._core.showIslands;
            }
            set
            {
                GnomeConsole._core.showIslands = value;
            }
        }

        public static bool showCollision
        {
            get
            {
                return GnomeConsole._core.showCollision;
            }
            set
            {
                GnomeConsole._core.showCollision = value;
            }
        }

        public static bool shieldMode
        {
            get
            {
                return GnomeConsole._core.shieldMode;
            }
            set
            {
                GnomeConsole._core.shieldMode = value;
            }
        }

        public static void Draw()
        {
            if (GnomeConsole._core.font == null)
            {
                GnomeConsole._core.font = new BitmapFont("biosFont", 8, -1);
                GnomeConsole._core.font.scale = new Vec2(2f, 2f);
                GnomeConsole._core.fancyFont = new FancyBitmapFont("smallFont");
                GnomeConsole._core.fancyFont.scale = new Vec2(2f, 2f);
            }
            if ((double)GnomeConsole._core.alpha <= 0.00999999977648258)
                return;
            float num1 = 256f;
            int width = Graphics.width;
            GnomeConsole._core.font.alpha = GnomeConsole._core.alpha;
            GnomeConsole._core.font.Draw(GnomeConsole._core.typing, 16f, num1 + 20f, Color.White, (Depth)0.9f, (InputProfile)null, false);
            int index1 = GnomeConsole._core.lines.Count - 1;
            float num2 = 0.0f;
            for (int index2 = 0; (double)index2 < (double)num1 / 18.0 && index1 >= 0; ++index2)
            {
                DCLine dcLine = GnomeConsole._core.lines.ElementAt<DCLine>(index1);
                if (!NetworkDebugger.enabled || dcLine.threadIndex == NetworkDebugger.networkDrawingIndex)
                {
                    GnomeConsole._core.font.scale = new Vec2(dcLine.scale);
                    GnomeConsole._core.font.Draw(dcLine.SectionString() + dcLine.line, 16f, num1 - 20f - num2, dcLine.color * 0.8f, (Depth)0.9f, (InputProfile)null, false);
                    num2 += (float)(18.0 * ((double)dcLine.scale * 0.5));
                    GnomeConsole._core.font.scale = new Vec2(2f);
                }
                --index1;
            }
            if (GnomeConsole._tray == null)
                return;
            GnomeConsole._tray.alpha = GnomeConsole._core.alpha;
            GnomeConsole._tray.scale = new Vec2(4f, 4f);
            GnomeConsole._tray.depth = (Depth)0.75f;
            GnomeConsole._scan.alpha = GnomeConsole._core.alpha;
            GnomeConsole._scan.scale = new Vec2(2f, 2f);
            GnomeConsole._scan.depth = (Depth)0.95f;
            Graphics.Draw(GnomeConsole._scan, 0.0f, 0.0f);
            GnomeConsole._core.fancyFont.depth = (Depth)0.98f;
            GnomeConsole._core.fancyFont.alpha = GnomeConsole._core.alpha;
        }

        public static Profile ProfileByName(string findName)
        {
            foreach (Profile profile in Profiles.all)
            {
                if (profile.team != null)
                {
                    string str = profile.name.ToLower();
                    if (findName == "player1" && (profile.persona == Persona.Duck1 || profile.duck != null && profile.duck.persona == Persona.Duck1))
                        str = findName;
                    else if (findName == "player2" && (profile.persona == Persona.Duck2 || profile.duck != null && profile.duck.persona == Persona.Duck2))
                        str = findName;
                    else if (findName == "player3" && (profile.persona == Persona.Duck3 || profile.duck != null && profile.duck.persona == Persona.Duck3))
                        str = findName;
                    else if (findName == "player4" && (profile.persona == Persona.Duck4 || profile.duck != null && profile.duck.persona == Persona.Duck4))
                        str = findName;
                    if (str == findName)
                        return profile;
                }
            }
            return (Profile)null;
        }


        public static void RunCommand(string command)
        {
            if (!(command != ""))
                return;
            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            command = command.ToLower(currentCulture);
            bool flag1 = false;
            ConsoleCommand consoleCommand = new ConsoleCommand(command);
            string str1 = consoleCommand.NextWord();
            if (str1 == "spawn")
            {
                flag1 = true;
                string text4 = consoleCommand.NextWord();
                float x = 0f;
                float y = 0f;
                try
                {
                    x = Change.ToSingle(consoleCommand.NextWord());
                    y = Change.ToSingle(consoleCommand.NextWord());
                }
                catch
                {
                    GnomeConsole._core.lines.Enqueue(new DCLine
                    {
                        line = "Parameters in wrong format.",
                        color = Color.Red
                    });
                    return;
                }
                if (text4 == "gun")
                {
                    GnomeConsole._core.lines.Enqueue(new DCLine
                    {
                        scale = 1.2f,
                        line = text4 + "THATS A BAD IDEA, it = CRASH.",
                        color = Color.Red
                    });
                    return;
                }
                string a = consoleCommand.NextWord();
                if (a != "i" && a != "")
                {
                    GnomeConsole._core.lines.Enqueue(new DCLine
                    {
                        line = "Too many parameters!",
                        color = Color.Red
                    });
                    return;
                }
                if (consoleCommand.NextWord() != "")
                {
                    GnomeConsole._core.lines.Enqueue(new DCLine
                    {
                        line = "Too many parameters!",
                        color = Color.Red
                    });
                    return;
                }
                Type type = null;
                foreach (Type type2 in Editor.ThingTypes)
                {
                    if (type2.Name.ToLower(currentCulture) == text4)
                    {
                        type = type2;
                        break;
                    }
                }
                if (type == null)
                {
                    GnomeConsole._core.lines.Enqueue(new DCLine
                    {
                        line = "The type " + text4 + " does not exist!",
                        color = Color.Red
                    });
                    return;
                }
                if (!Editor.HasConstructorParameter(type))
                {
                    GnomeConsole._core.lines.Enqueue(new DCLine
                    {
                        line = text4 + " can not be spawned this way.",
                        color = Color.Red
                    });
                    return;
                }
                Thing thing = Editor.CreateThing(type) as PhysicsObject;
                if (thing != null)
                {
                    thing.x = x;
                    thing.y = y;
                    if (a == "i" && thing is Gun)
                    {
                        (thing as Gun).infinite = true;
                    }
                    Level.Add(thing);
                    SFX.Play("hitBox", 1f, 0f, 0f, false);
                }
            }
            if (str1 == "set")
            {
                if (Network.isActive || Level.current is ChallengeLevel)
                {
                    GnomeConsole._core.lines.Enqueue(new DCLine()
                    {
                        line = "You can't do that here!",
                        color = Color.Red
                    });
                    return;
                }
                flag1 = true;
                string str2 = consoleCommand.NextWord();
                bool flag2 = false;
                foreach (Profile profile in Profiles.all)
                {
                    if (profile.name.ToLower(currentCulture) == str2)
                    {
                        if (profile.duck != null)
                        {
                            flag2 = true;
                            string str3 = consoleCommand.NextWord();
                            if (str3 == "")
                            {
                                GnomeConsole._core.lines.Enqueue(new DCLine()
                                {
                                    line = "Parameters in wrong format.",
                                    color = Color.Red
                                });
                                return;
                            }
                            System.Type type = typeof(Duck);
                            PropertyInfo[] properties = type.GetProperties();
                            bool flag3 = false;
                            foreach (PropertyInfo propertyInfo in properties)
                            {
                                if (propertyInfo.Name.ToLower(currentCulture) == str3)
                                {
                                    flag3 = true;
                                    if (propertyInfo.PropertyType == typeof(float))
                                    {
                                        float single;
                                        try
                                        {
                                            single = Change.ToSingle((object)consoleCommand.NextWord());
                                        }
                                        catch
                                        {
                                            GnomeConsole._core.lines.Enqueue(new DCLine()
                                            {
                                                line = "Parameters in wrong format.",
                                                color = Color.Red
                                            });
                                            return;
                                        }
                                        if (consoleCommand.NextWord() != "")
                                        {
                                            GnomeConsole._core.lines.Enqueue(new DCLine()
                                            {
                                                line = "Too many parameters!",
                                                color = Color.Red
                                            });
                                            return;
                                        }
                                        propertyInfo.SetValue((object)profile.duck, (object)single, (object[])null);
                                    }
                                    if (propertyInfo.PropertyType == typeof(bool))
                                    {
                                        bool boolean;
                                        try
                                        {
                                            boolean = Convert.ToBoolean(consoleCommand.NextWord());
                                        }
                                        catch
                                        {
                                            GnomeConsole._core.lines.Enqueue(new DCLine()
                                            {
                                                line = "Parameters in wrong format.",
                                                color = Color.Red
                                            });
                                            return;
                                        }
                                        if (consoleCommand.NextWord() != "")
                                        {
                                            GnomeConsole._core.lines.Enqueue(new DCLine()
                                            {
                                                line = "Too many parameters!",
                                                color = Color.Red
                                            });
                                            return;
                                        }
                                        propertyInfo.SetValue((object)profile.duck, (object)boolean, (object[])null);
                                    }
                                    if (propertyInfo.PropertyType == typeof(int))
                                    {
                                        int int32;
                                        try
                                        {
                                            int32 = Convert.ToInt32(consoleCommand.NextWord());
                                        }
                                        catch
                                        {
                                            GnomeConsole._core.lines.Enqueue(new DCLine()
                                            {
                                                line = "Parameters in wrong format.",
                                                color = Color.Red
                                            });
                                            return;
                                        }
                                        if (consoleCommand.NextWord() != "")
                                        {
                                            GnomeConsole._core.lines.Enqueue(new DCLine()
                                            {
                                                line = "Too many parameters!",
                                                color = Color.Red
                                            });
                                            return;
                                        }
                                        propertyInfo.SetValue((object)profile.duck, (object)int32, (object[])null);
                                    }
                                    if (propertyInfo.PropertyType == typeof(Vec2))
                                    {
                                        float single1;
                                        float single2;
                                        try
                                        {
                                            single1 = Change.ToSingle((object)consoleCommand.NextWord());
                                            single2 = Change.ToSingle((object)consoleCommand.NextWord());
                                        }
                                        catch
                                        {
                                            GnomeConsole._core.lines.Enqueue(new DCLine()
                                            {
                                                line = "Parameters in wrong format.",
                                                color = Color.Red
                                            });
                                            return;
                                        }
                                        if (consoleCommand.NextWord() != "")
                                        {
                                            GnomeConsole._core.lines.Enqueue(new DCLine()
                                            {
                                                line = "Too many parameters!",
                                                color = Color.Red
                                            });
                                            return;
                                        }
                                        propertyInfo.SetValue((object)profile.duck, (object)new Vec2(single1, single2), (object[])null);
                                    }
                                }
                            }
                            if (!flag3)
                            {
                                foreach (FieldInfo field in type.GetFields())
                                {
                                    if (field.Name.ToLower(currentCulture) == str3)
                                    {
                                        flag3 = true;
                                        if (field.FieldType == typeof(float))
                                        {
                                            float single;
                                            try
                                            {
                                                single = Change.ToSingle((object)consoleCommand.NextWord());
                                            }
                                            catch
                                            {
                                                GnomeConsole._core.lines.Enqueue(new DCLine()
                                                {
                                                    line = "Parameters in wrong format.",
                                                    color = Color.Red
                                                });
                                                return;
                                            }
                                            if (consoleCommand.NextWord() != "")
                                            {
                                                GnomeConsole._core.lines.Enqueue(new DCLine()
                                                {
                                                    line = "Too many parameters!",
                                                    color = Color.Red
                                                });
                                                return;
                                            }
                                            field.SetValue((object)profile.duck, (object)single);
                                        }
                                        if (field.FieldType == typeof(bool))
                                        {
                                            bool boolean;
                                            try
                                            {
                                                boolean = Convert.ToBoolean(consoleCommand.NextWord());
                                            }
                                            catch
                                            {
                                                GnomeConsole._core.lines.Enqueue(new DCLine()
                                                {
                                                    line = "Parameters in wrong format.",
                                                    color = Color.Red
                                                });
                                                return;
                                            }
                                            if (consoleCommand.NextWord() != "")
                                            {
                                                GnomeConsole._core.lines.Enqueue(new DCLine()
                                                {
                                                    line = "Too many parameters!",
                                                    color = Color.Red
                                                });
                                                return;
                                            }
                                            field.SetValue((object)profile.duck, (object)boolean);
                                        }
                                        if (field.FieldType == typeof(int))
                                        {
                                            int int32;
                                            try
                                            {
                                                int32 = Convert.ToInt32(consoleCommand.NextWord());
                                            }
                                            catch
                                            {
                                                GnomeConsole._core.lines.Enqueue(new DCLine()
                                                {
                                                    line = "Parameters in wrong format.",
                                                    color = Color.Red
                                                });
                                                return;
                                            }
                                            if (consoleCommand.NextWord() != "")
                                            {
                                                GnomeConsole._core.lines.Enqueue(new DCLine()
                                                {
                                                    line = "Too many parameters!",
                                                    color = Color.Red
                                                });
                                                return;
                                            }
                                            field.SetValue((object)profile.duck, (object)int32);
                                        }
                                        if (field.FieldType == typeof(Vec2))
                                        {
                                            float single1;
                                            float single2;
                                            try
                                            {
                                                single1 = Change.ToSingle((object)consoleCommand.NextWord());
                                                single2 = Change.ToSingle((object)consoleCommand.NextWord());
                                            }
                                            catch
                                            {
                                                GnomeConsole._core.lines.Enqueue(new DCLine()
                                                {
                                                    line = "Parameters in wrong format.",
                                                    color = Color.Red
                                                });
                                                return;
                                            }
                                            if (consoleCommand.NextWord() != "")
                                            {
                                                GnomeConsole._core.lines.Enqueue(new DCLine()
                                                {
                                                    line = "Too many parameters!",
                                                    color = Color.Red
                                                });
                                                return;
                                            }
                                            field.SetValue((object)profile.duck, (object)new Vec2(single1, single2));
                                        }
                                    }
                                }
                                if (!flag3)
                                {
                                    GnomeConsole._core.lines.Enqueue(new DCLine()
                                    {
                                        line = "Duck has no variable called " + str3 + ".",
                                        color = Color.Red
                                    });
                                    return;
                                }
                            }
                        }
                        else
                        {
                            GnomeConsole._core.lines.Enqueue(new DCLine()
                            {
                                line = str2 + " is not in the game!",
                                color = Color.Red
                            });
                            return;
                        }
                    }
                }
                if (!flag2)
                {
                    GnomeConsole._core.lines.Enqueue(new DCLine()
                    {
                        line = "No profile named " + str2 + ".",
                        color = Color.Red
                    });
                    return;
                }
            }
            if (str1 == "kill")
            {
                if (Network.isActive || Level.current is ChallengeLevel)
                {
                    GnomeConsole._core.lines.Enqueue(new DCLine()
                    {
                        line = "You can't do that here!",
                        color = Color.Red
                    });
                    return;
                }
                flag1 = true;
                string str2 = consoleCommand.NextWord();
                if (consoleCommand.NextWord() != "")
                {
                    GnomeConsole._core.lines.Enqueue(new DCLine()
                    {
                        line = "Too many parameters!",
                        color = Color.Red
                    });
                    return;
                }
                bool flag2 = false;
                foreach (Profile profile in Profiles.all)
                {
                    if (profile.name.ToLower(currentCulture) == str2)
                    {
                        if (profile.duck != null)
                        {
                            profile.duck.Kill((DestroyType)new DTIncinerate((Thing)null));
                            flag2 = true;
                        }
                        else
                        {
                            GnomeConsole._core.lines.Enqueue(new DCLine()
                            {
                                line = str2 + " is not in the game!",
                                color = Color.Red
                            });
                            return;
                        }
                    }
                }
                if (!flag2)
                {
                    GnomeConsole._core.lines.Enqueue(new DCLine()
                    {
                        line = "No profile named " + str2 + ".",
                        color = Color.Red
                    });
                    return;
                }
            }
            if (str1 == "fancymode")
            {
                if (Network.isActive || Level.current is ChallengeLevel)
                {
                    GnomeConsole._core.lines.Enqueue(new DCLine()
                    {
                        line = "You can't do that here!",
                        color = Color.Red
                    });
                    return;
                }
                GnomeConsole.fancyMode = !GnomeConsole.fancyMode;
                flag1 = true;
            }
            if (str1 == "spawnall")
            {
                flag1 = true;
                string text13 = consoleCommand.NextWord();
                string a3 = consoleCommand.NextWord();
                string a4 = consoleCommand.NextWord();
                Profile profile17 = DevConsole.ProfileByName(text13);
                if (profile17 == null)
                {
                    GnomeConsole._core.lines.Enqueue(new DCLine
                    {
                        line = "No profile named " + text13 + ".",
                        color = Color.Red
                    });
                    return;
                }
                if (profile17.duck == null)
                {
                    GnomeConsole._core.lines.Enqueue(new DCLine
                    {
                        line = text13 + " is not in the game!",
                        color = Color.Red
                    });
                    return;
                }
                if (a4 != "i" && a4 != "")
                {
                    GnomeConsole._core.lines.Enqueue(new DCLine
                    {
                        line = "Too many parameters!",
                        color = Color.Red
                    });
                    return;
                }
                if (consoleCommand.NextWord() != "")
                {
                    GnomeConsole._core.lines.Enqueue(new DCLine
                    {
                        line = "Too many parameters!",
                        color = Color.Red
                    });
                    return;
                }
                if (a3 == "gun")
                {
                    foreach (Type type3 in Editor.ThingTypes)
                    {
                        if (!type3.IsAbstract && type3.IsSubclassOf(typeof(PhysicsObject)) && type3.GetCustomAttributes(typeof(EditorGroupAttribute), false).Length != 0 && type3 != null)
                        {
                            Thing thing3 = Editor.CreateThing(type3) as PhysicsObject;
                            if (thing3 != null && thing3 is Gun)
                            {
                                thing3.x = profile17.duck.x;
                                thing3.y = profile17.duck.y;
                                if (a4 == "i" && thing3 is Gun)
                                {
                                    (thing3 as Gun).infinite = true;
                                }
                                Level.Add(thing3);
                                SFX.Play("hitBox", 1f, 0f, 0f, false);
                            }
                        }
                    }
                }
                if (a3 == "h")
                {
                    foreach (Type type4 in Editor.GetSubclasses(typeof(Holdable)).ToList<Type>())
                    {
                        if (!type4.IsAbstract && type4.IsSubclassOf(typeof(PhysicsObject)) && type4 != null && !(type4.Name.ToLower(currentCulture) == "tampingweapon"))
                        {
                            Thing thing4 = Editor.CreateThing(type4) as PhysicsObject;
                            if (thing4 != null)
                            {
                                thing4.x = profile17.duck.x;
                                thing4.y = profile17.duck.y;
                                if (a4 == "i" && thing4 is Gun)
                                {
                                    (thing4 as Gun).infinite = true;
                                }
                                Level.Add(thing4);
                                SFX.Play("hitBox", 1f, 0f, 0f, false);
                            }
                        }
                    }
                }
                if (a3 == "hw")
                {
                    foreach (Type type5 in Editor.GetSubclasses(typeof(Holdable)).ToList<Type>())
                    {
                        if (!type5.IsAbstract && type5.IsSubclassOf(typeof(PhysicsObject)) && type5 != null && !(type5.Name.ToLower(currentCulture) == "tampingweapon") && type5.GetCustomAttributes(typeof(EditorGroupAttribute), false).Length == 0 && !(type5.Name.ToLower(currentCulture) == "portalgun"))
                        {
                            Thing thing5 = Editor.CreateThing(type5) as PhysicsObject;
                            if (thing5 != null)
                            {
                                thing5.x = profile17.duck.x;
                                thing5.y = profile17.duck.y;
                                if (a4 == "i" && thing5 is Gun)
                                {
                                    (thing5 as Gun).infinite = true;
                                }
                                Level.Add(thing5);
                                SFX.Play("hitBox", 1f, 0f, 0f, false);
                            }
                        }
                    }
                }
                if (a3 == "all")
                {
                    foreach (Type type6 in Editor.ThingTypes)
                    {
                        if (!type6.IsAbstract && type6.IsSubclassOf(typeof(PhysicsObject)) && type6.GetCustomAttributes(typeof(EditorGroupAttribute), false).Length != 0 && type6 != null)
                        {
                            Thing thing6 = Editor.CreateThing(type6) as PhysicsObject;
                            if (thing6 != null)
                            {
                                thing6.x = profile17.duck.x;
                                thing6.y = profile17.duck.y;
                                if (a4 == "i" && thing6 is Gun)
                                {
                                    (thing6 as Gun).infinite = true;
                                }
                                Level.Add(thing6);
                                SFX.Play("hitBox", 1f, 0f, 0f, false);
                            }
                        }
                    }
                }
                if (a3 == "nogun")
                {
                    foreach (Type type7 in Editor.ThingTypes)
                    {
                        if (!type7.IsAbstract && type7.IsSubclassOf(typeof(PhysicsObject)) && type7.GetCustomAttributes(typeof(EditorGroupAttribute), false).Length != 0 && type7 != null)
                        {
                            Thing thing7 = Editor.CreateThing(type7) as PhysicsObject;
                            if (thing7 != null && !(thing7 is Gun) && !(thing7.editorName.ToLower() == "drumset"))
                            {
                                thing7.x = profile17.duck.x;
                                thing7.y = profile17.duck.y;
                                Level.Add(thing7);
                                SFX.Play("hitBox", 1f, 0f, 0f, false);
                            }
                        }
                    }
                }
            }
            if (str1 == "spawnat")
            {
                flag1 = true;
                string text27 = consoleCommand.NextWord();
                string text28 = consoleCommand.NextWord();
                string a7 = consoleCommand.NextWord();
                float bulletSpeed = 0f;
                Profile profile34 = DevConsole.ProfileByName(text27);
                bool flag4;
                try
                {
                    bulletSpeed = Change.ToSingle(consoleCommand.NextWord());
                    flag4 = true;
                }
                catch
                {
                    flag4 = false;
                }
                if (a7 != "i" && a7 != "")
                {
                    GnomeConsole._core.lines.Enqueue(new DCLine
                    {
                        line = "Too many parameters!",
                        color = Color.Red
                    });
                    return;
                }
                if (consoleCommand.NextWord() != "")
                {
                    GnomeConsole._core.lines.Enqueue(new DCLine
                    {
                        line = "Too many parameters!",
                        color = Color.Red
                    });
                    return;
                }
                Type type10 = null;
                foreach (Type type11 in Editor.ThingTypes)
                {
                    if (type11.Name.ToLower(currentCulture) == text28)
                    {
                        type10 = type11;
                        break;
                    }
                }
                if (type10 == null)
                {
                    GnomeConsole._core.lines.Enqueue(new DCLine
                    {
                        line = "The type " + text28 + " does not exist!",
                        color = Color.Red
                    });
                    return;
                }
                if (!Editor.HasConstructorParameter(type10))
                {
                    GnomeConsole._core.lines.Enqueue(new DCLine
                    {
                        line = text28 + " can not be spawned this way.",
                        color = Color.Red
                    });
                    return;
                }
                if (text28 == "gun")
                {
                    GnomeConsole._core.lines.Enqueue(new DCLine
                    {
                        line = text28 + "... THATS A BAD IDEA, it = CRASH.",
                        color = Color.Red
                    });
                    return;
                }
                if (text27 == "all")
                {
                    foreach (Profile profile35 in Profiles.all)
                    {
                        if (profile35.duck != null)
                        {
                           Thing thing13 = Editor.CreateThing(type10) as PhysicsObject;
                            if (thing13 != null)
                            {
                                thing13.x = profile34.duck.x;
                                thing13.y = profile34.duck.y;
                                if (a7 == "i" && thing13 is Gun)
                                {
                                    (thing13 as Gun).infinite = true;
                                }
                                /*
                                if (a7 == "im" && thing13 is Gun)
                                {
                                    (thing13 as Gun)._ammoType = new ATMissile();
                                    (thing13 as Gun).infinite = true;
                                }
                                if (a7 == "ic" && thing13 is Gun)
                                {
                                    (thing13 as Gun)._ammoType = new ATMag();
                                    (thing13 as Gun).infinite = true;
                                }
                                if (flag4)
                                {
                                    (thing13 as Gun)._ammoType.bulletSpeed = bulletSpeed;
                                }*
                                Level.Add(thing13);
                                SFX.Play("hitBox", 1f, 0f, 0f, false);
                                GnomeConsole._core.lines.Enqueue(new DCLine
                                {
                                    line = string.Concat(new string[]
                                    {
                                            thing13.editorName,
                                            " Spawned at ",
                                            profile35.rawName,
                                            " X ",
                                            thing13.x.ToString(),
                                            " Y ",
                                            thing13.y.ToString()
                                    }),
                                    color = Color.Green
                                });
                            }
                        }
                    }
                    return;
                }
                if (profile34 == null)
                {
                    GnomeConsole._core.lines.Enqueue(new DCLine
                    {
                        line = "No profile named " + text27 + ".",
                        color = Color.Red
                    });
                    return;
                }
                if (profile34.duck == null)
                {
                    GnomeConsole._core.lines.Enqueue(new DCLine
                    {
                        line = text27 + " is not in the game!",
                        color = Color.Red
                    });
                    return;
                }
                Thing thing14 = Editor.CreateThing(type10) as PhysicsObject;
                if (thing14 != null)
                {
                    thing14.x = profile34.duck.x;
                    thing14.y = profile34.duck.y;
                    if (a7 == "i" && thing14 is Gun)
                    {
                        (thing14 as Gun).infinite = true;
                    }/*
                    if (a7 == "im" && thing14 is Gun)
                    {
                        (thing14 as Gun).infinite = true;
                        (thing14 as Gun)._ammoType = new ATMissile();
                    }
                    if (a7 == "ic" && thing14 is Gun)
                    {
                        (thing14 as Gun)._ammoType = new ATMag();
                        (thing14 as Gun).infinite = true;
                    }
                    if (flag4)
                    {
                        (thing14 as Gun)._ammoType.bulletSpeed = bulletSpeed;
                    }*
                    Level.Add(thing14);
                    SFX.Play("hitBox", 1f, 0f, 0f, false);
                    GnomeConsole._core.lines.Enqueue(new DCLine
                    {
                        line = string.Concat(new string[]
                        {
                                thing14.editorName,
                                " Spawned at ",
                                profile34.rawName,
                                " X ",
                                thing14.x.ToString(),
                                " Y ",
                                thing14.y.ToString()
                        }),
                        color = Color.Green
                    });
                }
            }


        }
    }
}*/
            
        
    

