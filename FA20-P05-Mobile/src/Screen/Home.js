import React from 'react';
import { StyleSheet, Text, View, Image, Button, ImageBackground } from 'react-native';
import Logo from "../Screen/logo.png";
import { Ionicons } from '@expo/vector-icons';

export default function Home({ navigation }) {
  return (
    <View style={{ flex: 1 }}>
      <ImageBackground
      source={require('./secondLogo.png')} 
      style={styles.image}
     />
      <Ionicons style={styles.close} name="ios-list" size={40} onPress = {()=> navigation.toggleDrawer()}/>
      
    </View>
  );
}
const styles = StyleSheet.create({
  myCard: {
    flex: 1
  },
  Button: {
    padding: 5
  },
  logo: {
    height: 300,
    width: 400,
    margin: 20,
    marginTop: -200,
    alignContent: "center"
  },
  image: {
    flex: 1,
    resizeMode: 'cover',
    justifyContent: 'center',
  },
  inputStyle: {
    margin: 10
  },
  submitButton: {
    backgroundColor: "green",
    margin: 80,
    marginTop: 20
  },
  close: {
    margin: 10,
    marginTop: 50,
    position: "absolute",
    top: 10,
    left: 0,
    width: 25,
    height: 25,
    color: "tomato"
  }
});


