using CsvHelper;
using CsvHelper.Configuration;
using Galactic.Data.Interfaces;
using Galactic.Data.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Galactic.Data.Processing
{
    public class CsvDataFetcher : ITokenDataFetching
    {
        private string FolderPath;
        private string tokens_File = "Tokens.csv";
        private static object LogFileLock = new object();

        public CsvDataFetcher()
        {
            //string baseDir = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\..\"));
            //FolderPath = Path.Combine(baseDir, "BagageSortering/Data/Database/Csv/");
            FolderPath = "Data/Storage/";
        }

        public List<TokenDatabaseData> LoadTokenData()
        {
            try
            {
                using (var reader = new StreamReader(Path.Combine(FolderPath, tokens_File)))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<TokenDatabaseData>();

                    return Enumerable.ToList(records);
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public TokenDatabaseData GetSingleToken(string personName)
        {
            List<TokenDatabaseData> allTokens = LoadTokenData();

            return allTokens.Find(x => x.Name == personName);
        }

        public bool AddTokenData(TokenDatabaseData data)
        {
            try
            {
                lock (LogFileLock)
                {
                    List<string> fileData = File.ReadAllLines(Path.Combine(FolderPath, tokens_File)).ToList();
                    fileData.Add(data.CreateCsvFileLine());
                    WriteAllToFile(fileData);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }

        private bool WriteAllToFile(List<string> fileData) 
        {
            try
            {
                //Uses StreamWriter to write data to file. 
                using (StreamWriter writer = new StreamWriter(Path.Combine(FolderPath, tokens_File), false))
                {

                    foreach (string s in fileData)
                    {
                        writer.WriteLine(s);

                    }
                    writer.Flush();
                    writer.Close();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool UpdateTokenData(TokenDatabaseData data)
        {
            List<TokenDatabaseData> tokens = LoadTokenData();

            TokenDatabaseData found = tokens.Find(x => x.Name == data.Name);

            if(found is null)
            {
                return false;
            }
            else
            {
                found.TokenHash = data.TokenHash;
                found.ExpirationDate = data.ExpirationDate;

                List<string> allLines = new List<string>();

                allLines.Add(found.CreateCsvHeaderLine());

                foreach(TokenDatabaseData d in tokens)
                {
                    allLines.Add(d.CreateCsvFileLine());
                }

                return WriteAllToFile(allLines);
            }

            return true;
        }

        //public bool AddTokenData(TokenDatabaseData line)
        //{
        //    try
        //    {
        //        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        //        {
        //            // Don't write the header again.
        //            HasHeaderRecord = false,
        //        };

        //        using (var stream = File.Open(Path.Combine(FolderPath, tokens_File), FileMode.Append))
        //        using (var writer = new StreamWriter(stream))
        //        using (var csv = new CsvWriter(writer, config))
        //        {
        //            csv.WriteRecord(line);
        //        }

        //        return true;
        //    }
        //    catch (Exception ex) 
        //    {
        //        return false;
        //    }

        //}





        //public Dictionary<string, int> LoadTerminalGatesData()
        //{
        //    try
        //    {
        //        using (var reader = new StreamReader(Path.Combine(FolderPath, TerminalGates_FN)))
        //        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        //        {
        //            var records = csv.GetRecords<AirportTerminalGates>();
        //            Dictionary<string, int> returnDict = new Dictionary<string, int>();

        //            foreach (AirportTerminalGates couple in records)
        //            {
        //                returnDict.Add(couple.GateNr, couple.Terminal);
        //            }

        //            return returnDict;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLogger log = new ErrorLogger();
        //        log.LogError(ex);
        //        return null;
        //    }

        //}

    }
}