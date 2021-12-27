import React, { Component } from 'react';
import authService from '../../components/api-authorization/AuthorizeService';

export class EmployeeCreate extends Component {
  static displayName = EmployeeCreate.name;

  constructor(props) {
    super(props);
    this.state = { employeeTypes: [], fullName: '',birthdate: '',tin: '',typeId: 1, loading: false,loadingSave:false };
  }

  componentDidMount() {
    this.populateEmployeeTypeData();
  }

  handleChange(event) {
    this.setState({ [event.target.name] : event.target.value});
  }

  handleSubmit(e){
      e.preventDefault();
      if (this.state.fullName.trim() != '' && this.state.birthdate.trim() != '' && this.state.tin.trim() != '') {
          if (window.confirm("Are you sure you want to save?")) {
              this.saveEmployee();
          }
      } else {
          alert("Please fill in all fields");
      }
    }

  static renderEmployeeTypes(employeeTypes,parent) {
      return (
        <div>
    <form>
<div className='form-row'>
<div className='form-group col-md-6'>
  <label htmlFor='inputFullName4'>Full Name: *</label>
  <input type='text' className='form-control' id='inputFullName4' onChange={parent.handleChange.bind(parent)} name="fullName" value={parent.state.fullName} placeholder='Full Name' />
</div>
<div className='form-group col-md-6'>
  <label htmlFor='inputBirthdate4'>Birthdate: *</label>
  <input type='date' className='form-control' id='inputBirthdate4' onChange={parent.handleChange.bind(parent)} name="birthdate" value={parent.state.birthdate} placeholder='Birthdate' />
</div>
</div>
<div className="form-row">
<div className='form-group col-md-6'>
  <label htmlFor='inputTin4'>TIN: *</label>
  <input type='number' className='form-control' id='inputTin4' onChange={parent.handleChange.bind(parent)} value={parent.state.tin} name="tin" placeholder='TIN' />
</div>
<div className='form-group col-md-6'>
  <label htmlFor='inputEmployeeType4'>Employee Type: *</label>
  <select id='inputEmployeeType4' onChange={parent.handleChange.bind(parent)} value={parent.state.typeId} name="typeId" className='form-control'>
            {employeeTypes.map(employeeType =>
                <option value={employeeType.id}>{employeeType.typeName}</option>
            )}
        </select>
</div>
</div>
<button type="submit" onClick={parent.handleSubmit.bind(parent)} disabled={parent.state.loadingSave} className="btn btn-primary mr-2">{parent.state.loadingSave?"Loading...": "Save"}</button>
<button type="button" onClick={() => parent.props.history.push("/employees/index")} className="btn btn-primary">Back</button>
</form>
</div>
        
    );
  }

  render() {

    let contents = this.state.loading
    ? <p><em>Loading...</em></p>
    : EmployeeCreate.renderEmployeeTypes(this.state.employeeTypes,this);

    return (
        <div>
        <h1 id="tabelLabel" >Employee Create</h1>
        <p>All fields are required</p>
        {contents}
      </div>
    );
  }

  async saveEmployee() {
    this.setState({ loadingSave: true });
    const token = await authService.getAccessToken();
    const requestOptions = {
        method: 'POST',
        headers: !token ? {} : { 'Authorization': `Bearer ${token}`,'Content-Type': 'application/json' },
        body: JSON.stringify(this.state)
    };
    const response = await fetch('api/employees', requestOptions);

    if(response.status === 200){
        this.setState({ loadingSave: false });
        alert("Employee successfully saved");
        this.props.history.push("/employees/index");
    }
    else{
        alert("There was an error occured.");
    }
    }

    async populateEmployeeTypeData() {
        const token = await authService.getAccessToken();
        const requestOptions = {
            method: 'POST',
            headers: !token ? {} : { 'Authorization': `Bearer ${token}`, 'Content-Type': 'application/json' }
        };
        const response = await fetch('api/employees/employeetype', requestOptions);

        if (response.status === 200) {
            const data = await response.json();
            this.setState({ employeeTypes: data, loading: false });
        }
        else {
            alert("There was an error occured.");
            this.setState({ loading: false, loadingCalculate: false });
        }
    }

}
