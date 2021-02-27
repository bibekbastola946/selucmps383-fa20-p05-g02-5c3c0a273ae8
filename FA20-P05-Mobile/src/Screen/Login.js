import React, { useState } from "react";
import { StyleSheet, View, Image, Alert} from "react-native";
import Logo from "../Screen/logo.png";
import { TextInput, Button } from "react-native-paper";
import { DrawerActions } from 'react-navigation-drawer';
import { Ionicons } from '@expo/vector-icons';
import { API_ENDPOINT } from "../constants/constant";
import { AsyncStorage } from 'react-native';
import Authentication from "../Authentication/Authentication";




const Login = ({navigation}) => {
    const [Username, setUserName] = useState();
    const [Password, setPassword] = useState();
    
    
    const submitData = async () => {
      
      Authentication.login(Username, Password)
      .then( navigation.navigate("AdminMode"))      
    };

    
  return (
    <View style={styles.myCard}>
      

      <View>
     
        <TextInput
          label="UserName"
          style={styles.inputStyle}
          theme={theme}
          value={Username}
          mode="outlined"
          autoCapitalize="none"
          onChangeText={text => setUserName(text)}
        />
        <TextInput
          label="Password"
          style={styles.inputStyle}
          theme={theme}
          value={Password}
          mode="outlined"
          secureTextEntry={true}
          onChangeText={text => setPassword(text)}
        />
        <Button
          mode="contained"
          style={styles.submitButton}
          onPress={() => submitData()}
        >
          Sign In
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

export default Login;
