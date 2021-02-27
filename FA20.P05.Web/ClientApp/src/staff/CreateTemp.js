import React from "react";
import Button from "@material-ui/core/Button";
import CssBaseline from "@material-ui/core/CssBaseline";
import TextField from "@material-ui/core/TextField";
import * as R from 'ramda';

import Container from "@material-ui/core/Container";
import { withStyles } from "@material-ui/core/styles";
import Nav from "../Nav/Nav";

import MenuItem from "@material-ui/core/MenuItem";
import InputLabel from "@material-ui/core/InputLabel";

import FormControl from "@material-ui/core/FormControl";
import Select from "@material-ui/core/Select";

const styles = (theme) => ({
  root: {
    display: "flex",
    flexWrap: "wrap",
    width: "100%", // Fix IE 11 issue.
    marginTop: theme.spacing(1),
  },
  formControl: {
    margin: theme.spacing(2),
    minWidth: 120
  },
  paper: {
    marginTop: theme.spacing(8),
    display: "flex",
    flexDirection: "column",
    alignItems: "center",
  },
  color: {
    background: "#0d7377",
    color: "white",
    "&:hover": {
      background: "#ffa500",
      color: "white",
    },
  },
  submit: {
    margin: theme.spacing(3, 0, 2),
  },
});

class CreateTemp extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      schoolId: null,
      temperatureFahrenheit: null,
      islogin: false, 
      show: false,
    };

    this.handleClick = this.handleClick.bind(this);
    this.handleSchoolId = this.handleSchoolId.bind(this);
    this.handleTemperature = this.handleTemperature.bind(this);
    //this.toggleBarCode = debounce(this.toggleBarCode.bind(this),1000);
  }
  

  handleTemperature(event) {
    this.setState({ temperatureFahrenheit: parseInt(event.target.value) });
  }

  handleSchoolId(event) {
    this.setState({ schoolId: event.target.value });
  }

  handleClick (){

    let FormData = {
      schoolId: this.state.schoolId,
      temperatureFahrenheit: this.state.temperatureFahrenheit,
    };
    console.log(FormData);
    fetch(`https://selu383-fa20-p05-g02.azurewebsites.net/api/temperature-records`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(FormData),
    })
      .then((response) => {
        if (response.ok) {
          return response.json();
        } else {
          console.log("invalid input");
        }
      })
      .then((resData) => {
          console.log(resData);
          window.alert('temperature has been recorded')
          //this.toggleBarCode();
        // this.setState({ islogin: true });
      })
      .catch((error) => {
        console.log(error);
      });
     
    }

    

    // toggleBarCode() {
    //   this.setState({
    //     show: !this.state.show
    //   });
    // }



  render() {
    let { schoolId, temperatureFahrenheit } = this.state;
    const { classes } = this.props;
    
    const outerStyle = R.mergeAll([
      {
         backgroundColor: "#68b0ab",
         marginTop: "50px"
      }
    ]);

    return (
      <React.Fragment>
        <Nav />
        <Container component="main" maxWidth="xs">
          <CssBaseline />
          <div className={classes.paper}>
           <form className={classes.root}>
            <FormControl className={classes.formControl}>
                <InputLabel htmlFor="Role-simple">Schools</InputLabel>
                  <Select
                    name="Schools"
                    icon="exclamation-triangle"
                    group
                    type="text"
                    error="wrong"
                    success="right"
                    value={schoolId}
                    onChange={this.handleSchoolId}
                  >
                    <MenuItem value=""><em>None</em></MenuItem>
                    <MenuItem value={1} disabled>Hammond High Magnet School</MenuItem>
                    <MenuItem value={2}>Hammond Westside Montessori School</MenuItem>
                    <MenuItem value={3}>Hammond Eastside Magnet School</MenuItem>
                    </Select>
              </FormControl>
              <TextField
                name="temperatureFahrenheit"
                margin="normal"
                required
                fullWidth
                id="temp-record"
                label= "Temperature in Fahrenheit"
                autoComplete="off"
                type= "number"
                value={temperatureFahrenheit}
                autoFocus
                onChange={this.handleTemperature}
              />
              <Button
                className={("button", classes.color, classes.root)}
                type="submit"
                value="Submit"
                onClick={this.handleClick}
                style= {outerStyle}
              >

                  Submit Record
                
              </Button>
                <a style={{display: "table-cell"}} href="../imageDisplay" target="_blank">
                  Latest QR Code
                  </a>
            </form>
          </div>
          {/* <ToggleDisplay show={this.state.show}>
          <img src = {require('../uploads/20201111_020306.jpg')} />
        </ToggleDisplay> */}

        </Container>
      </React.Fragment>
    );
  }
}

export default withStyles(styles)(CreateTemp);