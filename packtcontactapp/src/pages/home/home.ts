import { PacktService } from './../../providers/packt-service';
import { Contacts } from './../../providers/contacts.class';
import { Component } from '@angular/core';
import {  FormGroup,  FormBuilder,  Validators} from '@angular/forms';
import { NavController } from 'ionic-angular';

@Component({
  selector: 'page-home',
  templateUrl: 'home.html'
})
export class HomePage {
  title = 'Ionic 3 & ASP.NET Core';
  recordsExists: boolean = false;
  formAddEdit:boolean = false;
  contactModel:Contacts;
  contactForm: FormGroup;
  public values: any[];

  constructor(public navCtrl: NavController, private _dataService: PacktService, private fb: FormBuilder) {

  }
  ionViewDidLoad() {
    this.LoadAllContacts();
  }

  LoadAllContacts(){
    this._dataService
      .GetAll()
      .subscribe(data => {
          if (data.length > 0) {
            this.values = data;
            this.recordsExists = true;
            console.log(data);
          }
        },
        error => {
          console.log(error.status);
        },
        () => console.log('complete'));
  }

  createContact()
  {
    this.recordsExists = false;
    this.contactModel = new Contacts();
    this.setupcontactForm();
    this.formAddEdit = true;
  }

  onSubmit({ value, valid }: { value: any, valid: boolean }) {
    console.log(value, valid);
    let values = value as Contacts;

    if(this.contactModel.id === 0 || this.contactModel.id === undefined){
      //Insert
      values.id =  Math.floor((Math.random() * 100) + 1);
      this.AddContacts(values);
    }
    else{
      values.id = this.contactModel.id;
      this.UpdateContacts(values);
    }
  }

  AddContacts(contactsObj: Contacts){
    this._dataService
      .addContacts(contactsObj)
      .subscribe(data => {
            console.log(data);
            this.recordsExists = true;
            this.formAddEdit = false;
            this.LoadAllContacts();
        },
        error => {
          console.log(error.status)
        },
        () => console.log('Added Successfully'));
  }

  UpdateContacts(contactsObj: Contacts){
    this._dataService
      .UpdateContacts(contactsObj, contactsObj.id)
      .subscribe(data => {
            console.log(data);
            this.recordsExists = true;
            this.formAddEdit = false;
            this.LoadAllContacts();
        },
        error => {
          console.log(error.status);
        },
        () => console.log('Updated Successfully'));
  }

  setupcontactForm() {
    this.contactForm = this.fb.group({
      firstname: [this.contactModel.firstName, [Validators.required, Validators.minLength(5), Validators.maxLength(100)]],
      lastname: [this.contactModel.lastName, [Validators.required, Validators.minLength(5), Validators.maxLength(100)]],
      email: [this.contactModel.email, [Validators.required]]
    });
  }

  editContact(contactItem) {
    console.log('test');

    this.formAddEdit = true;
    this.recordsExists = false;
    this.contactModel = contactItem;
    this.setupcontactForm();
  }

  cancelForm(){
    this.LoadAllContacts();
    this.formAddEdit = false;
  }

  deleteContact(contactItem) {
    console.log('In Delete' + contactItem);
    this._dataService
      .DeleteContacts(this.contactModel.id)
      .subscribe(data => {
            this.LoadAllContacts();
        },
        error => {
          console.log(error.status);
        },
        () => console.log('Deleted Successfully'));
  }

}


