import React, { Component } from 'react';


export class FetchData extends Component {
  static displayName = FetchData.name;

  constructor(props) {
    super(props);
    this.state = { forecasts: [], loading: true };
  }

  componentDidMount() {
    this.populateWeatherData();
  }

  static renderForecastsTable(forecasts) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        {/* <thead>
          <tr>
            <th>Date</th>
            <th>Temp. (C)</th>
            <th>Temp. (F)</th>
            <th>Summary</th>
          </tr>
        </thead> */}
        <tbody>
          {forecasts.map(forecast =>
              <tr key={forecast.id}>
              <td>{forecast.contactName}<br/>{forecast.birth}</td>
              <td>Ranking<br/>{forecast.stars}</td>
              <td>{(forecast.favorite) ? 1 : 0}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : FetchData.renderForecastsTable(this.state.forecasts);

    return (
      <div>
        <h1 id="tabelLabel" >Weather forecast</h1>
        <p>This component demonstrates fetching data from the server.</p>
        {contents}
      </div>
    );
  }

  async populateWeatherData() {
    //const response = await fetch('weatherforecast');
      const response = await fetch('AllReservations');
    const data = await response.json();
    this.setState({ forecasts: data, loading: false });
  }
}
