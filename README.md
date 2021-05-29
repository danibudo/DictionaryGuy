# DictionaryGuy

## Functionality 

The project consists of a desktop app that interacts with 2 APIs: Twinword Word Dictionary (https://www.twinword.com/api/word-dictionary.php) and Datamuse (http://www.datamuse.com/api/).  
The UI consists of 3 separate tabs, each handling a different kind of search.  
- Word search:  
The user is able to look for:  
    - Synonyms of a given word
    - Words that rhyme with a given word (rhymes)
    - Words that begin with a set of letters
    - Words that end with a set of letters
- Sentence search:  
The user is able to search sentences that contain a given word.  
- Definition search:  
The user is able to search the (dictionary) definition of a given word.

## Structure & Flow
Each tab (corresponding to a search type) contains a search button. The function attached to that button checks if the keyword is valid (meaning that there is some text, and there is only one word in there).  

If the keyword is valid, a function from the _APIHelper_ class is being called, and the keyword is passed as a parameter. The _APIHelper_ method sends the request to the API and passes the response to the _JSONHelper_. \

The method from _JSONHelper_ parses the JSON file (that comes as a response from the API) and sends the response (in string format) back to the initial function (the one attached to the _Search_ button).  

If the string received from the JSON parser is not null, the text is printed into the _Result_ textbox on the UI. Otherwise, an informative message is printed to the user.

Also, if the API sends a non-empty response, the response is saved as a text file (path to textfile: *DictionaryGuy\DictionaryGuy_GUI\bin\Debug*). The name of the text file is suggestive: the search type + the keyword + timestamp (example: _sentences_poke_202105170014442925.txt_ stands for a file that contains sentences with the word poke, and the timestamp at which the search was done).
