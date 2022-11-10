# 8080 포트로의 모든 연결에 대해 hello+i 메세지를 1회 보낸다.
from socket import *
import time

serverSocket=socket(AF_INET,SOCK_STREAM)

HOST = ''
PORT = 8080

serverSocket.bind((HOST, PORT))
serverSocket.listen(1)

sequence = 1

while True:
    print('The server is ready to receive')
    connectionSocket, addr = serverSocket.accept() 
    print('Connected by', addr)

    msg = 'hello ' + str(sequence)
    connectionSocket.send(msg.encode('utf-8'))
    sequence += 1
    connectionSocket.close()

