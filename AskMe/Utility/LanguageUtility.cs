using System;
using System.Collections.Generic;
using System.Text;

namespace AskMe.Utility
{
    public static class LanguageUtility
    {
        const string Penske = "Penske";
        const string AskMe = "AskMe";
        const string Ryder = "Ryder";

        private static Dictionary<string, string> lookupTable = new Dictionary<string, string>();

        public static void GenerateLanguageModel()
        {
            //Penske
            lookupTable.Add("pensky", Penske);
            lookupTable.Add("penski", Penske);
            lookupTable.Add("spinski",Penske);
            lookupTable.Add("bensky", Penske);
            lookupTable.Add("pinski", Penske);
            lookupTable.Add("pinske", Penske);
            lookupTable.Add("pinsky", Penske);

            //AskMe
            lookupTable.Add("inginiri", AskMe);
            lookupTable.Add("generi", AskMe);
            lookupTable.Add("engineering", AskMe);

            //Ryder
            lookupTable.Add("rider", Ryder);
        }

        public static string GetProperLanguageContent(string text)
        {
            var s = text.TrimEnd(new char[] { '?', '.' });
            var textArray = s.Split(' ');
            foreach(var t in textArray)
            {
                var lower = t.ToLower();
                if(lookupTable.ContainsKey(lower))
                    text = text.Replace(t, lookupTable[lower]);
            }
            return text;
        }
    }
}
