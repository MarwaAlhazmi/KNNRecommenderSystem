using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Text.RegularExpressions;

namespace RecomenderSystem
{
    public class VSM
    {
        public static Dictionary<string,int> removeSW(string sentence)
        {
            string[] stopewords = { "a", "about", "above", "above", "across", "after", "afterwards", "again", "against", "all", "almost", "alone", "along", "already", "also", "although", "always", "am", "among", "amongst", "amoungst", "amount", "an", "and", "another", "any", "anyhow", "anyone", "anything", "anyway", "anywhere", "are", "around", "as", "at", "back", "be", "became", "because", "become", "becomes", "becoming", "been", "before", "beforehand", "behind", "being", "below", "beside", "besides", "between", "beyond", "bill", "both", "bottom", "but", "by", "call", "can", "cannot", "cant", "co",
                                      "con", "could", "couldnt", "cry", "de", "describe", "detail", "do", "done", "down", "due", "during", "each", "eg", "eight", "either", "eleven", "else", "elsewhere", "empty", "enough", "etc", "even", "ever", "every", "everyone", "everything", "everywhere", "except", "few", "fifteen", "fify", "fill", "find", "fire", "first", "five", "for", "former", "formerly", "forty", "found", "four", "from", "front", "full", "further", "get", "give", "go", "had", "has", "hasnt", "have", "he", "hence", "her", "here", "hereafter", "hereby", "herein", "hereupon", "hers", "herself",
                                      "him", "himself", "his", "how", "however", "hundred", "ie", "if", "in", "inc", "indeed", "interest", "into", "is", "it", "its", "itself", "keep", "last", "latter", "latterly", "least", "less", "ltd", "made", "many", "may", "me", "meanwhile", "might", "mill", "mine", "more", "moreover", "most", "mostly", "move", "much", "must", "my", "myself", "name", "namely", "neither", "never", "nevertheless", "next", "nine", "no", "nobody", "none", "noone", "nor", "not", "nothing", "now", "nowhere", "of", "off", "often", "on", "once", "one", "only", "onto", "or", "other",
                                      "others", "otherwise", "our", "ours", "ourselves", "out", "over", "own", "part", "per", "perhaps", "please", "put", "rather", "re", "same", "see", "seem", "seemed", "seeming", "seems", "serious", "several", "she", "should", "show", "side", "since", "sincere", "six", "sixty", "so", "some", "somehow", "someone", "something", "sometime", "sometimes", "somewhere", "still", "such", "system", "take", "ten", "than", "that", "the", "their", "them", "themselves", "then", "thence", "there", "thereafter", "thereby", "therefore", "therein", "thereupon", "these", "they",
                                      "thickv", "thin", "third", "this", "those", "though", "three", "through", "throughout", "thru", "thus", "to", "together", "too", "top", "toward", "towards", "twelve", "twenty", "two", "un", "under", "until", "up", "upon", "us", "very", "via", "was", "we", "well", "were", "what", "whatever", "when", "whence", "whenever", "where", "whereafter", "whereas", "whereby", "wherein", "whereupon", "wherever", "whether", "which", "while", "whither", "who", "whoever", "whole", "whom", "whose", "why", "will", "with", "within", "without", "would", "yet", "you", "your",
                                      "yours", "yourself", "yourselves", "the",":", "(", ")" };
            Dictionary<string, int> result = new Dictionary<string,int>();
            foreach (var st in sentence.ToLower().Split(' '))
            {
                if (!stopewords.Contains(st))
                {
                    // remove spaces
                    RegexOptions options = RegexOptions.None;
                    Regex regex = new Regex(@"[ ]{2,}", options);
                    string u = regex.Replace(st, @" ");
                    // remove ()
                    u = Regex.Replace(u, "[()]", string.Empty);
                    // remove :
                    u = Regex.Replace(u, @"\+-+\+|: +:", "");
                    if (u[u.Count() - 1] == ',')
                    {
                        u = u.TrimEnd(',');
                    }
                    if (u[u.Count() - 1] == ':')
                    {
                        u = u.TrimEnd(':');
                    }
                    if (!result.Keys.Contains(u))
                    {
                        result.Add(u, 1);
                    }
                    else
                    {
                        result[u] += 1;
                    }
                }
            }
            return result;
        }

