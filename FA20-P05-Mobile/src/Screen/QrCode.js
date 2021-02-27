import React, { useState } from "react";
import { StyleSheet, View, Image, Alert, Picker} from "react-native";
import Logo from "../Screen/logo.png";
import { TextInput, Button } from "react-native-paper";
import { DrawerActions } from 'react-navigation-drawer';
import { Ionicons } from '@expo/vector-icons';
import { API_ENDPOINT } from "../constants/constant";
import { AsyncStorage } from 'react-native';


const QrCode = ({navigation}) => {

    const FetchTemp = async () => {
        fetch(`${API_ENDPOINT}/api/TemperatureRecordDtoes`)
          .then(res => {
            if (res.ok) {
              res.json().then(
                result => {  
                  setQrUrl(result[(result.length-1)].qrcode)
                  console.log(QrUrl)

                })
            } 
          })
          .then()
      };

    
    
    const [QrUrl, setQrUrl] = useState();
    
    
  return (
    <View style={styles.myCard}>
      <Button onPress={() => FetchTemp()}>Get Latest QR CODE</Button>
      <Image
              source={{uri: QrUrl}}
              style={{ width: 400, height: 400 }}
            />
    </View>
  );
};
const theme = {
  colors: {
    primary: "green"
  }
};
const styles = StyleSheet.create({
  myCard: {
    flex: 1,
    resizeMode: 'cover',

  backgroundColor:'#dffad2',
  },
  
  inputStyle: {
    margin: 10
  },
  submitButton: {
    backgroundColor: "green",
    margin: 80,
    marginTop: 20
  },
  back: {
    margin: 10,
    marginLeft:20,
   
    top: 0,
    left: 0,
    width: 25,
    height: 25,
    color: "tomato"
  }
  
});

export default QrCode;
