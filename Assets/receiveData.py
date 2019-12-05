import socket
import csv

csvfile = open("..\data.csv", 'w')
writer = csv.writer(csvfile, delimiter =' ',quotechar =',',quoting=csv.QUOTE_MINIMAL)

d_list=[]
backlog = 1
size = 1024
s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
s.bind(('192.168.1.102', 50001))
s.listen(backlog)
try:
    print ("is waiting")
    client, address = s.accept()
    print ("Connected")
    while 1:
        data = client.recv(size).decode()
        if data:
            #writer.writerow(data)
            print (data)
            print(len(data))
            for e in data.split('\n'):
                d_list.append(e)
except:
    print("closing socket")
    client.close()
    s.close()
    for d in d_list:
        if(len(d)>0):
            writer.writerow(d)
    csvfile.close()
    
