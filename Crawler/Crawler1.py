import os
import requests
from bs4 import BeautifulSoup

echelons = ["1120310", "1111010", "1110310", "1101010", "1100310", "1091010", "1090315", "1081015", "1080315", "1071031", "1070315", "1061031", "1060315", "1051031", "1050315", "1041031", "1040315", "1031031", "1030315", "1021031", "1020315", "1011130", "1010315", "1001031", "1000315"]
counties = [
    {
        "Name" : "新北",
        "Value": "1",
    },
    {
        "Name" : "高二",
        "Value": "2",
    },
    {
        "Name" : "高雄",
        "Value": "3",
    },
    {
        "Name" : "花蓮",
        "Value": "4",
    },
    {
        "Name" : "基隆",
        "Value": "5",
    },
    {
        "Name" : "嘉義",
        "Value": "6",
    },
    {
        "Name" : "苗栗",
        "Value": "7",
    },
    {
        "Name" : "南投",
        "Value": "8",
    },
    {
        "Name" : "屏東",
        "Value": "9",
    },
    {
        "Name" : "臺北",
        "Value": "10",
    },
    {
        "Name" : "臺東及海外",
        "Value": "11",
    },
    {
        "Name" : "臺南",
        "Value": "12",
    },
    {
        "Name" : "臺中",
        "Value": "13",
    },
    {
        "Name" : "桃園",
        "Value": "14",
    },
    {
        "Name" : "新竹",
        "Value": "15",
    },
    {
        "Name" : "宜蘭",
        "Value": "16",
    },
    {
        "Name" : "雲林",
        "Value": "17",
    },
    {
        "Name" : "彰化",
        "Value": "18",
    },
]
root_folder = ""
url = "https://www.shs.edu.tw/Customer/Winning/OverIndex"
the_api = "https://www.shs.edu.tw/Customer/Winning/GetOverAll?draw=1&columns[0][data]=Id&columns[0][name]=Id&columns[0][searchable]=false&columns[0][orderable]=false&columns[0][search][value]=&columns[0][search][regex]=false&columns[1][data]=HistoryOverAreaName&columns[1][name]=HistoryOverAreaName&columns[1][searchable]=false&columns[1][orderable]=false&columns[1][search][value]=&columns[1][search][regex]=false&columns[2][data]=HistoryEssayOverCityName&columns[2][name]=HistoryEssayOverCityName&columns[2][searchable]=false&columns[2][orderable]=true&columns[2][search][value]=&columns[2][search][regex]=false&columns[3][data]=HistoryOverSchoolName&columns[3][name]=HistoryOverSchoolName&columns[3][searchable]=false&columns[3][orderable]=true&columns[3][search][value]=&columns[3][search][regex]=false&columns[4][data]=HistoryOverUserGrade&columns[4][name]=HistoryOverUserGrade&columns[4][searchable]=false&columns[4][orderable]=true&columns[4][search][value]=&columns[4][search][regex]=false&columns[5][data]=HistoryOverUserClass&columns[5][name]=HistoryOverUserClass&columns[5][searchable]=false&columns[5][orderable]=true&columns[5][search][value]=&columns[5][search][regex]=false&columns[6][data]=HistoryOverUserName&columns[6][name]=HistoryOverUserName&columns[6][searchable]=false&columns[6][orderable]=true&columns[6][search][value]=&columns[6][search][regex]=false&columns[7][data]=HistoryOverTeacherName1&columns[7][name]=HistoryOverTeacherName1&columns[7][searchable]=false&columns[7][orderable]=true&columns[7][search][value]=&columns[7][search][regex]=false&columns[8][data]=HistoryOverTitleName&columns[8][name]=HistoryOverTitleName&columns[8][searchable]=false&columns[8][orderable]=true&columns[8][search][value]=&columns[8][search][regex]=false&columns[9][data]=HistoryOverChineseName&columns[9][name]=HistoryOverChineseName&columns[9][searchable]=false&columns[9][orderable]=true&columns[9][search][value]=&columns[9][search][regex]=false&columns[10][data]=HistoryOverRanking&columns[10][name]=HistoryOverRanking&columns[10][searchable]=false&columns[10][orderable]=false&columns[10][search][value]=&columns[10][search][regex]=false&columns[11][data]=Id&columns[11][name]=&columns[11][searchable]=true&columns[11][orderable]=true&columns[11][search][value]=&columns[11][search][regex]=false&order[0][column]=0&order[0][dir]=asc&start=0&length=5000&search[value]=&search[regex]=false&schoolNo=&txtName=&txtAuthorName=&grade=&ranking=&_=1682386843397&"

for echelon in echelon:
    os.mkdir(root_folder + "/" + echelon)
    for county in counties:
        the_api += "contestNo=" + echelon + "&areaId=" + county["Value"]
        res = requests.get(the_api)
        with open(county + ".json", "a", encoding="utf-8") as fp:
            fp.write(res.content)
        fp.close()