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
  userName: any = localStorage.getItem("username");
  userId: number = Number(localStorage.getItem("userid"));
  urlFormSend: any;

  TstingList?: Urls[] = [];

  constructor(private formbulider: FormBuilder,
    private urlsService: UrlsService, private router: Router,
    private jwtHelper: JwtHelperService, private toastr: ToastrService) { }

  ngOnInit() {
    this.urlFormSend = this.formbulider.group({
      title: ['', [Validators.required]],
      url: ['', [Validators.required]],
      userId: [this.userId, [Validators.required]]
    });
    this.urlForm = this.formbulider.group({
      title: ['', [Validators.required]],
      usesCounter: ['', [Validators.required]],
      url: ['', [Validators.required]],
      shortUrl: ['', [Validators.required]]
    });
    this.getUrlList(this.userId);
    
  }
  
  getUrlList(userId: number) {
    this.URlList1 = this.urlsService.getUrlList(userId);
    this.UrlList = this.URlList1;
  }

  PostUrl(url: Urls) {
    const url_Master = this.urlFormSend.value;
    console.log(url_Master)
    this.urlsService.postUrlData(url_Master).subscribe(
      () => {
        this.getUrlList(this.userId);
        this.urlFormSend.reset();
        this.toastr.success('URL Created Successfully');
      });
  }

  // UrlDetailsToEdit(id: string) {
  //   this.urlsService.getUrlDetailsById(id).subscribe(urlResult => {
  //     console.log(urlResult.title);
  //     this.urlForm.controls['title'].setValue(urlResult.title);
  //     this.urlForm.controls['url'].setValue(urlResult.url);
  //   });
  // }

  // UpdateURl(url: Urls) {
  //   url.id = this.urlId;
  //   const url_Master = this.urlForm.value;
  //   this.urlsService.updateUrl(url_Master).subscribe(() => {
  //     this.toastr.success('Data Updated Successfully');
  //     this.urlForm.reset();
  //     this.getUrlList(this.userId);
  //   });
  // }

  DeleteUrl(id: number) {
    if (confirm('Do you want to delete this URL?')) {
      this.urlsService.deleteUrlById(id).subscribe(() => {
        this.toastr.success('URL Deleted Successfully');
        this.getUrlList(this.userId);
      });
    }
  }

  Clear(url: Urls) {
    this.urlForm.reset();
  }

  public logOut = () => {
    localStorage.removeItem("jwt");
    localStorage.removeItem("username");
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