#! /usr/bin/python3

import json
import redis
import cv2
import mediapipe as mp
import time


r = redis.Redis(host='localhost', port=6379, db=0)
r.flushdb() # delete all keys
r.lpush('hand', json.dumps({'init': 0}))
if r.pexpire('hand',3000): # 300 millisec
  print("TIME OUT SET")
else:
  print("TIME OUT NOT SET")

mp_drawing = mp.solutions.drawing_utils
mp_drawing_styles = mp.solutions.drawing_styles
mp_hands = mp.solutions.hands



# For webcam input:
cap = cv2.VideoCapture(0)
with mp_hands.Hands(
    model_complexity=1,
    min_detection_confidence=0.5,
    min_tracking_confidence=0.7) as hands:
  while cap.isOpened():
    success, image = cap.read()
    if not success:
      print("Ignoring empty camera frame.")
      # If loading a video, use 'break' instead of 'continue'.
      continue

    # To improve performance, optionally mark the image as not writeable to
    # pass by reference.
    image.flags.writeable = False
    image = cv2.cvtColor(image, cv2.COLOR_BGR2RGB)
    results = hands.process(image)

    # Draw the hand annotations on the image.
    image.flags.writeable = True
    image = cv2.cvtColor(image, cv2.COLOR_RGB2BGR)

    # to redis
    if results.multi_hand_landmarks:
      for hand_landmarks in results.multi_hand_landmarks:
        # print("\n\n\n ######################## \n\n\n")
        hand_dict = {}
        for i,p in enumerate(hand_landmarks.landmark):
          hand_dict[i] = [p.x,p.y,p.z]

        print(hand_dict)
        json_data = json.dumps(hand_dict)
        r.lpush('hand', json_data) # list expire is resetted after every update
        r.pexpire('hand',300)
        # print(json_data)
        # print("\n\n\n @@@@@@@@@@@@@@@@@@@@@@@ \n\n\n")






    if results.multi_hand_landmarks:
      for hand_landmarks in results.multi_hand_landmarks:
        mp_drawing.draw_landmarks(
            image,
            hand_landmarks,
            mp_hands.HAND_CONNECTIONS,
            mp_drawing_styles.get_default_hand_landmarks_style(),
            mp_drawing_styles.get_default_hand_connections_style())


    # Flip the image horizontally for a selfie-view display.
    cv2.imshow('MediaPipe Hands', cv2.flip(image, 1))
    
    if cv2.waitKey(5) & 0xFF == 27:
      break
cap.release()