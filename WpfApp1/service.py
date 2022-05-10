from SimpleWebSocketServer import SimpleWebSocketServer, WebSocket
import pickle
import json
import pandas as pd
import numpy as np
from sklearn.ensemble import RandomForestClassifier
from sklearn import metrics
from sklearn.metrics import roc_auc_score

TripModel = None
class WebSocket(WebSocket):

    def handleMessage(self):
       data = np.array(json.loads(self.data)).astype(float)
       print(data.shape)
       global TripModel
       predictedTrip = TripModel.predict([data])
       print(predictedTrip)
       data = np.array([])
       data = np.append(data,predictedTrip)
       print(data.shape)
       data = json.dumps(data.tolist())
       print(data)
       self.sendMessage((u""+data))

    def handleConnected(self):
       print(self.address, 'connected')

    def handleClose(self):
       print(self.address, 'closed')

if __name__ == '__main__':
	
	TripModel = pickle.load(open('finalized_model.sav', 'rb'))

	csvTest = pd.read_csv('df_feature_test.csv')
	csvTest = csvTest.drop(columns=['bookingID'],axis=1)
	prob = TripModel.predict_proba(csvTest.drop(columns=['label'],axis=1))[:,1]
	print('Nilai validasi AUC :', roc_auc_score(csvTest.label,prob))
	print('Accuracy: ', metrics.accuracy_score(csvTest.label,prob.round()))
		
	clients = []
	server = SimpleWebSocketServer('127.0.0.1', 8000, WebSocket)
	server.serveforever()
