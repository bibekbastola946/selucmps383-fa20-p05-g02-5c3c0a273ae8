import React from "react";
import Button from "@material-ui/core/Button";
import CssBaseline from "@material-ui/core/CssBaseline";
import TextField from "@material-ui/core/TextField";
import Container from "@material-ui/core/Container";
import * as R from 'ramda';
import Nav from "../Nav/Nav";
import { withStyles } from "@material-ui/core/styles";
import MenuItem from "@material-ui/core/MenuItem";
import InputLabel from "@material-ui/core/InputLabel";

import FormControl from "@material-ui/core/FormControl";
import Select from "@material-ui/core/Select";

import history from '../history';



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
  logo: {
    height: "175px",
  },
  color: {
    background: "#39311d",
    color: "#0d7377",
    "&:hover": {
      background: "#ffa500",
      color: "white",
    },
  },
  submit: {
    margin: theme.spacing(3, 0, 2),
  },
});

class Register extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      username: "",
      password: "",
      role:"",
      staffId: null,
      isSignup: false,
    };
    this.handleChange = this.handleChange.bind(this);
    this.handlepassword = this.handlepassword.bind(this);
    this.handleRole = this.handleRole.bind(this);
    this.handlestaffId = this.handlestaffId.bind(this);
    this.handleClick = this.handleClick.bind(this);
  }

  handleChange(event) {
    this.setState({ username: event.target.value });
  }

  handlepassword(event) {
    this.setState({ password: event.target.value });
  }

  handleRole(event) {
    this.setState({ role: event.target.value });
  }

  handlestaffId(event) {
    console.log(event, "targetted Value");
    if (event.target.value === 1) {
      this.setState({role:"Principal"})
    } else if (event.target.value === 2) {
      this.setState({role:"Staff"})
    }
    this.setState({ staffId: event.target.value });
  }

  handleSubmit(event) {
    event.preventDefault();
  }

  handleClick() {
    let FormData = {
      username: this.state.username,
      password: this.state.password,
      role: this.state.role,
      staffId: this.state.staffId
    };
    console.log(FormData, "data")
    fetch(`https://selu383-fa20-p05-g02.azurewebsites.net/api/users`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(FormData),
    })
    .then((response) => {
        if (response.ok) { 
          return response.json();
        } 
      })
      .then((resData) => {
        this.setState({ isSignup: true });
        window.alert( " New User Created ")
        history.push('/Home')
      })
      .catch((error) => {
        console.log(error)
        
      });
  }

  render() {
    const { classes } = this.props;
    let { username, password, staffId} = this.state;
    // if (this.state.isSignup) {
    //   return <Redirect to="/login" />;
    // }
    const outerStyle = R.mergeAll([
        {backgroundColor: "#68b0ab"}
    ]);

    return (
      <React.Fragment>
        <Nav />
        <Container component="main" maxWidth="xs">
          <CssBaseline />
          <div className={classes.paper}>

            <form className={classes.root} autoComplete="off">
              <TextField
                name="Username"
                margin="normal"
                required
                fullWidth
                id="Username"
                label="Username"
                autoComplete="off"
                value={username}
                autoFocus
                onChange={this.handleChange}
              />
              <TextField
                margin="normal"
                required
                fullWidth
                name="Password"
                label="Password"
                type="Password"
                id="Password"
                autoComplete="off"
                value={password}
                onChange={this.handlepassword}
              />
              <FormControl className={classes.formControl}>
                <InputLabel htmlFor="Role-simple">Role</InputLabel>
                  <Select
                    name="Roles"
                    icon="exclamation-triangle"
                    group
                    type="text"
                    error="wrong"
                    success="right"
                    value={staffId}
                    onChange={this.handlestaffId}
                  >
                    <MenuItem value=""><em>None</em></MenuItem>
                    <MenuItem value={1}>Principal</MenuItem>
                    <MenuItem value={2}>Staff</MenuItem>
                    </Select>
              </FormControl>
              <Button
                className={("button", classes.color, classes.root)}
                type="submit"
                value="Submit"
                onClick={this.handleClick}
                style= {outerStyle}
              >
                Register
              </Button>
            </form>
          </div>
        </Container>
      </React.Fragment>
    );
  }
}

export default withStyles(styles)(Register);