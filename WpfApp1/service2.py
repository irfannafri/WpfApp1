from SimpleWebSocketServer import SimpleWebSocketServer, WebSocket
import pickle
import json
import pandas as pd
import numpy as np
from sklearn.ensemble import RandomForestClassifier
from sklearn import metrics
from sklearn.metrics import roc_auc_score
from sklearn.metrics import confusion_matrix

tripModel = None
class TripWebSocket(WebSocket):

    def handleMessage(self):
        data = np.array(json.loads(self.data)).astype(float)
        data = data[:n_features]
        print(data.shape)
        global tripModel
        predictedTrip = tripModel.predict([data])
        print(predictedTrip)
        accu = data[0]
        bear = data[1]
        acx = data[2]
        acy = data[3]
        acz = data[4]
        gyrox = data[5]
        gyroy = data[6]
        gyroz = data[7]
        second = data[8]
        speed = data[9]
        data = np.array([])
        data = np.append(data,predictedTrip)
        data = np.append(data,accu)
        data = np.append(data,bear)
        data = np.append(data,acx)
        data = np.append(data,acy)
        data = np.append(data,acz)
        data = np.append(data,gyrox)
        data = np.append(data,gyroy)
        data = np.append(data,gyroz)
        data = np.append(data,second)
        data = np.append(data,speed)
        print(data.shape)
        data = json.dumps(data.tolist())
        print(data)
        self.sendMessage((u""+data))

    def handleConnected(self):
        print(self.address, 'connected')

    def handleClose(self):
        print(self.address, 'closed')

if __name__ == '__main__':
	n_features = 10
	n_label = 1
	tripModel = pickle.load(open('finalized_model.sav', 'rb'))

	csvTest = pd.read_csv('df_feature_test.csv').values
	#csvTest = csvTest.drop(columns=['bookingID'],axis=1).T
	csvTest = csvTest.T
	dataTest = csvTest[:n_features].T
	
	labelTest = csvTest[n_features:(n_features+n_label)]

	scoreTest = tripModel.score(dataTest,labelTest.ravel())
	predictedTrip = tripModel.predict(dataTest)
	
	confusion_Trip = confusion_matrix(labelTest.ravel(), predictedTrip)
	print("Accuracy :",scoreTest)
	clients = []
	print("confusion matrix")
	print(confusion_Trip)
	server = SimpleWebSocketServer('127.0.0.1', 8000, TripWebSocket)
	server.serveforever()
