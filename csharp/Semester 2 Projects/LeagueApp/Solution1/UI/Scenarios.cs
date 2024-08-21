using System;
using System.Collections.Generic;
using System.Text;
using Lib;
using Lib.Model;

namespace UI
{
    public static class Scenarios
    {
        static DbFunctions funcs = new DbFunctions();
        public static void SpelerAanpassenScenario()
        {
            var temp = new Speler("Tester McTest", 44,50000,4);
            funcs.VoegSpelerToe(temp);
            // Naam veranderen
            temp.Naam = "Assembly Tester";
            funcs.UpdateSpeler(temp);
            
        }

        public static void TransferScenario()
        {
            var temp = new Speler("Test Transfer", 1337, 55000, 4);
            funcs.VoegSpelerToe(temp);
            var tempTeam = new Team(87, "testTeam","cSharpIsBestSharp", "Compiler Jones");
            funcs.VoegTeamToe(tempTeam);
            Transfer transfer = new Transfer(temp.Id, tempTeam.Stamnummer, 3, 60000); //3 = Club Brugge
            funcs.VoegTransferToe(transfer);
        }


    }
}
