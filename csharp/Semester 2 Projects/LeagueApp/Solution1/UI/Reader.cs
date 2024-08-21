using Lib.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Lib;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace UI
{
    public static class Reader
    {
        public static char Seperator = ','; // Global Seperator
        static DbFunctions funcs = new DbFunctions(); //Global Call,
        public static void Import()
        {
            Dictionary<int, Team> teams = new Dictionary<int, Team>();
            var csvReader = new StreamReader(File.OpenRead(@"E:\School\repos\Programmeren4\LeagueApp\Solution1\Bestanden\foot.csv"));
            csvReader.ReadLine(); // header
            while (csvReader.Peek() >= 0)
            {
  
                string temp = csvReader.ReadLine();
                
                var line = temp.Split(Seperator);
                var spelerNaam = line[0];
                var rugNummer = int.Parse(line[1]);
                var clubNaam = line[2];
                var waarde = int.Parse(line[3].Replace(" ", "")); ;
                var stamNummer = int.Parse(line[4]);
                var Trainer = line[5];
                var bijNaam = line[6];

                var speler = new Speler(spelerNaam.Trim(), rugNummer, waarde, stamNummer);
                if (teams.ContainsKey(stamNummer))
                {
                    teams[stamNummer].Spelers.Add(speler);
                }
                else
                {
                    var team = new Team(stamNummer, clubNaam, bijNaam, Trainer);
                    team.Spelers.Add(speler);
                    teams.TryAdd(team.Stamnummer, team);
                }


            }

            foreach (var team in teams.Values)
            {
              funcs.VoegTeamToe(team);
              foreach (var speler in team.Spelers)
              {
                  funcs.VoegSpelerToe(speler);
              }
            }

        }



    }
}
