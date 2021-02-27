import React, { useState } from "react";
import { View, Text, StyleSheet } from "react-native";
import { Card, Title, Paragraph } from "react-native-paper";


const TemperatureDetails = (props) => {
console.log(props);
    return (
      <View>
        <Card>
        <Text>{props.temperature}</Text>
        </Card>
          
      </View>
    );
  };
  
  export default TemperatureDetails;