import React, { useState, useEffect } from 'react';
import { Text, View, StyleSheet, Button } from 'react-native';
import { BarCodeScanner } from 'expo-barcode-scanner';
import axios from "axios";
import { API_ENDPOINT } from "../constants/constant";
import ajax from "../ajax";
import TemperatureRecords from "./ScanedTemperature"
const Scanner = ({navigation}) => { 
  const [hasPermission, setHasPermission] = useState(null);
  const [scanned, setScanned] = useState(false);
  const [id,setId]= useState();
  const [contents, setContent] = useState();
 
  useEffect(() => {
    (async () => {
      const { status } = await BarCodeScanner.requestPermissionsAsync();
      setHasPermission(status === 'granted');
    })();
  }, []);
 
  async function FetchTemp(e) {
    const result = await axios.get(`${API_ENDPOINT}/temperature-records/${e.data}`);
    const Fetcheddatas=result.data;
    const temp=Fetcheddatas.map((fetchdata)=>(fetchdata.temperatureKelvin));
    setContent(Fetcheddatas);
    setScanned(true);
    alert(`Your temperature is ${temp}`);
  };
  
   function  handleBarCodeScanned ({ type, data }) {
    setScanned(true);
    setId(data); 
    console.log(id);
    alert(`Bar code with type ${type} and data ${data} has been scanned!`);
    
  }

  if (hasPermission === null) {
    return <Text>Requesting for camera permission</Text>;
  }
  if (hasPermission === false) {
    
    return <Text>No access to camera</Text>;
  }
  
  return (
  
    <View
      style={{
        flex: 1,
        flexDirection: 'column',
        justifyContent: 'flex-end',
      }}>
     
      <BarCodeScanner
        onBarCodeScanned={scanned ? undefined : FetchTemp}
        style={StyleSheet.absoluteFillObject}
      />
  
  {scanned && <Button title={'Tap to Scan Again'} onPress={() => setScanned(false)} />}
       {/* {scanned && <TemperatureRecords items={contents} />}   */}
       {/* {scanned &&  navigation.navigate("TemperatureRecords",{items: contents})} */}
    </View>
  );
}
export default Scanner; 


