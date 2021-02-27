import axios from "axios";
import { API_ENDPOINT } from "./constants/constant";

export default {
    async Schools() {
      try {
        console.log("a");
        const result = await axios.get(`${API_ENDPOINT}/schools/active`);
        return result;
      } catch (e) {
        console.error(e);
      }
    },
    
    async Temperature() {
      try {
        const result = await axios.get(`${API_ENDPOINT}/schools/active`);
        return result;
      } catch (e) {
        console.error(e);
      }
    },
    async TemperatureAbove100() {
      try {
        const result = await axios.get(`${API_ENDPOINT}/api/TemperatureRecordDtoes/active`);
        return result.data;
      } catch (e) {
        console.error(e);
      }
    },
    
  };
  