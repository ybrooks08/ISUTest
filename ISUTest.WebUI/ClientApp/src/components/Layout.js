import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';

export class Layout extends Component {
  static displayName = Layout.name;

  render () {
    return (
      <div>
        <NavMenu />
        <div id="bar-ontop-content"></div>
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
