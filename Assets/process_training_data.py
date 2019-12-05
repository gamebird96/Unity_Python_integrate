import csv


data_list = []

csvfile = open("..\data.csv", 'r')
reader = csv.reader(csvfile, dialect='excel')

for row in reader:
    if(len(row)>0):
        d = filter(lambda x: len(x)>0 , row)
        data_list.append(list(d))

csvfile2 = open("..\data_modified.csv", 'w')
writer = csv.writer(csvfile2)
writer.writerows(data_list)
