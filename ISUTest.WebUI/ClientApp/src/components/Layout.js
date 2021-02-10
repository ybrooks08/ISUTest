import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';
import { Link } from 'react-router-dom';

export class Layout extends Component {
  static displayName = Layout.name;
  state = { showingList: true };
  
  render () {
    return (
      <div>
        <NavMenu />
        <div id="bar-ontop-content">
          <div id="top-res-list">
            <div className="row align-items-center">
              { 
                this.state.showingList ? <div className="col title-top-bar">Reservation List</div> 
                    : <div className="col create-title-top">Create Reservation</div>
              }
              <div className="col des-top-bar">
                Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. 
              </div>
              <div className="col button-top-bar-reg">
              <Link
                  className = "button-top-bar"
                  to={{
                    pathname: "/add-reservation"
                  }}
                >Create Reservation</Link>
              </div>
            </div>           
          </div>
        </div>
        <main id="main-content">
        <Container>
          {this.props.children}
        </Container>
        </main>
        <footer id="footer-reg"></footer>
      </div>
    );
  }
}
