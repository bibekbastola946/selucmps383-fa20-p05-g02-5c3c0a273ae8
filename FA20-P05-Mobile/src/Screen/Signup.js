import React, { useState } from "react";
import { StyleSheet, View, Image, Alert} from "react-native";
import Logo from "../Screen/logo.png";
import { TextInput, Button } from "react-native-paper";
import { API_ENDPOINT } from "../constants/constant";


const Signup = props => {
    const [Username, setUserName] = useState();
    const [Password, setPassword] = useState();
    const [Email, setEmail] = useState();
    const [PhoneNumber, setPhoneNumber] = useState("");
    const handleSignup = async () => {
      fetch(`${API_ENDPOINT}/api/users`, {
        method: "POST",
        headers: {
          "Accept": "application/json",
          "Content-Type": "application/json"
        },
        body: JSON.stringify({
          Username: Username,
          Password: Password,
          PhoneNumber: PhoneNumber,
          Email: Email
        })
      })
        .then(res => {
          if (res.ok) {
            return res.json();
          } else {
            Alert.alert("Invalid User or User Already Exists");
            window.stop();
          }
        })
        .then(data => {
          Alert.alert("Sucessful SignUp", data);
          props.props.navigation.push("Login");
        })
        .catch(error => {
          console.log(error);
        });
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
          label="Email"
          style={styles.inputStyle}
          theme={theme}
          value={Email}
          mode="outlined"
          secureTextEntry={true}
          onChangeText={text => setEmail(text)}
        />
        <TextInput
        label="PhoneNumber"
        style={styles.inputStyle}
        mode="outlined"
        autoCapitalize="none"
        theme={theme}
        onChangeText={text => setPhoneNumber(text)}
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
         <TextInput
          label="Confirm Password"
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
          onPress={() => handleSignup()}
        >
          Register
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
    flex: 1
  },
  
  inputStyle: {
    margin: 10
  },
  submitButton: {
    backgroundColor: "green",
    margin: 80,
    marginTop: 20
  }
});

export default Signup;
