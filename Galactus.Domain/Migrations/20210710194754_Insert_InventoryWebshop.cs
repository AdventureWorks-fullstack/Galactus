using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Galactus.Domain.Migrations
{
    public partial class Insert_InventoryWebshop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            foreach (var value in GetValues())
            {
                string sql = $"INSERT INTO Production.Inventory (InventoryName, LocationId) VALUES " + value;

                migrationBuilder.Sql(sql);
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string sql = $"DELETE FROM Production.Inventory WHERE LocationId = {locationId}";

            migrationBuilder.Sql(sql);
        }

        static int locationId = 61;
        static string[] prefixes = new string[]
        { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        static int[][] shelves = new int[][]
        {
            new int[]{1, 12, 12, 12, 1},
            new int[]{1, 12, 12, 12, 1},
            new int[]{1, 4, 4, 4, 1},
            new int[]{1, 4, 4, 4, 1},
            new int[]{1, 1, 1, 1, 1},
            new int[]{1, 1, 1, 1, 1},
        };

        public static List<string> GetValues()
        {
            var result = new List<string>();
            var valuesList = new List<string>();
            for (int nbShelves = 0; nbShelves < prefixes.Length; nbShelves++)
            {
                for (int nbColumns = 0; nbColumns < shelves.Length; nbColumns++)
                {
                    for (int nbRows = 0; nbRows < shelves[nbColumns].Length; nbRows++)
                    {
                        for (int nbBins = 0; nbBins < shelves[nbColumns][nbRows]; nbBins++)
                        {
                            // A-01-01-01
                            string value =
                                $"{prefixes[nbShelves]}-" +
                                $"{(nbColumns + 1).ToString("00")}-" +
                                $"{(nbRows + 1).ToString("00")}-" +
                                $"{(nbBins + 1).ToString("00")}";


                            valuesList.Add($"('{value}', {locationId})");
                        }
                    }
                }
            }

            // there is a limit of how many rows you can insert at once so chunk the list
            var chunks = SplitList(valuesList, 100);
            foreach (var chunk in chunks)
            {
                result.Add(string.Join(",", chunk));
            }

            return result;
        }
        // https://stackoverflow.com/a/11463800
        static IEnumerable<List<T>> SplitList<T>(List<T> locations, int nSize = 30)
        {
            for (int i = 0; i < locations.Count; i += nSize)
            {
                yield return locations.GetRange(i, Math.Min(nSize, locations.Count - i));
            }
        }
    }
}
