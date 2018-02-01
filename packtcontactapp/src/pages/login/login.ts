import { HomePage } from './../home/home';
import { PacktService } from './../../providers/packt-service';
import { Component } from '@angular/core';
import { NavController, NavParams, Platform, ToastController, LoadingController } from 'ionic-angular';
import { FormBuilder,FormGroup,Validators } from '@angular/forms';


@Component({
  selector: 'page-login',
  templateUrl: 'login.html',
})
export class LoginPage {
  loginform:FormGroup;
  items: Array<Object>;
  //loginModel:any;
  loginModel: {username: string, password: string} = {
    username: '',
    password: ''
  };
  constructor(public navCtrl: NavController,
              public loadingCtrl: LoadingController,
              public toastCtrl: ToastController,
              private _dataService: PacktService,
              private fb: FormBuilder,
              private platform: Platform
              ) {
                this.loginform = this.fb.group({
                  username: [this.loginModel.username, [Validators.required]],
                  password: [this.loginModel.password, [Validators.required]]
    });
  }

  ionViewDidLoad() {
    console.log('ionViewDidLoad LoginPage');
  }

  onSubmit({value,valid}: {value: any,valid: boolean}) {

    //this.navCtrl.setRoot(HomePage);
    let loading  = this.loadingCtrl.create({
        content: 'Please wait...'
      });
      loading.present();
      this._dataService.login(value.username, value.password).subscribe((resp) => {
        loading.dismiss();
        //console.log(resp);
        if (resp === true) {
                      // login successful
                      this.navCtrl.setRoot(HomePage);
                  } else {
                      // login failed
                      // Unable to log in
                      loading.dismiss();
                      let toast = this.toastCtrl.create({
                        message: "Login Failed",
                        duration: 3000,
                        position: 'middle'
                      });
                      toast.present();
                  }

      }, (err) => {
        // Unable to log in
        loading.dismiss();
        let toast = this.toastCtrl.create({
          message: err,
          duration: 2000,
          position: 'middle'
        });
        toast.present();
      });
    }
    }

