import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from "@angular/router";
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import configurl from '../../../assets/config/config.json'

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html'
})
export class RegisterComponent {
  invalidUser?: boolean;

  url = configurl.apiServer.url + '/api/users/';

  constructor(private router: Router, private http: HttpClient, private toastr: ToastrService) { }

  public register = (form: NgForm) => {
    const newCredentials = JSON.stringify(form.value);
    this.http.post(this.url +"CreateUser", newCredentials, {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    }).subscribe(response => {
      const user = (<any>response);
      this.toastr.success("User Register successfully");
      setTimeout(function() {}, 500);
      this.router.navigate(["/"]);
    }, err => {
      this.toastr.error("User Already Exist or Register error");
    });
  }

  password: string = '';
  confirm_password: string = '';
  myStyles = { color: 'red' };
  htmlStr: string = '';
  validated: boolean = false;
  check(){
    if (this.password == this.confirm_password) {
      this.myStyles.color = 'green';
      this.htmlStr = ' Matching';
      this.validated = true;
    } else {
      this.myStyles.color = 'red';
      this.htmlStr = ' Not Matching';
      this.validated = false;
    }
  }
}
