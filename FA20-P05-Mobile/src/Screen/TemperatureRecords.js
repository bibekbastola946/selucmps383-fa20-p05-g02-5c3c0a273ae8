
import React, { PureComponent } from 'react';
import { View, Image, FlatList, TouchableOpacity, Text, ActivityIndicator, Button } from 'react-native';
import { API_ENDPOINT } from '../constants/constant';
import { Ionicons } from '@expo/vector-icons';
import { StyleSheet } from 'react-native';
import { DrawerActions } from 'react-navigation-drawer';


var globals = ['abc'];
async function getName(id) {

    const schoolApi = await fetch(`${API_ENDPOINT}/api/schools/2`);
    const schoolData = await schoolApi.json();
    //record = schoolData.name;
    //console.log(record)
    return schoolData;
}
export default class TemperatureRecords extends PureComponent {

    state = {
        TemperatureRecords: [],
        loading: true
    }

    toggleDrawer = () => {
        this.props.navigation.dispatch(DrawerActions.toggleDrawer())
      }
    async componentDidMount() {
        try {
            const temperatureApi = await fetch(`${API_ENDPOINT}/api/TemperatureRecordDtoes`);
            const tempData = await temperatureApi.json();
            const schoolApi = await fetch(`${API_ENDPOINT}/api/schools/active`);
            const schoolData = await schoolApi.json();
            
            console.log(schoolData)
            console.log(tempData)
            //    const res = a2.map(x => Object.assign(x, a1.find(y => y.id == x.schoolId)));
            
            const res = tempData.map(x => Object.assign(x, schoolData.find(y => y.id == x.schoolId)));
            //const res = _(schoolData).concat(tempData).groupBy('id').map(_.spread(_.assign)).value();
            console.log(res)
            
            
            
            //console.log(this.state.SchoolRecord)
            //var schoolData;
            // console.log(tempData)
            // console.log(schoolData)
            //console.log(schoolData),

            this.setState({ TemperatureRecords: res, loading: false });
            //console.log(this.state.TemperatureRecords)


        } catch (err) {
            console.log("Error fetching data-----------", err);
        }
    }
    // callSchool() {

    //     const schoolApi = await fetch(`${API_ENDPOINT}/api/schools/2`);
    //     const schoolData = await schoolApi.json();
    //     console.log(schoolData)
    // }
    renderItem(data) {

        try {
            // getName(1).then((value) => {
            //     globals.push(value.name);
            // });
            
            // const a = globals
            // //console.log(a[2])
            
            return (
                
                <TouchableOpacity >
                    <View  >
                        {/* Use same school id to make api call and display school */}
                        <ul><b>Recorded Temperature:</b> {data.item.temperatureKelvin}{'\u00b0'} fahrenheit <b>Location:</b>{data.item.name}</ul>
                    </View>
                </TouchableOpacity>
            )
        }
        catch (err) {
            console.log("found error")
        }
    }
    render() {
        //console.log(this.TemperatureRecords)

        const { TemperatureRecords, loading } = this.state;
        if (!loading) {
            return (<View style = {styles.backgroundColor}>
      <Ionicons style={styles.back} name="ios-arrow-back" size={40} onPress = {()=> this.toggleDrawer()}/>
      
            <FlatList
                data={TemperatureRecords}
                renderItem={this.renderItem}
                keyExtractor={(item) => item.id}
                
            />
            </View>

            )
        } else {
            return <ActivityIndicator />
        }
    }
}

const styles = StyleSheet.create({
    container: {
      flex: 1,
      flexDirection: 'column',
    },
    
    margin: {
      marginTop: 10
    },
    backgroundColor: {
        flex: 1,
    resizeMode: 'cover',
    justifyContent: 'center',
      backgroundColor:'#dffad2',
      
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
  
  
  
  
  


// const TemperatureRecords = props => {



//   const [temp, setTemperature] = useState();
//   //console.log(temp);



//   async function fetchTemperature() {
//     const temperature = await ajax.Temperature();
//     setTemperature(temperature.data);

//   }

//   return (

    //   <ul>
    //   {props.items.map((user) => {
    //     return (
    //       <TemperatureDetails
    //         key={user.id}
    //         id={user.id}
    //         temperature={user.temperatureKelvin}
    //       />

    //     );
    //   })}
    // </ul>
//     <View style={{ flex: 1, alignItems: 'center', justifyContent: 'center' }}>
//       <View>
//         <Text>List All TemperatureRecords</Text>

//         <Button
//           title="Display Temperature Records"
//           onPress={() => fetchTemperature()}
//         >ab</Button>

//       </View>

//       <View>

//               {/* {
//               globalVar.map((prop, key) => {

//               })
//           }
//          */}
//       </View>



//     </View>





//   );
// }
// const styles = StyleSheet.create({
//   myCard: {
//     flex: 1
//   },
//   Button: {
//     padding: 5
//   },
//   logo: {
//     height: 300,
//     width: 400,
//     margin: 20,
//     marginTop: -200,
//     alignContent: "center"
//   },
//   inputStyle: {
//     margin: 10
//   },
//   submitButton: {
//     backgroundColor: "green",
//     margin: 80,
//     marginTop: 20
//   }
// });
// export default TemperatureRecords; 
