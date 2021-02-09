import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import { AddReservation } from './components/AddReservation';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={FetchData} />
        <Route exact path='/home' component={Home} />
        <Route path='/counter' component={Counter} />
        <Route path='/reservation-list' component={FetchData} />
        <Route path='/add-reservation' component={AddReservation} />
      </Layout>
    );
  }
}
