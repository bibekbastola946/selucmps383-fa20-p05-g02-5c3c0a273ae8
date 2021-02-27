import React, { useState } from "react";
import { Text, View, Button } from 'react-native';
import { useNavigation } from '@react-navigation/native';
import { NavigationContainer } from '@react-navigation/native';
import { createStackNavigator } from '@react-navigation/stack';
import { and } from 'react-native-reanimated';
import ajax from "../ajax";
import { API_ENDPOINT } from "../constants/constant";

export default function Details({ navigation }) {
  const [schools, setSchool] = useState();
  const [pressed, setPressed] = useState(false);
  function ListItem(props) {
    
    return <View>
      <Text>{props.id}</Text>
  <Text>{props.active}</Text>
  <Text>{props.schoolPopulation}</Text>
    </View>
  }
  function SchoolList(props) {
    const numbers = props.schools;
    const listItems = numbers.map((number) =>
      
      <ListItem key={number.id} id={number.id} active={number.active} schoolPopulation={number.schoolPopulation} />
    );
    return (
      <View>
        {listItems}
        </View>
    );
  }
  
  async function fetchSchools() {
    const schoolss = await ajax.Schools();
    
    setSchool(schoolss.data);
    setPressed(true);
    console.log(schools)
  }
    // function fetchTemperature() {
    //     return fetch(`${API_ENDPOINT}/api/TemperatureRecordDtoes/active`)
    //       .then((response) => response.json())
    //       .then(data => console.log(data));
          
    //   }
      
      
    return (
      <View style={{ flex: 1, alignItems: 'center', justifyContent: 'center' }}>
        <Text>List All TemperatureRecords</Text>
        <Button
          title="Display Temperature Records"
          onPress={() => fetchSchools()}
        />
        { pressed && <SchoolList schools={schools} />}
      </View>
    );
  }