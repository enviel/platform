﻿using System.Drawing;
using System.Linq;
using GTA;
using NativeUI;

namespace GTANetwork
{
    public class DebugWindow
    {
        public bool Visible { get; set; }
        public int PlayerIndex { get; set; }

        public void Draw()
        {
            if (!Visible) return;

            if (Game.IsControlJustPressed(0, Control.FrontendLeft))
            {
                PlayerIndex--;
                UI.ShowSubtitle("NewIndex: " + PlayerIndex);
            }

            else if (Game.IsControlJustPressed(0, Control.FrontendRight))
            {
                PlayerIndex++;
                UI.ShowSubtitle("NewIndex: " + PlayerIndex);
            }

            if (PlayerIndex >= Main.NetEntityHandler.ClientMap.Count(item => item is SyncPed) || PlayerIndex < 0)
            {
                // wrong index
                return;
            }

            var player = Main.NetEntityHandler.ClientMap.Where(item => item is SyncPed).Cast<SyncPed>().ElementAt(PlayerIndex);
            string output = "=======PLAYER #" + PlayerIndex + " INFO=======\n";
            output += "Name: " + player.Name + "\n";
            output += "IsInVehicle: " + player.IsInVehicle + "\n";
            output += "Position: " + player.Position + "\n";
            output += "VehiclePosition: " + player.VehiclePosition + "\n";
            output += "Last Updated: " + player.LastUpdateReceived + "\n";
            output += "Character Pos: " + player.Character?.Position + "\n";
            output += "BlipPos: " + player.Character?.CurrentBlip?.Position + "\n";
            output += "Ragdoll: " + player.IsRagdoll + "\n";
            if (player.MainVehicle != null)
            {
                output += "CharacterIsInVeh: " + player.Character?.IsInVehicle() + "\n";
                output += "ActualCarPos: " + player.MainVehicle?.Position + "\n";
                output += "SeatIndex: " + player.VehicleSeat + "\n";
            }
            
            new UIResText(output, new Point(500, 10), 0.5f) {Outline = true}.Draw();
        }
    }
}