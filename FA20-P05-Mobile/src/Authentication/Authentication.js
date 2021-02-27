import AsyncStorage from "@react-native-community/async-storage";
import { API_ENDPOINT } from "../constants/constant";
//import * as SecureStore from 'expo-secure-store';

class authentication {
  login(Username, Password) {
    return fetch(API_ENDPOINT+"/api/authentication/login", {

      method: "POST",
      
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        Username,
        Password,
      }),
      credentials: 'include',
    }).then((res) => {
      if (res.ok) {
          //SecureStore.setItemAsync(Username, Password, SecureStore.ALWAYS)
        return res.json();
      } else {
        console.log("Invalid Username or Password");
        
      }
    })
  }
  async saveItem(item, selectedValue) {
    try {
      await AsyncStorage.setItem(item, selectedValue);
    } catch (error) {
      console.error('AsyncStorage error: ' + error.message);
    }
  }

  
  logout() {
    AsyncStorage.getAllKeys().then((keys) => AsyncStorage.multiRemove(keys));
  }
}
export default new authentication();