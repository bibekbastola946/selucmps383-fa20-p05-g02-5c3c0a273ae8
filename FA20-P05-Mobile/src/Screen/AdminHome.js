// import React, { useState } from "react";
// import { StyleSheet, View, Image, Alert, Picker} from "react-native";
// import Logo from "../Screen/logo.png";
// import { TextInput, Button } from "react-native-paper";
// import { DrawerActions } from 'react-navigation-drawer';
// import { Ionicons } from '@expo/vector-icons';
// import { API_ENDPOINT } from "../constants/constant";


// const AdminHome = ({navigation}) => {
    
//   const getUserInfo = async () => {
//     var schoolId = await AsyncStorage.getItem("LoggedInUser");
//     let parsed = JSON.parse(schoolId);
//     let userId = parsed.staffId;
//     setId(JSON.parse(schoolId));

//     fetch(API_ENDPOINT + "/api/staff/" + userId, {
//       credentials: "include",
//     })
//       .then((response) => {
//         return response.json();
//       })
//       .then((res) => setData(res))
//       .catch((error) => alert(error));
//   };

  
  
//     const [temperatureFahrenheit, setTemperature] = useState();
//     const [schoolId, setSelectedValue] = useState();
    
//     const submitData = async () => {
//       getUserInfo();
//       fetch(`${API_ENDPOINT}/api/temperature-records`, {
//         method: "POST",
//         headers: {
//           Accept: "application/json",
//           "Content-Type": "application/json",
//           'Authorization': `PrincipalStaff`
//         },
//         body: JSON.stringify({
//           schoolId,
//           temperatureFahrenheit
//         })
//       })
//         .then(res => {
//           if (res.ok) {
//             navigation.navigate('QrPage')
//             return res.json();
            
//           } else {
//             Alert.alert("Invalid");
//             window.stop();
//           }
//         })
//         .catch(error => {  
//           console.log("Problem with fetch", error);
//         });
//     };
//   return (
//     <View style={styles.myCard}>
      

//       <View>
//       <Ionicons style={styles.back} name="ios-arrow-back" size={40} />
//       <Picker
//         selectedValue={selectedValue}
//         style={{ height: 50, width: 150 }}
//         onValueChange={(schoolId, itemIndex) => setSelectedValue(schoolId)}
//       >
//           <Picker.Item label="Hammond High Magnet School" value="1" />
//           <Picker.Item label="Hammond Westside Montessori School" value="2" />
//           <Picker.Item label="Hammond Eastside Magnet School" value="3" />
//         </Picker>
        
//         <TextInput
//           label="Temperature Recorded"
//           style={styles.inputStyle}
//           theme={theme}
//           value={temperatureFahrenheit}
//           mode="outlined"
//           secureTextEntry={true}
//           onChangeText={text => setTemperature(text)}
//         />
//         <Button
//           mode="contained"
//           style={styles.submitButton}
//           onPress={() => submitData()}
//         >
//           Submit Record
//         </Button>
//       </View>
//     </View>
//   );
// };
// const theme = {
//   colors: {
//     primary: "green"
//   }
// };
// const styles = StyleSheet.create({
//   myCard: {
//     flex: 1,
//     resizeMode: 'cover',

//   backgroundColor:'#dffad2',
//   },
  
//   inputStyle: {
//     margin: 10
//   },
//   submitButton: {
//     backgroundColor: "green",
//     margin: 80,
//     marginTop: 20
//   },
//   back: {
//     margin: 10,
//     marginLeft:20,
   
//     top: 0,
//     left: 0,
//     width: 25,
//     height: 25,
//     color: "tomato"
//   }
  
// });

// export default AdminHome;
