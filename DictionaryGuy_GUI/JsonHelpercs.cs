using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace DictionaryGuy_GUI
{
    class SentenceResponse
    {
        public List<string> example { get; set; }
    }

    class SynonimResponse
    {
        public List<string> assoc_word { get; set; }
    }

    class WordEntry
    {
        public string word { get; set; }
        int score { get; set; }
    }

    class Meaning
    {
        public string noun { get; set; }
        public string verb { get; set; }
        public string adverb { get; set; }
        public string adjective { get; set; }
    } 

    class DefinitionResponse
    {
        public Meaning meaning { get; set; }
    } 

    class JsonHelper
    {
        public static string getSentencesFromResponse(string response)
        {
            SentenceResponse deserializedResponse = JsonConvert.DeserializeObject<SentenceResponse>(response);
            response = "";

            if (deserializedResponse.example == null)
            {
                List<string> list = new List<string>();
                list.Add("Sorry, can't help you with this one :(");
                deserializedResponse.example = list;
            }

            foreach (string item in deserializedResponse.example)
            {
                response += item + "\n";
            }
            return response;
        }

        public static string getSynonimsFromResponse(string response)
        {
            SynonimResponse deserializedResponse = JsonConvert.DeserializeObject<SynonimResponse>(response);
            response = "";

            if (deserializedResponse.assoc_word == null)
            {
                List<string> list = new List<string>();
                list.Add("Sorry, can't help you with this one :(");
                deserializedResponse.assoc_word = list;
            }

            foreach (string item in deserializedResponse.assoc_word)
            {
                response += item + "\n";
            }
            return response;
        }

        public static string getWordsFromDatamuseResponse(string response)
        {
            string result = "";
            List<WordEntry> rhymes = JsonConvert.DeserializeObject<List<WordEntry>>(response);
            if(rhymes.Count == 0)
            {
                return "Sorry, can't help you with this one :(";
            }
            foreach (var item in rhymes)
            {
                result += item.word + "\n";
            }
            return result;
        }

        public static string getDefinitionFromResponse(string response)
        {
            DefinitionResponse deserializedResponse = JsonConvert.DeserializeObject<DefinitionResponse>(response);
            Meaning meaning = deserializedResponse.meaning;

            string result = "";

            if(meaning == null)
                return "Sorry, can't help you with this one :(";

            if (meaning.adjective != null && meaning.adjective != "")
                result += meaning.adjective + '\n';
            if (meaning.adverb != null && meaning.adverb != "")
                result += meaning.adverb + '\n';
            if (meaning.noun != null && meaning.noun != "")
                result += meaning.noun + '\n';
            if (meaning.verb != null && meaning.verb != "")
                result += meaning.verb;
            if(result == "")
                result = "Sorry, can't help you with this one :(";
            return result;
        }
    }
}
