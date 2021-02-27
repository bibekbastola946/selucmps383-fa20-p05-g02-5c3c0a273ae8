import React from "react";
import { NavLink , Redirect } from "react-router-dom";
import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import Typography from "@material-ui/core/Typography";
import { withStyles } from "@material-ui/core/styles";

const styles = (theme) => ({
  nav: {  
    display: "inline-flex",
  },
  appBar: {
    borderBottom: `1px solid ${theme.palette.divider}`,
    background: "#0d7377",
  },
  mini: {
    height: 32,
  },
  icon: {
    marginRight: theme.spacing(2),
  },
  heroContent: {
    backgroundColor: theme.palette.background.paper,
    padding: theme.spacing(8, 0, 6),
  },
  link: {
    margin: theme.spacing(1, 1.5),
    color: "white",
    textDecoration: "none",
    fontFamily: "Raleway",
    fontSize: "20px",
    "&:hover": {
      textDecoration: "none",
      color: "#e4e4e4",
    },
  },
  toolbar: {
    flexWrap: "wrap",
  },
  toolbarTitle: {
    flexGrow: 1,
  },
});

class Nav extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      reDirect: false,
    };
    this.handleLogout = this.handleLogout.bind(this);
  }

  handleLogout() {
    sessionStorage.clear("userData");
    return <Redirect to="/login" />;
  }

  render() {
    const { classes } = this.props;
    const userData = JSON.parse(sessionStorage.getItem("userData"));

    return (
        <React.Fragment>
          <AppBar
            position="static"
            color="primary"
            elevation={0}
            className={classes.appBar}
          >
            <Toolbar className={classes.toolbar}> 
              <Typography
                variant="h6"
                color="inherit"
                noWrap
                className={classes.toolbarTitle}
              >
                <NavLink  to="/" className={classes.link}>
                  Covid 19 Record
                </NavLink >
                <NavLink  to="/Public" className={classes.link}>
                  Public Covid Records
                </NavLink >
                
              </Typography>
                <nav className= {classes.nav}> 
                <NavLink to="/Home" activeClassName='is-active' className={classes.link}>
                  Home
                </NavLink>
                
                {(userData) ? 
                  (userData.staffId == null) ? (
                    <NavLink to= '/register' activeClassName='is-active' className= {classes.link}>Create User</NavLink >
                  ) : <div/>
                  :
                <div/>
                }
                 {(userData) ? (userData.staffId === 2) ? (
                  <NavLink  to= '/createTemperature' className= {classes.link}>Record Temperature</NavLink>
                ): <div/>
                :<div/>
                }
                
                {userData == null ? (
                  <NavLink  to="/login" className={classes.link}>
                    {" "}
                    Login
                  </NavLink>
                ) : (
                  <NavLink 
                    to="/login"
                    className={classes.link}
                    onClick={this.handleLogout}
                  >
                    Logout
                  </NavLink>
                )}
              </nav>
            </Toolbar>
          </AppBar>
        </React.Fragment>
      );
   }
}

export default withStyles(styles)(Nav);