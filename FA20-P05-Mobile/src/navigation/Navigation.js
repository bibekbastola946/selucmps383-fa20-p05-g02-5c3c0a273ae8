

import React, { useState, useEffect } from 'react';
import { NavigationContainer } from '@react-navigation/native';
import { createStackNavigator } from '@react-navigation/stack';
import  Details  from "../Screen/Details.js";
import Home from '../Screen/Home'
import Scanner from '../Screen/Scanner'
import Login from '../Screen/Login.js';
import Signup from '../Screen/Signup.js';
import TemperatureRecords from '../Screen/TemperatureRecords.js';
import AdminMode from '../Screen/AdminMode';
import QrCode from '../Screen/QrCode';
import { createDrawerNavigator } from 'react-navigation-drawer';
import { createAppContainer } from 'react-navigation';
import { State } from 'react-native-gesture-handler';


let DrawerNotLoggedIn = createDrawerNavigator(
  {

    Home:{ screen: Home},
    Scanner:{ screen: Scanner},
    TemperatureRecords:{ screen: TemperatureRecords},
    QrCode: {screen: QrCode},
    LogIn:{ screen: LogIn},
  },
  {
    initialRouteName: "Home",
    unmountInactiveRoutes: true,
    headerMode: "none",
    
    
  },
  
 
)
const DrawerLogginIn = createDrawerNavigator(
  {

    Home:{ screen: Home},
    Scanner:{ screen: Scanner},
    TemperatureRecords:{ screen: TemperatureRecords},
    AdminMode: {screen: AdminMode}
  },
  {
    initialRouteName: "AdminMode",
    unmountInactiveRoutes: true,
    headerMode: "none",
    

  }
)




const Stack = createStackNavigator()
let AppContainer = createAppContainer(DrawerNotLoggedIn)

function LogIn() {
  return (
    <NavigationContainer>
      <Stack.Navigator>
      <Stack.Screen name="Login Here" component={Login} />
      <Stack.Screen name="AdminMode" component={AdminMode} />
      </Stack.Navigator>
    </NavigationContainer>
  );
}





export default function App() {
  return (
    
      <AppContainer/>

    
    
  );
}



// import * as React from 'react';
// import AsyncStorage from '@react-native-community/async-storage';

// export default function App({ navigation }) {
//   const [state, dispatch] = React.useReducer(
//     (prevState, action) => {
//       switch (action.type) {
//         case 'RESTORE_TOKEN':
//           return {
//             ...prevState,
//             userToken: action.token,
//             isLoading: false,
//           };
//         case 'SIGN_IN':
//           return {
//             ...prevState,
//             isSignout: false,
//             userToken: action.token,
//           };
//         case 'SIGN_OUT':
//           return {
//             ...prevState,
//             isSignout: true,
//             userToken: null,
//           };
//       }
//     },
//     {
//       isLoading: true,
//       isSignout: false,
//       userToken: null,
//     }
//   );

//   React.useEffect(() => {
//     // Fetch the token from storage then navigate to our appropriate place
//     const bootstrapAsync = async () => {
//       let userToken;

//       try {
//         userToken = await AsyncStorage.getItem('userToken');
//       } catch (e) {
//         // Restoring token failed
//       }

//       // After restoring token, we may need to validate it in production apps

//       // This will switch to the App screen or Auth screen and this loading
//       // screen will be unmounted and thrown away.
//       dispatch({ type: 'RESTORE_TOKEN', token: userToken });
//     };

//     bootstrapAsync();
//   }, []);

//   const authContext = React.useMemo(
//     () => ({
//       signIn: async data => {
//         // In a production app, we need to send some data (usually username, password) to server and get a token
//         // We will also need to handle errors if sign in failed
//         // After getting token, we need to persist the token using `AsyncStorage`
//         // In the example, we'll use a dummy token

//         dispatch({ type: 'SIGN_IN', token: 'dummy-auth-token' });
//       },
//       signOut: () => dispatch({ type: 'SIGN_OUT' }),
//       signUp: async data => {
//         // In a production app, we need to send user data to server and get a token
//         // We will also need to handle errors if sign up failed
//         // After getting token, we need to persist the token using `AsyncStorage`
//         // In the example, we'll use a dummy token

//         dispatch({ type: 'SIGN_IN', token: 'dummy-auth-token' });
//       },
//     }),
//     []
//   );

//   return (
//     <AuthContext.Provider value={authContext}>
//       <Stack.Navigator>
//         {state.userToken == null ? (
//           <Stack.Screen name="SignIn" component={SignInScreen} />
//         ) : (
//           <Stack.Screen name="Home" component={HomeScreen} />
//         )}
//       </Stack.Navigator>
//     </AuthContext.Provider>
//   );
// }