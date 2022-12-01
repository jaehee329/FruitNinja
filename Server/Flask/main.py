from flask import Flask
from flask_cors import CORS, cross_origin
from flask_redis import FlaskRedis
import json
import ast
import numpy as np



HAND_AVG_NUM = 3 # num of hand data to take average on (>=1)
REDIS_URL = "redis://:password@localhost:6379/0"


app = Flask("__main__")
cors = CORS(app)
redis_client = FlaskRedis(app)



# [GET /]
@app.route("/")
@cross_origin()
def home():
	return "home"


# [GET /hand]
@app.route("/hand")
@cross_origin()
def hand():
	res = redis_client.lrange('hand',0,HAND_AVG_NUM-1)

	hand_avg = {}
	if (res):
		for i in range(21):
			x = 0
			y = 0
			z = 0
			# ast.literal_eval(res.decode("UTF-8"))
			for j,_ in enumerate(res):
				x += ast.literal_eval(res[j].decode("UTF-8"))[str(i)][0]
				y += ast.literal_eval(res[j].decode("UTF-8"))[str(i)][1]
				z += ast.literal_eval(res[j].decode("UTF-8"))[str(i)][2]
			hand_avg[i] = [x/HAND_AVG_NUM, y/HAND_AVG_NUM, z/HAND_AVG_NUM]

	# print(hand_avg)

	hand_coor_val = hand_coor(hand_avg) if hand_avg else [100,100,100]
	hand_type_val = hand_type(hand_avg) if hand_avg else 0 # default
	return {"hand_coor": hand_coor_val, "hand_type": hand_type_val}
	# return hand


# [GET /hand_data_num]
@app.route("/hand_data_num")
@cross_origin()
def hand_data_num():
	res = redis_client.lrange('hand',0,-1)
	return str(len(res))



def hand_coor(hand):
	x = 0
	y = 0
	z = 0
	for _,v in hand.items():
		x += v[0]
		y += v[1]
		z += v[2]
	l = len(hand)
	return [x/l, y/l, z/l]


# return 0 if blade
# return 1 if grab
def hand_type(hand):
	# 0-5-17: palm
	# 4,8,12,16,20: finger tips
	# get plane equation of palm
	# get distance of finger tips from palm
	# use the distance magnitude to determine type
	# a*x + b*y + c*z = d

	for k in hand:
		hand[k][2] = (-0.5 - hand[k][2])*1

	p1 = np.array(hand[0])
	p2 = np.array(hand[5])
	p3 = np.array(hand[17])
	v1 = p3-p1
	v2 = p2-p1
	v3 = p2-p3
	cp = np.cross(v1,v2)
	a,b,c = cp
	d = np.dot(cp,p3)
	distances = [abs(p[0]*a + p[1]*b + p[2]*c - d)/(a**2 + b**2 + c**2) for p in (hand[4],hand[8],hand[12],hand[16],hand[20])]
	offset = 0
	for d in distances:
		offset += d

	# normalize offset
	norm = 0
	for k,v in hand.items():
		print(v[2])
		norm += (v[0]**2 + v[1]**2 + v[2]**2)
		# norm += (v[2])**2
	# offset /= (np.linalg.norm(v1)**2 + np.linalg.norm(v2)**2 + np.linalg.norm(v3)**2)
	offset /= norm


	print(offset)
	print(norm)
	print(np.linalg.norm(v1)**2)
	print(np.linalg.norm(v2)**2)
	print(np.linalg.norm(v3)**2)

	return 0 if offset < 2 else 1







if __name__ == "__main__":
	app.run(port=8000)