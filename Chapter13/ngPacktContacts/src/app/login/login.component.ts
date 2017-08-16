import { PacktAPIsService } from './../packt-apis.service';
import { CredentialsModel } from './../credentials-model';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginform: FormGroup;
  loginModel: CredentialsModel = new CredentialsModel();

  constructor(public fb: FormBuilder, private router: Router, private _dataService: PacktAPIsService) {
    this.loginform = this.fb.group({
                  username: [this.loginModel.Username, [Validators.required]],
                  password: [this.loginModel.Password, [Validators.required]]
    });
  }

  onLogin({value, valid}: {value: any, valid: boolean}) {
    this._dataService.login(value.username, value.password).subscribe((resp) => {
        if (resp === true) {
              this.router.navigate(['contacts']);
                  } else {
                      alert('Login failed');
                  }

      }, (err) => {
        // Unable to log in
        console.log('Login Error');
      });
  }

  ngOnInit() {
  }

}