        // getting the vsm for top rated books
        public static List<Book> recommendvsm(string username)
        {
            using (BooksEntities book = new BooksEntities())
            {
                // get user info
                var user = book.Users.Where(a=>a.UserName == username).First();
                // get the top 5 latest rated book
                var top = book.Ratings.Where(a => a.Rate > 3 && a.UserID == user.UserID).OrderByDescending(a => a.Date).Select(a=>a.Book).Take(5);
                Dictionary<string,int> finalDic = new Dictionary<string,int>();
                foreach (var row in top)
                {
                    // get kaywords then calculate vsm
                    var keys = row.Keywords;
                    List<string> query = new List<string>();
                    foreach (var t in keys)
                    {
                        if (t.Freq == 1)
                        {
                            query.Add(t.Word);
                        }
                        else
                        {
                            for (int i = 0; i < t.Freq; i++)
                            {
                                query.Add(t.Word);
                            }
                        }
                    }
                    string[] queryarray = query.ToArray();
                    // calculate vsm to each book get top 2
                    var re = vsm(queryarray, user.UserID).Take(2);
                    foreach (var t in re)
                    {
                        if (finalDic.Keys.Contains(t.Key))
                        {
                            finalDic[t.Key] += 1;
                        }
                        else
                        {
                            finalDic.Add(t.Key, 1);
                        }
                    }
                }
                // return the final result
                var ordered = finalDic.OrderByDescending(a => a.Value).Take(6);
                List<Book> finalbooks = new List<Book>();
                foreach (var u in ordered)
                {
                    finalbooks.Add(book.Books.Where(a=>a.ISBN == u.Key).First());
                }
                return finalbooks;
            }
        }
        public static Dictionary<string, double> vsmSearch(string[] query, int userid)
        {
            using (BooksEntities book = new BooksEntities())
            {
                
                var td = book.Keywords.Where(a => query.Contains(a.Word));
                if (td.Count() != 0)
                {
                    List<Keyword> docs = new List<Keyword>();
                    foreach (var t in td)
                    {
                            docs.Add(t);
                    }
                    var docdis = docs.Select(a => a.ISBN).Distinct();
                    var N = book.Books.Count();
                    //var ni = docs.Count();
                    Dictionary<string, Dictionary<string, double>> finaldict = new Dictionary<string, Dictionary<string, double>>();
                    foreach (var d in docdis)
                    {
                        Dictionary<string, double> wordidf = new Dictionary<string, double>();
                        // get words in document
                        var words = docs.Where(a => a.ISBN == d);
                        foreach (var word in words)
                        {
                            if (query.Contains(word.Word))
                            {
                                //get all docs that contain this word
                                var ni = docs.Where(a => a.Word == word.Word).Select(a => a.ISBN).Distinct().Count();
                                if (ni == 0) ni = 1;
                                var tf = 1 + Math.Log10(word.Freq);
                                var idf = Math.Log10(N / ni);
                                wordidf.Add(word.Word, tf * idf);
                            }
                        }
                        if (wordidf.Count != 0)
                        {
                            finaldict.Add(d, wordidf);
                        }
                    }
                    // foreach document calculate the v norm 
                    var afternorm = ConvertFtoN(finaldict);
                    //do the same for the query
                    Dictionary<string, double> querycount = new Dictionary<string, double>();
                    Dictionary<string, double> querytfidf = new Dictionary<string, double>();
                    foreach (var s in query)
                    {
                        if (querycount.ContainsKey(s))
                        {
                            querycount[s] += 1;
                        }
                        else
                        {
                            querycount.Add(s, 1.0);
                        }
                    }
                    // tf idf
                    foreach (var word in querycount)
                    {
                        //get all docs that contain this word
                        var ni = docs.Where(a => a.Word == word.Key).Select(a => a.ISBN).Distinct().Count();
                        if (ni == 0) ni = 1;
                        var tf = 1 + Math.Log10(word.Value);
                        var idf = Math.Log10(N / ni);
                        querytfidf.Add(word.Key, tf * idf);
                    }
                    Dictionary<string, Dictionary<string, double>> queryn = new Dictionary<string, Dictionary<string, double>>();
                    queryn.Add("query", querytfidf);
                    var querynorm = ConvertFtoN(queryn);
                    //loop through the dic and for each matching keywords multiply
                    Dictionary<string, double> finalR = new Dictionary<string, double>();
                    foreach (var d in afternorm)
                    {
                        double value = 0.0;
                        foreach (var term in d.Value)
                        {
                            if (query.Contains(term.Key))
                            {
                                // get from q
                                var t = querynorm["query"].Where(a => a.Key == term.Key).Select(a => a).First();
                                value += term.Value * t.Value;
                            }
                        }
                        finalR.Add(d.Key, value);
                    }
                    finalR = finalR.OrderByDescending(a => a.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
                    return finalR;
                }
                else
                {
                    return new Dictionary<string, double>();
                }
            }

        }
        public static Dictionary<string,double> vsm(string[] query, int userid)
        {
            using (BooksEntities book = new BooksEntities())
            {
                var ratings = book.Ratings.Where(a => a.UserID == userid).Select(a=>a.ISBN);
                var dislikes = book.Dislikes.Where(a => a.UserID == userid).Select(a=>a.ISBN);
                var td = book.Keywords.Where(a=> query.Contains(a.Word));
                if (td.Count() != 0)
                {
                    List<Keyword> docs = new List<Keyword>();
                    foreach (var t in td)
                    {
                        if (!ratings.Contains(t.ISBN) && !dislikes.Contains(t.ISBN))
                        {
                            docs.Add(t);
                        }
                    }
                    var docdis = docs.Select(a => a.ISBN).Distinct();
                    var N = book.Books.Count();
                    //var ni = docs.Count();
                    Dictionary<string, Dictionary<string, double>> finaldict = new Dictionary<string, Dictionary<string, double>>();
                    foreach (var d in docdis)
                    {
                        Dictionary<string, double> wordidf = new Dictionary<string, double>();
                        // get words in document
                        var words = docs.Where(a => a.ISBN == d);
                        foreach (var word in words)
                        {
                            if (query.Contains(word.Word))
                            {
                                //get all docs that contain this word
                                var ni = docs.Where(a => a.Word == word.Word).Select(a => a.ISBN).Distinct().Count();
                                if (ni == 0) ni = 1;
                                var tf = 1 + Math.Log10(word.Freq);
                                var idf = Math.Log10(N / ni);
                                wordidf.Add(word.Word, tf * idf);
                            }
                        }
                        if (wordidf.Count != 0)
                        {
                            finaldict.Add(d, wordidf);
                        }
                    }
                    // foreach document calculate the v norm 
                    var afternorm = ConvertFtoN(finaldict);
                    //do the same for the query
                    Dictionary<string, double> querycount = new Dictionary<string, double>();
                    Dictionary<string, double> querytfidf = new Dictionary<string, double>();
                    foreach (var s in query)
                    {
                        if (querycount.ContainsKey(s))
                        {
                            querycount[s] += 1;
                        }
                        else
                        {
                            querycount.Add(s, 1.0);
                        }
                    }
                    // tf idf
                    foreach (var word in querycount)
                    {
                        //get all docs that contain this word
                        var ni = docs.Where(a => a.Word == word.Key).Select(a => a.ISBN).Distinct().Count();
                        if (ni == 0) ni = 1;
                        var tf = 1 + Math.Log10(word.Value);
                        var idf = Math.Log10(N / ni);
                        querytfidf.Add(word.Key, tf * idf);
                    }
                    Dictionary<string, Dictionary<string, double>> queryn = new Dictionary<string, Dictionary<string, double>>();
                    queryn.Add("query", querytfidf);
                    var querynorm = ConvertFtoN(queryn);
                    //loop through the dic and for each matching keywords multiply
                    Dictionary<string, double> finalR = new Dictionary<string, double>();
                    foreach (var d in afternorm)
                    {
                        double value = 0.0;
                        foreach (var term in d.Value)
                        {
                            if (query.Contains(term.Key))
                            {
                                // get from q
                                var t = querynorm["query"].Where(a => a.Key == term.Key).Select(a => a).First();
                                value += term.Value * t.Value;
                            }
                        }
                        finalR.Add(d.Key, value);
                    }
                    finalR = finalR.OrderByDescending(a => a.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
                    return finalR;
                }
                else
                {
                    return new Dictionary<string,double>();
                }
            }

        }
        public static Dictionary<string, Dictionary<string, double>> ConvertFtoN(Dictionary<string, Dictionary<string, double>> docs)
        {
            Dictionary<string, Dictionary<string, double>> newdict = new Dictionary<string, Dictionary<string, double>>();

            foreach (var d in docs)
            {
                double nr = norm(d.Value);
                Dictionary<string, double> nn = new Dictionary<string, double>();
                foreach (var word in d.Value)
                {
                    nn.Add(word.Key, word.Value / nr);
                }
                newdict.Add(d.Key, nn);
            }
            return newdict;
        }

        public static double norm(Dictionary<string, double> vector)
        {
            double sum = 0.0;
            foreach (var r in vector)
            {
                sum += r.Value * r.Value;
            }
            return Math.Sqrt(sum);
        }
        // get the tf idf for t
        public static double tf(string doc, string term)
        {
            string[] doc2 = doc.ToLower().Split(' ');
            term = term.ToLower();
            double count = 0.0;
            foreach(var r in doc2)
            {
                if (r == term)
                {
                    count = count + 1;
                }
            }
            return (Math.Log(count) + 1);

        }

        public static void idf(int count)
        {


        }
        //===================================

      /*  public static void run(string[] query)
        {
            using (BooksEntities book = new BooksEntities())
            {
                var docs = book.Keywords.Where(a => query.Contains(a));

            createWordList();
            createVector();
            classify();
            }
            var dict = sortedList;
            foreach (var x in dict.Reverse())
            {
                Console.WriteLine("{0} -> Doc{1}", x.Key, x.Value);
            }
        }
        static Hashtable DTVector = new Hashtable(); //Hashtable to hold Document Term Vector
        static List<string> wordlist = new List<string>(); //List of terms found in documents
        static Dictionary<double, string> sortedList = new Dictionary<double, string>();//Documents ranked by VSM with angle value
        static string[] docs;
        public static void createWordList()
        {
            foreach (string doc in docs)
            {
                wordlist = getWordList(wordlist, doc);
            }
        }

        public static List<string> getWordList(List<string> wordlist, string query)
        {
            Regex exp = new Regex("\\w+", RegexOptions.IgnoreCase);
            MatchCollection MCollection = exp.Matches(query);

            foreach (Match match in MCollection)
            {
                if (!wordlist.Contains(match.Value))
                {
                    wordlist.Add(match.Value);
                }
            }

            return wordlist;
        }

        public static void createVector()
        {
            double[] queryvector;

            for (int j = 0; j < docs.Length; j++)
            {
                queryvector = new double[wordlist.Count];

                for (int i = 0; i < wordlist.Count; i++)
                {

                    double tfIDF = getTF(docs[j], wordlist[i]) * getIDF(wordlist[i]);
                    queryvector[i] = tfIDF;
                }

                if (j == 0) //is it a query?
                {
                    DTVector.Add("Query", queryvector);
                }
                else
                {
                    DTVector.Add(j.ToString(), queryvector);
                }
            }
        }

        public static void classify()
        {
            double temp = 0.0;

            IDictionaryEnumerator _enumerator = DTVector.GetEnumerator();

            double[] queryvector = new double[wordlist.Count];

            Array.Copy((double[])DTVector["Query"], queryvector, wordlist.Count);

            while (_enumerator.MoveNext())
            {
                if (_enumerator.Key.ToString() != "Query")
                {
                    temp = cosinetheta(queryvector, (double[])_enumerator.Value);

                    sortedList.Add(temp, _enumerator.Key.ToString());

                }
            }
        }

        public static double dotproduct(double[] v1, double[] v2)
        {
            double product = 0.0;
            if (v1.Length == v2.Length)
            {
                for (int i = 0; i < v1.Length; i++)
                {
                    product += v1[i] * v2[i];
                }
            }
            return product;
        }

        public static double vectorlength(double[] vector)
        {
            double length = 0.0;
            for (int i = 0; i < vector.Length; i++)
            {
                length += Math.Pow(vector[i], 2);
            }

            return Math.Sqrt(length);
        }
        private static double getTF(string document, string term)
        {
            string[] queryTerms = Regex.Split(document, "\\s");
            double count = 0;


            foreach (string t in queryTerms)
            {
                if (t == term)
                {
                    count++;
                }
            }
            return count;

        }

        private static double getIDF(string term)
        {
            double df = 0.0;
            //get term frequency of all of the sentences except for the query
            for (int i = 1; i < docs.Length; i++)
            {
                if (docs[i].Contains(term))
                {
                    df++;
                }
            }

            //Get sentence count
            double D = docs.Length - 1; //excluding the query

            double IDF = 0.0;

            if (df > 0)
            {
                IDF = Math.Log(D / df);
            }

            return IDF;
        }

        public static double cosinetheta(double[] v1, double[] v2)
        {
            double lengthV1 = vectorlength(v1);
            double lengthV2 = vectorlength(v2);

            double dotprod = dotproduct(v1, v2);

            return dotprod / (lengthV1 * lengthV2);

        }

      /*  static Hashtable DTVector = new Hashtable(); //Hashtable to hold Document Term Vector
static List wordlist = new List(); //List of terms found in documents
static Dictionary sortedList = new Dictionary(); //Documents ranked by VSM with angle value
static string[] docs ={"gold silver truck", //Query
"shipment of gold damaged in a fire",//Doc 1
"delivery of silver arrived in a silver truck", //Doc 2
"shipment of gold arrived in a truck"}; //Doc 3

static void Main(string[] args)
{
createWordList();
createVector();
classify();

var dict = sortedList;
foreach (var x in dict.Reverse())
{
Console.WriteLine("{0} -> Doc{1}", x.Key, x.Value);
}


Console.ReadLine();
}


public static void createWordList()
{
foreach (string doc in docs)
{
wordlist = getWordList(wordlist, doc);
}
}

public static List getWordList(List wordlist, string query)
{
Regex exp = new Regex("\\w+", RegexOptions.IgnoreCase);
MatchCollection MCollection = exp.Matches(query);

foreach (Match match in MCollection)
{
if (!wordlist.Contains(match.Value))
{
wordlist.Add(match.Value);
}
}

return wordlist;
}

public static void createVector()
{
double[] queryvector;

for (int j = 0; j < docs.Length; j++)
{
queryvector = new double[wordlist.Count];

for (int i = 0; i < wordlist.Count; i++)
{

double tfIDF = getTF(docs[j], wordlist[i]) * getIDF(wordlist[i]);
queryvector[i] = tfIDF;
}

if (j == 0) //is it a query?
{
DTVector.Add("Query", queryvector);
}
else
{
DTVector.Add(j.ToString(), queryvector);
}
}
}

public static void classify()
{
double temp = 0.0;

IDictionaryEnumerator _enumerator = DTVector.GetEnumerator();

double[] queryvector = new double[wordlist.Count];

Array.Copy((double[])DTVector["Query"], queryvector, wordlist.Count);

while (_enumerator.MoveNext())
{
if (_enumerator.Key.ToString() != "Query")
{
temp = cosinetheta(queryvector, (double[])_enumerator.Value);

sortedList.Add(temp, _enumerator.Key.ToString());

}
}
}

public static double dotproduct(double[] v1, double[] v2)
{
double product = 0.0;
if (v1.Length == v2.Length)
{
for (int i = 0; i < v1.Length; i++)
{
product += v1[i] * v2[i];
}
}
return product;
}

public static double vectorlength(double[] vector)
{
double length = 0.0;
for (int i = 0; i < vector.Length; i++)
{
length += Math.Pow(vector[i], 2);
}

return Math.Sqrt(length);
}
private static double getTF(string document, string term)
{
string[] queryTerms = Regex.Split(document, "\\s");
double count = 0;


foreach (string t in queryTerms)
{
if (t == term)
{
count++;
}
}
return count;

}

private static double getIDF(string term)
{
double df = 0.0;
//get term frequency of all of the sentences except for the query
for (int i = 1; i < docs.Length; i++)
{
if (docs[i].Contains(term))
{
df++;
}
}

//Get sentence count
double D = docs.Length - 1; //excluding the query

double IDF = 0.0;

if (df > 0)
{
IDF = Math.Log(D / df);
}

return IDF;
}

public static double cosinetheta(double[] v1, double[] v2)
{
double lengthV1 = vectorlength(v1);
double lengthV2 = vectorlength(v2);

double dotprod = dotproduct(v1, v2);

return dotprod / (lengthV1 * lengthV2);

}
}*/

    }
}