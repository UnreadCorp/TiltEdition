using System;
using System.Collections.Generic;

using System.Linq;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace TiltEdition
{
    // TREES DID EVERYTHING I JUST COPYPASTED HALF THE STUFF
    // TREES DID EVERYTHING I JUST COPYPASTED HALF THE STUFF
    // TREES DID EVERYTHING I JUST COPYPASTED HALF THE STUFF
    // TREES DID EVERYTHING I JUST COPYPASTED HALF THE STUFF
    internal class Program
    {
        public static Menu Menu;
        public static int Radius = 500;
        public static int LastPing;
        public static List<string> m_ourHomies = new List<string>();
        // TREES DID EVERYTHING I JUST COPYPASTED HALF THE STUFF
        // TREES DID EVERYTHING I JUST COPYPASTED HALF THE STUFF
        // TREES DID EVERYTHING I JUST COPYPASTED HALF THE STUFF
        // TREES DID EVERYTHING I JUST COPYPASTED HALF THE STUFF
        private static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
        }
        // TREES DID EVERYTHING I JUST COPYPASTED HALF THE STUFF
        // TREES DID EVERYTHING I JUST COPYPASTED HALF THE STUFF
        // TREES DID EVERYTHING I JUST COPYPASTED HALF THE STUFF
        // TREES DID EVERYTHING I JUST COPYPASTED HALF THE STUFF
        private static void Game_OnGameLoad(EventArgs args)
        {
            Menu = new Menu("TiltEdition", "TiltEdition", true);
            Menu.AddItem(new MenuItem("Ping", "Ping").SetValue(false));
            Menu.AddItem(new MenuItem("Mute", "NotWorking").SetValue(false));
            Menu.AddItem(new MenuItem("Frequence", "Ping per Ms").SetValue(new Slider(5000, 50, 5000)));

            Menu.AddItem(
             new MenuItem("Type", "Ping Type").SetValue(
                 new StringList(
                     new[]
                     {
                            PingCategory.Danger.ToString(), PingCategory.AssistMe.ToString(),
                            PingCategory.EnemyMissing.ToString(), PingCategory.Fallback.ToString(),
                            PingCategory.Normal.ToString(), PingCategory.OnMyWay.ToString()
                     })));
            Menu.AddItem(new MenuItem("Exploit", "Ping Exploit").SetValue(false));
            foreach (Obj_AI_Hero Hero in HeroManager.Allies)
            {
                if (Hero.ChampionName == ObjectManager.Player.ChampionName)
                    continue;

                Menu.AddItem(new MenuItem("Menu" + Hero.ChampionName, "Spam: " + Hero.ChampionName).SetValue(false));
            }
            Menu.AddToMainMenu();

            Game.OnUpdate += Game_OnGameUpdate;
            Game.OnChat += Game_OnChat;
        }
        // TREES DID EVERYTHING I JUST COPYPASTED HALF THE STUFF
        // TREES DID EVERYTHING I JUST COPYPASTED HALF THE STUFF
        // TREES DID EVERYTHING I JUST COPYPASTED HALF THE STUFF
        // TREES DID EVERYTHING I JUST COPYPASTED HALF THE STUFF
        private static void Game_OnChat(GameChatEventArgs args)
        {
            if (args.Message.StartsWith("You must wait"))
                args.Process = false;
        }
        // TREES DID EVERYTHING I JUST COPYPASTED HALF THE STUFF
        // TREES DID EVERYTHING I JUST COPYPASTED HALF THE STUFF
        // TREES DID EVERYTHING I JUST COPYPASTED HALF THE STUFF
        // TREES DID EVERYTHING I JUST COPYPASTED HALF THE STUFF
        private static void Game_OnGameUpdate(EventArgs args)
        {
           

            if (!Menu.Item("Ping").IsActive() || Utils.TickCount - LastPing < Menu.Item("Frequence").GetValue<Slider>().Value)
                return;

            var type =
                      (PingCategory)Enum.Parse(typeof(PingCategory), Menu.Item("Type").GetValue<StringList>().SelectedValue);
            type = Menu.Item("Exploit").IsActive() ? (PingCategory)1 : type;
            foreach (var hero in HeroManager.Allies)
            {
                if (hero.ChampionName == ObjectManager.Player.ChampionName)
                    continue;

                if (Menu.Item("Menu" + hero.ChampionName).IsActive())
                {
                    try
                    {
                       
                       
                        if (Menu.Item("Mute").IsActive())
                        {
                           // Game.OnPing += Game_OnPing;// Fuck you Kyon
                            Game.SendPing(type, hero);
                        //    Game.OnPing -= Game_OnPing;// Fuck you Kyon
                        }
                        else
                            Game.SendPing(type, hero);

                        LastPing = Utils.TickCount;
                        return;
                    }
                    catch
                    {
                        Game.PrintChat("Error Pinging :(");
                    }

                }

            }

        }

        private static void Game_OnPing(GamePingEventArgs args)
        {
            args.Process = false;
        }

    }
}

// TREES DID EVERYTHING I JUST COPYPASTED HALF THE STUFF
// TREES DID EVERYTHING I JUST COPYPASTED HALF THE STUFF
// TREES DID EVERYTHING I JUST COPYPASTED HALF THE STUFF
// TREES DID EVERYTHING I JUST COPYPASTED HALF THE STUFF