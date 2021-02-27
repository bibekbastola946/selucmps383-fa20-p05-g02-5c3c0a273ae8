
import React, { useState } from "react";
import { View, Text, StyleSheet } from "react-native";
import { Card, Title, Paragraph } from "react-native-paper";
import TemperatureDetails from "../components/TemperatureDetails"

const ScanedTemperature = (props) => {
  console.log('this is props',props);
  // if (props.items.length === 0) {
  //   return (
  //     <div className="center">
  //       <Card>
  //         <h2>No Temperature found</h2>
  //       </Card>
  //     </div>
  //   );
  // }
  return (
    <View>
      {props.route.params.items.map((item) => {
        return (
          <TemperatureDetails
            key={item.id}
            id={item.id}
            schoolId={item.schoolId}
            temperature={item.temperatureKelvin}
          />
        );
      })}
      </View>
  );
};
export default ScanedTemperature;