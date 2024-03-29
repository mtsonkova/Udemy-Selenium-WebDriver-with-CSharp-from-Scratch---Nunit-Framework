﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemySeleniumFrameworkWithCSharp.utilities
{
    public class JSONReader
    {
        public JSONReader() { }

        public string extractData(string tokenName, string filePath)
        {
            string myJsonString = File.ReadAllText(filePath);
            var jsonObject = JToken.Parse(myJsonString);

            string? output =  jsonObject.SelectToken(tokenName).Value<string>();
            return output;
        }

        public String[] extractDataArray(string tokenName, string filePath)
        {
            string myJsonString = File.ReadAllText(filePath);
            var jsonObject = JToken.Parse(myJsonString);

            var output = jsonObject.SelectTokens(tokenName).Values<string>().ToList();
                
            return output.ToArray();
        }
    }
}
