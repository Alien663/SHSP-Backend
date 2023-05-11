using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Xml.XPath;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Tokenization;

class Program
{
    static void Main()
    {
        string test = @"壬戌之秋，七月既望，蘇子與客泛舟遊於赤壁之下。清風徐來，水波不興，舉酒屬客，誦明月之詩，歌窈窕之章。少焉，月出於東山之上，徘徊於斗牛之間，白露橫江，水光接天；縱一葦之所如，凌萬頃之茫然。浩浩乎如馮虛御風，而不知其所止；飄飄乎如遺世獨立，羽化而登仙。

於是飲酒樂甚，扣舷而歌之。歌曰：「桂棹兮蘭槳，擊空明兮泝流光。渺渺兮予懷，望美人兮天一方。」客有吹洞簫者，倚歌而和之，其聲嗚嗚然，如怨如慕，如泣如訴，餘音嫋嫋，不絕如縷。舞幽壑之潛蛟，泣孤舟之嫠婦。

蘇子愀然，正襟危坐，而問客曰：「何為其然也？」

客曰：「『月明星稀，烏鵲南飛』，此非曹孟德之詩乎？西望夏口，東望武昌，山川相繆，鬱乎蒼蒼，此非孟德之困於周郎者乎？方其破荊州，下江陵，順流而東也，舳艫千里，旌旗蔽空，釃酒臨江，橫槊賦詩，固一世之雄也，而今安在哉？況吾與子，漁樵於江渚之上，侶魚蝦而友麋鹿；駕一葉之扁舟，擧匏樽以相屬。寄蜉蝣於天地，渺滄海之一粟。哀吾生之須臾，羨長江之無窮。挾飛仙以遨遊，抱明月而長終。知不可乎驟得，託遺響於悲風。」

蘇子曰：「客亦知夫水與月乎？逝者如斯，而未嘗往也；盈虛者如彼，而卒莫消長也，蓋將自其變者而觀之，則天地曾不能以一瞬；自其不變者而觀之，則物與我皆無盡也，而又何羨乎？且夫天地之間，物各有主，苟非吾之所有，雖一毫而莫取。惟江上之清風，與山間之明月，耳得之而為聲，目遇之而成色，取之無禁，用之不竭，是造物者之無盡藏也，而吾與子之所共適。」

客喜而笑，洗盞更酌。肴核既盡，杯盤狼籍，相與枕藉乎舟中，不知東方之既白。";
        List<TokenModel> Semgnets = Segmentation(test);
        Console.WriteLine(Semgnets);

        string test2 = @"蘇子與客泛舟遊於赤壁之下";
        List<TokenModel> Tokens = Tokenization(test2);
        Console.WriteLine(Tokens);

        Console.ReadLine();
    }
    static List<TokenModel> Segmentation(string context)
    {
        string temp = context;
        List<TokenModel> result = new List<TokenModel>();
        List<PunctuationModel> AllPunctuationMarks = new List<PunctuationModel>
        {
            new PunctuationModel {Marks= ","},
            new PunctuationModel {Marks= "."},
            new PunctuationModel {Marks= "?"},
            new PunctuationModel {Marks= "!"},
            new PunctuationModel {Marks= "，"},
            new PunctuationModel {Marks= "。"},
            new PunctuationModel {Marks= "？"},
            new PunctuationModel {Marks= "！"},
            new PunctuationModel {Marks= ";"},
            new PunctuationModel {Marks= ":"},
            new PunctuationModel {Marks= "："},
            new PunctuationModel {Marks= "；"},
            new PunctuationModel {Marks= "'"},
            new PunctuationModel {Marks= "\""},
            new PunctuationModel {Marks= "("},
            new PunctuationModel {Marks= ")"},
            new PunctuationModel {Marks= "["},
            new PunctuationModel {Marks= "]"},
            new PunctuationModel {Marks= "{"},
            new PunctuationModel {Marks= "}"},
            new PunctuationModel {Marks= "（"},
            new PunctuationModel {Marks= "）"},
            new PunctuationModel {Marks= "［"},
            new PunctuationModel {Marks= "］"},
            new PunctuationModel {Marks= "｛"},
            new PunctuationModel {Marks= "｝"},
            new PunctuationModel {Marks= "「"},
            new PunctuationModel {Marks= "」"},
            new PunctuationModel {Marks= "『"},
            new PunctuationModel {Marks= "』"},
            new PunctuationModel {Marks= "\n"},
        };
        List<PunctuationModel> PunctuationMarks = AllPunctuationMarks.Where(p => context.IndexOf(p.Marks) >= 0 ).ToList();
        int ID = 1;
        while(temp.Length > 0)
        {
            int min_index = int.MaxValue;
            string mark = "";
            PunctuationMarks.ForEach(item =>
            {
                int indexof = temp.IndexOf(item.Marks);
                if (indexof >= 0 && indexof < min_index)
                {
                    min_index = indexof;
                    mark = item.Marks;
                }
            });

            if (min_index == int.MaxValue)
            {
                min_index = temp.Length;
            }
            if(! string.IsNullOrWhiteSpace(temp.Substring(0, min_index)))
            result.Add(new TokenModel
            {
                ID = ID++,
                Context = temp.Substring(0, min_index),
                Mark = mark,
            });
            if (min_index == int.MaxValue) break;
            temp = temp.Substring(min_index + 1);
        }
        return result;
    }

    static List<TokenModel> Tokenization(string context, int window = 6)
    {
        List<TokenModel> result = new List<TokenModel>();
        int ID = 1;
        for (int i = 1; i<= window; i++)
        {
            for(int j=0; j <= context.Length - i; j++)
            {
                result.Add(new TokenModel { ID = ID++, Context = context.Substring(j, i) });
            }
        }
        return result;
    }
}