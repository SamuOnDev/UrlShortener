import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import configurl from '../../assets/config/config.json'; 
import { UrlDto } from '../_models/UrlDto/urldto';
import { Urls } from '../_models/Urls/urls';

@Injectable({
  providedIn: 'root'
})
export class UrlsService {

  url = configurl.apiServer.url + '/api/urllist/';
  constructor(private http: HttpClient) { }

  getUrlList(userId: number): Observable<Urls[]> {
    return this.http.get<Urls[]>(this.url + 'UrlList?UserId=' + userId);
  }

  postUrlData(urlData: UrlDto): Observable<UrlDto> {
    const httpHeaders = { headers:new HttpHeaders({'Content-Type': 'application/json'}) };
    return this.http.post<UrlDto>(this.url + 'CreateUrl', urlData, httpHeaders);
  }

  // updateUrl(url: Urls): Observable<Urls> {
  //   const httpHeaders = { headers:new HttpHeaders({'Content-Type': 'application/json'}) };
  //   return this.http.post<Urls>(this.url + 'UpdateUrl?id=' + url.id, url, httpHeaders);
  // }

  deleteUrlById(id: number): Observable<number> {
    return this.http.delete<number>(this.url + 'DeleteUrl?idToDelete=' + id);
  }

  // getUrlDetailsById(id: string): Observable<Urls> {
  //   return this.http.get<Urls>(this.url + 'UrlDetail?urlId=' + id);
  // }

}