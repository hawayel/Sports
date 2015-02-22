using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PinnacleFeed.Engine.Models;
using System.Data.SqlClient;
using System.Data;
using PinnacleFeed.Engine.Infrastructure;

namespace PinnacleFeed.Engine.Database
{
    public class EventWriter
    {
        /// <summary>
        /// 
        /// </summary>
        private const string InsertEventSql = @"INSERT INTO [Match]([Id], [SportId], [LeagueId], [StartTime], [HomeTeam], [AwayTeam]) 
VALUES(@E_Id{0}, @E_SportId{0}, @E_LeagueId{0}, @E_StartTime{0}, @E_HomeTeam{0}, @E_AwayTeam{0})";

        /// <summary>
        /// 
        /// </summary>
        private const string InsertSpreadSql = @"INSERT INTO [Spread]([MatchId],[SportId],[LeagueId],[HomeSpread],[AwaySpread],[HomePrice], [AwayPrice],[IsAlt])
VALUES(@S_MatchId{0}{1}, @S_SportId{0}{1}, @S_LeagueId{0}{1}, @S_HomeSpread{0}{1}, @S_AwaySpread{0}{1}, @S_HomePrice{0}{1}, @S_AwayPrice{0}{1}, @S_IsAlt{0}{1})";

        /// <summary>
        /// 
        /// </summary>
        private const string InserTotalSql = @"INSERT INTO [Total]([MatchId],[SportId],[LeagueId],[Points],[OverPrice], [UnderPrice],[IsAlt])
VALUES(@T_MatchId{0}{1}, @T_SportId{0}{1}, @T_LeagueId{0}{1}, @T_Points{0}{1}, @T_OverPrice{0}{1}, @T_UnderPrice{0}{1}, @T_IsAlt{0}{1})";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="events"></param>
        public void Write(Event[] events)
        { 
            var sb = new StringBuilder();

            using (var cn = new SqlConnection(ConfigManager.GetConnectionString())) 
            {
                using (var cmd = new SqlCommand())
                {

                    for (int i=0; i < events.Length; i++)
                    {
                        sb.AppendLine(string.Format(InsertEventSql, i));

                        cmd.Parameters.AddWithValue("E_Id"          + i, events[i].Id);
                        cmd.Parameters.AddWithValue("E_SportId"     + i, events[i].SportId);
                        cmd.Parameters.AddWithValue("E_LeagueId"    + i, events[i].LeagueId);
                        cmd.Parameters.AddWithValue("E_StartTime"   + i, events[i].StartTime);
                        cmd.Parameters.AddWithValue("E_HomeTeam"    + i, events[i].HomeTeam);
                        cmd.Parameters.AddWithValue("E_AwayTeam"    + i, events[i].AwayTeam);

                        for (int j=0; j < events[i].Spreads.Length; j++)
                        {
                            sb.AppendLine(string.Format(InsertSpreadSql, i, j));

                            cmd.Parameters.AddWithValue("S_MatchId"     + i + j, events[i].Spreads[j].EventId);
                            cmd.Parameters.AddWithValue("S_SportId"     + i + j, events[i].Spreads[j].SportId);
                            cmd.Parameters.AddWithValue("S_LeagueId"    + i + j, events[i].Spreads[j].LeagueId);
                            cmd.Parameters.AddWithValue("S_HomeSpread"  + i + j, events[i].Spreads[j].HomeSpread);
                            cmd.Parameters.AddWithValue("S_AwaySpread"  + i + j, events[i].Spreads[j].AwaySpread);
                            cmd.Parameters.AddWithValue("S_HomePrice"   + i + j, events[i].Spreads[j].HomePrice);
                            cmd.Parameters.AddWithValue("S_AwayPrice"   + i + j, events[i].Spreads[j].AwayPrice);
                            cmd.Parameters.AddWithValue("S_IsAlt"       + i + j, events[i].Spreads[j].IsAlt);
                        }

                        for (int k=0; k < events[i].Totals.Length; k++)
                        {
                            sb.AppendLine(string.Format(InserTotalSql, i, k));

                            cmd.Parameters.AddWithValue("T_MatchId"     + i + k, events[i].Totals[k].EventId);
                            cmd.Parameters.AddWithValue("T_SportId"     + i + k, events[i].Totals[k].SportId);
                            cmd.Parameters.AddWithValue("T_LeagueId"    + i + k, events[i].Totals[k].LeagueId);
                            cmd.Parameters.AddWithValue("T_Points"      + i + k, events[i].Totals[k].Points);
                            cmd.Parameters.AddWithValue("T_OverPrice"   + i + k, events[i].Totals[k].OverPrice);
                            cmd.Parameters.AddWithValue("T_UnderPrice"  + i + k, events[i].Totals[k].UnderPrice);
                            cmd.Parameters.AddWithValue("T_IsAlt"       + i + k, events[i].Totals[k].IsAlt);
                        }

                        sb.AppendLine();
                    }//events

                    cmd.CommandText = sb.ToString();
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection  = cn;

                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }
    }
}

