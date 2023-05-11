import os
import json

root_folder = ""
echelons = ["1120310", "1111010", "1110310", "1101010", "1100310", "1091010", "1090315", "1081015", "1080315", "1071031", "1070315", "1061031", "1060315", "1051031", "1050315", "1041031", "1040315", "1031031", "1030315", "1021031", "1020315", "1011130", "1010315", "1001031", "1000315"]

json_data = {}

for folder in echelons:
    for filepath in os.listdir(root_folder + "/" + folder):
        with open(root_folder + "/" + folder + "/" + filepath) as fp:
            json_data = fp.read()
        fp.close()
        
