import React, { Component } from 'react';
import { Button, Col, Form, FormGroup, Input, Row } from 'reactstrap';
//This import coul be done at app.js to use it in all project, in this case I put it here to avoid load it always.
import { Editor } from 'react-draft-wysiwyg';
import { EditorState, ContentState, convertFromHTML } from 'draft-js';
import DatePicker from "react-datepicker";
import Select from 'react-select';
import { request, POST, CREATE_RESERVATION_URL, UPDATE_RESERVATION_URL, launchToast } from '../utils/Utils';
import 'react-draft-wysiwyg/dist/react-draft-wysiwyg.css';
import "react-datepicker/dist/react-datepicker.css";
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

export class AddReservation extends Component {
  static displayName = AddReservation.name;

  constructor(props) {
    super(props);
    this.state = 
    { 
      currentCount: 0,      
      contacts: [],
      contactTypes: [],
      //reservation data
      contactId: "",
      description: "",
      contactName: "",
      phoneNumber: "",
      birth: "",
      contactTypeId: "",
      editing: false,       
      editorState: EditorState.createEmpty(), 
      //to disable component on edit
      disable: false  
    };

  }

  componentDidMount() { 
    console.info(this.state.showingList);   
    this.populateContactsData();
    this.populateContactTypesData();
    if(this.props.location.data != null){
      this.setState({ editing: true });
      this.setState({ disable: true });
      var reservation = this.props.location.data;
      this.setState({ contactId: reservation.contactId });
      this.setState({ contactName: reservation.contactName });
      this.setState({ phoneNumber: reservation.phoneNumber });
      this.setState({ birth:  new Date(reservation.birth) });
      this.setState({ contactTypeId: reservation.contactTypeId });
      //Creating an editor state to pass it in right way 
      var editorValue = EditorState.createWithContent(
        ContentState.createFromBlockArray(
          convertFromHTML(reservation.description)
        )
      );
      this.setState({ editorState: editorValue });
    }
  }

  //Setting reservation fields values
  setBirth = (v) => {
    console.info(typeof(v));
    this.setState({ birth: v })
  }
  setContactId = (v) => {
    this.setState({ contactId: v })
  }
  setDescription = (v) => {
    this.setState({ description: v.target.value })
  }
  onEditorStateChange = (editorState) => {
      this.setState({
        editorState
    });
  }
  setContactName = (v) => {
    var contact = null;
    var iterator = 0;
    while(iterator < this.state.contacts.length && contact == null){ 
      if(v.target.value.toLowerCase() === this.state.contacts[iterator].name.toLowerCase()){
        contact = this.state.contacts[iterator];
      }
      iterator++;        
    }
    if(contact!=null){
      this.setState({ phoneNumber: contact.phoneNumber });
      this.setState({ birth:  new Date(contact.birthDate) });
      this.setState({ contactTypeId: contact.contactTypeId });
    }
    else{
      this.setState({ phoneNumber: "" });
    }    
    this.setState({ contactName: v.target.value });
    //I could use this way
    /*var contact = this.state.contacts.filter(x => x.name.toLowerCase() == v.target.value.toLowerCase());    
    this.setState({ contactName: v.target.value });
    if(contact.length > 0){
      console.info("contact");
      console.info(contact);
      this.setState({ phoneNumber: contact.phoneNumber });
    }*/      
  } 
  setPhoneNumber = (v) => {
    this.setState({ phoneNumber: v.target.value })
  }
  
  SetContactTypeId = (v) => {
    this.setState({ contactTypeId: v ? v.id : "" });
  }

  handleContactTypeChange = cTypeSelected => {
    this.setState({ cTypeSelected });    
    this.SetContactTypeId(cTypeSelected);  
  }

  async populateContactsData() {
    const response = await fetch('AllContacts');
    const data = await response.json();
    this.setState({ contacts: data, loading: false });
  }

  async populateContactTypesData() {
    const response = await fetch('AllContactTypes');
    const data = await response.json();
    this.setState({ contactTypes: data, loading: false });
  }

  //Another way to declare functions, this is a way used mostly when we use hooks instead class
  //I used the most the other way because I'm using classes way instead of hooks
  handleSubmit = (evt) => {
    evt.preventDefault();
    //alert(`Submitting Name ${name}`);
    if(!this.state.editing)
    {
      request(POST, CREATE_RESERVATION_URL, JSON.stringify({
      contactId: null,
      description: this.state.editorState.getCurrentContent().getPlainText(),
      contactName: this.state.contactName,
      phoneNumber: this.state.phoneNumber,
      birth: this.state.birth,
      contactTypeId: this.state.contactTypeId})
      , null,
      (code, body) => {
        launchToast("error", body.message);
        this.setState({loading: true});
      },
      (body) => {
        if(body.success) {          
          launchToast("success", body.message);
          this.setState({loading: false});
        }
        else {
          toast.error(body.message, {
            position: "top-center",
            autoClose: 5000,
            hideProgressBar: false,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
            progress: undefined,
            });
          launchToast("error", body.message);
          this.setState({loading: false})
        }
      });
    }
    else{
      request(POST, UPDATE_RESERVATION_URL, JSON.stringify({
        contactId: this.state.contactId,
        description: this.state.editorState.getCurrentContent().getPlainText(),
        contactName: this.state.contactName,
        phoneNumber: this.state.phoneNumber,
        birth: this.state.birth,
        contactTypeId: this.state.contactTypeId})
        , null,
        (code, body) => {
          launchToast("error", body.message);
          this.setState({loading: true});
        },
        (body) => {
          if(body.success) {          
            launchToast("success", body.message);
            this.setState({loading: false});
          }
          else {
            toast.error(body.message, {
              position: "top-center",
              autoClose: 5000,
              hideProgressBar: false,
              closeOnClick: true,
              pauseOnHover: true,
              draggable: true,
              progress: undefined,
              });
            launchToast("error", body.message);
            this.setState({loading: false})
          }
        });
    }
  }
 
  render() {    
    return (
      <Form onSubmit={this.handleSubmit}>
      <Row form>
        <Col md="3" className="form-group">
          <Input
            id="feContactName"
            type="text"
            placeholder="Contact Name ..."
            value={this.state.contactName} 
            onChange={this.setContactName}
            required 
            disabled = {(this.state.disable)? "disabled" : ""}
          />
        </Col>
        <Col md="3">          
          <Select
            ///value={this.cTypeSelected}
            value={this.state.contactTypes.find(op => {
              return op.id === this.state.contactTypeId
            })}
            getOptionLabel={option => option.contactType}
            getOptionValue={option => option.id}
            onChange={this.handleContactTypeChange}
            options={this.state.contactTypes}
            placeholder="Contact Type"
            isDisabled = {this.state.disable}
          />
        </Col>
        <Col md="3">
          <Input
            id="text"
            type="text"
            placeholder="Phone:"
            value={this.state.phoneNumber} 
            onChange={this.setPhoneNumber}
            required
            disabled = {(this.state.disable)? "disabled" : ""} 
          />
        </Col>
        <Col md="3">
          <DatePicker
            className="form-control" 
            placeholderText="Birth Date" 
            selected={this.state.birth} 
            onChange={date => this.setBirth(date)} 
            required
            disabled = {(this.state.disable)? "disabled" : ""}
           />
        </Col>
      </Row>
      <FormGroup>
        <Editor
          wrapperClassName="wrapper"
          editorClassName="editor"
          toolbarClassName="toolbar"
          editorState={this.state.editorState}
          onEditorStateChange={this.onEditorStateChange}
        />
      </FormGroup>     
      <Button type="submit">Save</Button>
    </Form>
        
    );
  }
}
