import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, } from '@angular/forms';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ToastrService } from 'ngx-toastr';
import { Urls } from 'src/app/_models/Urls/urls';
import { UrlsService } from 'src/app/_services/urls.service';

@Component({
  selector: 'app-url',
  templateUrl: './userboard.component.html',
  styleUrls: ['./userboard.component.css']
})
export class UserBoardComponent implements OnInit {

  UrlList?: Observable<Urls[]>;
  URlList1?: Observable<Urls[]>;
  urlForm: any;
  urlId = 0;
  constructor(private formbulider: FormBuilder,
    private urlsService: UrlsService, private router: Router,
    private jwtHelper: JwtHelperService, private toastr: ToastrService) { }

  ngOnInit() {
    this.urlForm = this.formbulider.group({
      urlName: ['', [Validators.required]],
      urlUsesCounter: ['', [Validators.required]],
      urlLong: ['', [Validators.required]],
      urlShort: ['', [Validators.required]]
    });
    this.getUrlList();
  }

  getUrlList() {
    this.URlList1 = this.urlsService.getUrlList();
    this.UrlList = this.URlList1;
  }

  PostUrl(url: Urls) {
    const url_Master = this.urlForm.value;
    this.urlsService.postUrlData(url_Master).subscribe(
      () => {
        this.getUrlList();
        this.urlForm.reset();
        this.toastr.success('Data Saved Successfully');
      }
    );
  }

  UrlDetailsToEdit(id: string) {
    this.urlsService.getUrlDetailsById(id).subscribe(urlResult => {
      this.urlId = urlResult.urlId;
      this.urlForm.controls['urlName'].setValue(urlResult.urlName);
      this.urlForm.controls['urlUsesCounter'].setValue(urlResult.urlUsesCounter);
      this.urlForm.controls['urlLong'].setValue(urlResult.urlLong);
      this.urlForm.controls['urlShort'].setValue(urlResult.urlShort);
    });
  }

  UpdateURl(url: Urls) {
    url.urlId = this.urlId;
    const url_Master = this.urlForm.value;
    this.urlsService.updateUrl(url_Master).subscribe(() => {
      this.toastr.success('Data Updated Successfully');
      this.urlForm.reset();
      this.getUrlList();
    });
  }

  DeleteUrl(id: number) {
    if (confirm('Do you want to delete this URL?')) {
      this.urlsService.deleteUrlById(id).subscribe(() => {
        this.toastr.success('URL Deleted Successfully');
        this.getUrlList();
      });
    }
  }

  Clear(url: Urls) {
    this.urlForm.reset();
  }

  public logOut = () => {
    localStorage.removeItem("jwt");
    this.router.navigate(["/"]);
  }

  isUserAuthenticated() {
    const token = localStorage.getItem("jwt");
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      return true;
    }
    else {
      return false;
    }
  }

}