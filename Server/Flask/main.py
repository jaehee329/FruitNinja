from flask import Flask
from flask_redis import FlaskRedis
import json
import ast

REDIS_URL = "redis://:password@localhost:6379/0"

app = Flask("__main__")
redis_client = FlaskRedis(app)






@app.route("/")
def home():
	return "home"

@app.route("/hand")
def hand():
	hand = redis_client.get('hand')
	hand = ast.literal_eval(hand.decode("UTF-8"))
	print(hand)



	return {"hand_coor": hand_coor(hand), "hand_type": hand_type(hand)}


def hand_coor(hand):
	x = 0
	y = 0
	z = 0
	for p in hand:
		x += p.x
		y += p.y
		z += p.z
	l = len(hand)
	return [x/l, y/l, z/l]

def hand_type(hand):
	return 0







if __name__ == "__main__":
	app.run()