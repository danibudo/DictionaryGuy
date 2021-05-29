using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DictionaryGuy_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Get the option for the word search
        private string getOption() {
            string result;
            if (btn_synonyms.IsChecked == true)
                result = "synonyms";
            else if (btn_rhymes.IsChecked == true)
                result = "rhymes";
            else if (btn_wordbegin.IsChecked == true)
                result = "wordsBegin";
            else if (btn_wordend.IsChecked == true)
                result = "wordsEnd";
            else
                result = "0";
            return result;
        }

        // Word Search (synonyms, rhymes, begin/end with)
        private async void SearchTypeOne(object sender, RoutedEventArgs e)
        {
            // Get data from form
            string txt = searchCriteria.Text.Trim();
            string option = getOption();
            string output = "Search type 1\nOption: " + option + "\nKeyword: " + txt;

            bool write = true;

            // Keyword validation
            if (!isValid(txt))
            {
                MessageBox.Show("Bad search criteria", "Search Error", MessageBoxButton.OK, MessageBoxImage.Error);
                write = false;
                return;
            }
            
            // Match the option to the desired search type
            switch(option)
            {
                case "synonyms":
                    output = await ApiHelper.SynonimSearch(txt);
                    output = JsonHelper.getSynonimsFromResponse(output);
                    break;
                case "rhymes":
                    output = await ApiHelper.RhymeSearch(txt);
                    if (output == "Sorry, can't help you with this one :(")
                        write = false;
                    output = JsonHelper.getWordsFromDatamuseResponse(output);
                    break;
                case "wordsBegin":
                    output = await ApiHelper.WordBeginSearch(txt);
                    output = JsonHelper.getWordsFromDatamuseResponse(output);
                    break;
                case "wordsEnd":
                    output = await ApiHelper.WordEndSearch(txt);
                    output = JsonHelper.getWordsFromDatamuseResponse(output);
                    break;
                default:
                    output = "No search type chosen";
                    write = false;
                    break;
            }
            // Bind output to form (result textbox)
            word_search.DataContext = output;
            if (output == "Sorry, can't help you with this one :(")
                write = false;

            if (write)
                FileInteraction.Write(option, txt, output);
        }

        // Sentence search
        private async void SearchTypeTwo(object sender, RoutedEventArgs e)
        {
            string txt = sentence_keyword.Text.Trim();
            string result = "Search type 2\nKeyword: " + txt;
            bool write = true;

            // Keyword validation
            if (!isValid(txt))
            {
                MessageBox.Show("Bad search criteria", "Search Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            result = await ApiHelper.SentenceSearch(txt);

            string output = JsonHelper.getSentencesFromResponse(result);

            // Bind output to form
            sentences.DataContext = output;

            if (output.Contains("Sorry, can't help you with this one :("))
                write = false;
            if(write)
                FileInteraction.Write("sentences", txt, output);
        }

        // Dictionary search
        private async void SearchTypeThree(object sender, RoutedEventArgs e)
        {
            string txt = dict_word.Text.Trim();
            string output = "Search type 3\nKeyword: " + txt;
            bool write = true;

            // Keyword validation
            if (!isValid(txt))
            {
                MessageBox.Show("Bad search criteria", "Search Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.WriteLine("Not good");
                return;
            }

            output = await ApiHelper.DefinitionSearch(txt);
            output = JsonHelper.getDefinitionFromResponse(output);

            // Bind output to form
            dictionary.DataContext = output;
            // Write file
            if (output == "Sorry, can't help you with this one :(")
                write = false;
            if (write)
                FileInteraction.Write("definition", txt, output);
        }

        // Methods that checks if a text contains whitespaces or if it is the empty string (it shouldn't, since the text should be a word)
        private bool isValid(string s)
        {
            if (s.Contains(" ") || s.Equals(""))
                return false;
            return true;
        }
    }
}
