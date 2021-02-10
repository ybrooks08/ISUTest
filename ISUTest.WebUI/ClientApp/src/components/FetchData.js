import React, { Component } from 'react';
import BootstrapTable from 'react-bootstrap-table-next';
import { Link } from 'react-router-dom';
import paginationFactory from 'react-bootstrap-table2-paginator';
import Moment from 'react-moment';
//import 'react-bootstrap-table-next/dist/react-bootstrap-table2.min.css';
import 'react-bootstrap-table2-paginator/dist/react-bootstrap-table2-paginator.min.css';
import '../components/lisStyle.css';

const optionsPag = {
  paginationSize: 8,
  hideSizePerPage: true,
  pageStartIndex: 0,
  // alwaysShowAllBtns: true, // Always show next and previous button
  // withFirstAndLast: false, // Hide the going to First and Last page button
  // hideSizePerPage: true, // Hide the sizePerPage dropdown always
  // hidePageListOnlyOnePage: true, // Hide the pagination list when only one page
  //firstPageText: 'First',
  prePageText: '<',
  nextPageText: '>',
  lastPageText: 'Last',
  firstPageTitle: 'First page',
  sizePerPageList: [{
    text: '5', value: 8
  }, {
    text: '10', value: 20
  }]
};

export class FetchData extends Component {
  static displayName = FetchData.name;

  constructor(props) {
    super(props);
    this.state = 
    { 
      reservations: [], 
      loading: true,
      columns: [{
        dataField: 'contactName',
        text: 'Contact Name'
      }, {
        dataField: 'modifiedDate',
        text: 'ModifiedDate',
        formatter:this.dateFormatter

      }, {
        dataField: 'stars',
        text: 'Stars',
      },
      {
        id: "edit-column",
        dataField: "edit",
        text: "Edit",
        formatter: this.linkEdit,
        style: {textAlign: "right" }
        
      }]
    };

    this.onEditChanged.bind(this);
  }

  componentDidMount() {
    this.populateReservationData();    
  }

  static renderReservationsTable(reservations, columns) {
    return (
      <BootstrapTable keyField='id' data={ reservations } columns={ columns } pagination={ paginationFactory(optionsPag) } headerClasses="header-class" />
    );
  }

  onEditChanged(e) {
    console.log(e);
  }

  dateFormatter(cell) {
    if (!cell) {
          return "";
    }
    //return `${moment(cell).format("DD-MM-YYYY")? moment(cell).format("DD-MM-YYYY"):moment(cell).format("DD-MM-YYYY") }`;
    return <Moment format="ddd MMM D LT">{cell}</Moment>
}

  linkEdit = (cell, row, rowIndex, formatExtraData) => {
    return (
      <Link
        className = "link-as-button"
        to={{
          pathname: "/add-reservation",
          data: row // your data array of objects
        }}
      >Edit</Link>
    );
  };

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : FetchData.renderReservationsTable(this.state.reservations, this.state.columns);

    return (
      <div className="showed-content">       
        {contents}
      </div>
    );
  }

  async populateReservationData() {
    const response = await fetch('AllReservations');
    const data = await response.json();
    console.log(data);
    this.setState({ reservations: data, loading: false });
  }
}
