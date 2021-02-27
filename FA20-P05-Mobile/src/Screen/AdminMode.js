import React, { useState } from "react";
import { StyleSheet, View, Image, Alert, Picker} from "react-native";
import Logo from "../Screen/logo.png";
import { TextInput, Button } from "react-native-paper";
import { DrawerActions } from 'react-navigation-drawer';
import { Ionicons } from '@expo/vector-icons';
import { API_ENDPOINT } from "../constants/constant";
import { AsyncStorage } from 'react-native';
import Authentication from "../Authentication/Authentication";


const AdminHome = ({navigation}) => {
  const [id, setId] = useState([]);
  const [temperatureFahrenheit, setTemperature] = useState();
    const [schoolId, setSchoolId] = useState("2");

    
    const submitData = async () => {

      fetch(`${API_ENDPOINT}/api/temperature-records`, {
        method: "POST",
        
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
        },
       
        body: JSON.stringify({
          schoolId: parseInt(schoolId),
          temperatureFahrenheit: parseFloat(temperatureFahrenheit)
        }),
        credentials: "include",
      })
        .then(res => {
          if (res.ok) {
            navigation.navigate('QrPage')
            return res.json();
            
          } else {
            Alert.alert("Added Temperature");
            window.stop();
          }
        })
        .catch(error => {  
          console.log("Problem with fetch", error);
        });
    };
  return (
    <View style={styles.myCard}>
      <View>
      
      <Picker
        schoolId={schoolId}
        style={{ height: 50, width: 150 }}
        onValueChange={(ivalue, itemIndex) => setSchoolId(ivalue)}
      >
           <Picker.Item label="Hammond Westside Montessori School" value="2" />
          <Picker.Item label="Hammond Eastside Magnet School" value="3" />
        </Picker>
        
        <TextInput
          label="Temperature Recorded"
          style={styles.inputStyle}
          theme={theme}
          value={temperatureFahrenheit}
          mode="outlined"
          secureTextEntry={true}
          onChangeText={text => setTemperature(text)}
        />
        <Button
          mode="contained"
          style={styles.submitButton}
          onPress={() => submitData()}
        >
          Submit Record
        </Button>

        <Button
          mode="contained"
          style={styles.Qr}
          onPress={() => navigation.navigate('QRCode')}
        >
          Latest QRCode
        </Button>
      </View>
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
  Qr: {
    backgroundColor: "green",
    margin: 10,
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

export default AdminHome;
