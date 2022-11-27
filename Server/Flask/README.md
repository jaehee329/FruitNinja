## overview

server url: localhost:8000
redis url:  localhost:6379

1. GET localhost:8000/hand
  * returns latest hand coordinates as dict(json) format


## redis data structure
client['hand'] = { "0": [x,y,z], ... } # 21 data points



# HOW TO RUN

NOTE: requires camera access to run

1. install docker (client)
  `brew install docker`
1. install docker-desktop (for daemon)
  https://www.docker.com/products/docker-desktop/
1. run docker-desktop
1. download redis container
  `docker pull redis`
1. install requirements
  `pip isntall -r requirements.txt`
1. start redis
  `docker run --name=redis-devel --publish=6379:6379 --hostname=redis --restart=on-failure --detach redis:latest`
1. start server
  `python3 main.py`




