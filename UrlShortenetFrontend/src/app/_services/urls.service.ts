import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import configurl from '../../assets/config/config.json'; 
import { Urls } from '../_models/Urls/urls';

@Injectable({
  providedIn: 'root'
})
export class UrlsService {

  url = configurl.apiServer.url + '/api/urllist/';
  constructor(private http: HttpClient) { }

  getUrlList(): Observable<Urls[]> {
    return this.http.get<Urls[]>(this.url + 'UrlList');
  }

  postUrlData(urlData: Urls): Observable<Urls> {
    const httpHeaders = { headers:new HttpHeaders({'Content-Type': 'application/json'}) };
    return this.http.post<Urls>(this.url + 'CreateUrl', urlData, httpHeaders);
  }

  updateUrl(url: Urls): Observable<Urls> {
    const httpHeaders = { headers:new HttpHeaders({'Content-Type': 'application/json'}) };
    return this.http.post<Urls>(this.url + 'UpdateUrl?id=' + url.urlId, url, httpHeaders);
  }

  deleteUrlById(id: number): Observable<number> {
    return this.http.post<number>(this.url + 'DeleteUrl?id=' + id, null);
  }

  getUrlDetailsById(id: string): Observable<Urls> {
    return this.http.get<Urls>(this.url + 'UrlDetail?id=' + id);
  }

}